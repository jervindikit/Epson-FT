Imports System.Windows.Forms
Imports System.Threading
Imports XtraLibrary.SecsGem
Imports Rohm.Apcs.Tdc
Imports System.ComponentModel
Imports System.IO
Imports yezpedLibrary.WaferSlip
Imports CellcontrollerDataAdapter
Imports Rohm.Common.Model
Imports System.ServiceModel
Imports System.Data.SqlClient

Public Class FormMain
    Implements IDiebondDataAdaptorClient
    'Revision History ---------------------------------------------------------------
    '+ FEB.16.16,Prasarn    | Software issue
    '+ SecsGem All Reply Event to Frm Product    |Jun.14.2016
    '+ Relaese Ver1.01_170313"   \783 Reload CellconTag
    '+ Relaese Ver1.01_170403"   \783 TDC Control by web
    '+ Relaese Ver1.01_170408"   \783 Authen warning datail
    '===============================================================================


#Region "Single Instance"

    Private Shared c_Instance As FormMain

    Public Shared Function GetInstance() As FormMain
        Return c_Instance
    End Function

#End Region



#Region "Commomn Define"

    Dim WithEvents FrmTCPClient As TcpIpClientTest
    Private Delegate Sub UpdateTextDelegate(ByVal text As String)
    Public c_ServiceProxy As DiebondDataAdaptorServiceProxy


#End Region


#Region "Coding"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'because from main will only have 1
        'assign current instance to shared
        c_Instance = Me

        'set service instance
        c_ServiceProxy = New DiebondDataAdaptorServiceProxy(Me, My.Settings.DbDataAdaptorServiceUrl)

    End Sub

    Private Sub MDIParent1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'set current culture to support iLibrary
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US") '160712 \783 Fix display in Anno Domini 

        'create system folders
        MakeDirectories()

        If My.Settings.MDISizable Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If

        'Me.WindowState = FormWindowState.Maximized
        Using table As DataTable = New DataTable()
            LoadRegistrationMahine(table)                 'Load machine data from [SECSGEM].[SECS].[Machines]

            For Each row As DataRow In table.Rows
                Try
                    'load machine IP from db
                    CreateFormProductionAndTabPage(row("MCNo").ToString(), row("IP").ToString())

                Catch ex As Exception
                    '.....
                End Try

            Next

        End Using



        If DownloadReportSetting() Like "False*" Then                                  'Load Define Report from server
            LoadLinkReportAndDefineReportFromFile()                                    'Load Define Report from file
        End If


        If c_ServiceProxy Is Nothing Then
            'c_ServiceProxy = New DiebondDataAdaptorServiceProxy(Me, "net.Tcp://localhost:1234/WcfService")
            MachineInterfaceWCFLinkTest()
        End If
        TimerServiceLinkTest.Enabled = True

        For Each child As Form In Me.MdiChildren
            Dim pForm As FormProduction = DirectCast(child, FormProduction)
            If pForm IsNot Nothing Then
                pForm.StartHsmsHost()
            End If
        Next


    End Sub

    Private Sub FormMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown      '160624 Change width
        ToolStripLabelMessage.Width = Me.Width - 200
    End Sub

    Private Sub LoadRegistrationMahine(ByRef tblUser As DataTable)
        Name = System.Net.Dns.GetHostName()
        Try

            Using con As SqlConnection = New SqlConnection(My.Settings.SecsConnStr)
                Dim IPAddress As String = ""
                For Each obj In System.Net.Dns.GetHostEntry(Name).AddressList
                    If obj.ToString Like "172*" Or obj.ToString Like "10*" Then
                        IPAddress = obj.ToString
                        IPAddress = "172.27.21.254"
                    End If
                Next


                Using cmd = New SqlCommand("SELECT [MCNo],[IP] FROM [SECSGEM].[SECS].[Machines] WHERE (SelfConIP = @CellConIP)", con)
                    cmd.Parameters.AddWithValue("@CellConIP", IPAddress)
                    Using da = New SqlDataAdapter(cmd)
                        da.Fill(tblUser)
                    End Using

                End Using
            End Using


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LoadRegistrationMahine")
        End Try

    End Sub

    Public Sub LoadLinkReportAndDefineReportFromFile()

        Try
            Using linkTable As New DataTable
                linkTable.ReadXmlSchema(My.Application.Info.DirectoryPath & "\LinkTableSchema.xml")
                linkTable.ReadXml(My.Application.Info.DirectoryPath & "\LinkTable.xml")
                If linkTable.Rows(0)("MachineType").ToString <> My.Settings.MCType Then
                    ToolStripLabelMessage.Text = "Load Err Machine type in Setting difference from File (" &
                        linkTable.Rows(0)("MachineType").ToString & ")" & Format(Now, " |HH:mm:ss.fff")
                    Exit Sub
                End If
                SECSGEMDB.LoadLinkReportTableToDictionary(linkTable, m_LinkedReportDic)
            End Using

            Using defReportTable As New DataTable
                defReportTable.ReadXmlSchema(My.Application.Info.DirectoryPath & "\DefReportSchema.xml")
                defReportTable.ReadXml(My.Application.Info.DirectoryPath & "\DefReport.xml")
                If defReportTable.Rows(0)("MachineType").ToString <> My.Settings.MCType Then          '170328 \783 check machine type before load from file
                    ToolStripLabelMessage.Text = "Load Err Machine type in Setting difference from File (" &
                        defReportTable.Rows(0)("MachineType").ToString & ")" & Format(Now, " |HH:mm:ss.fff")
                    Exit Sub
                End If
                SECSGEMDB.LoadDefineReportTableToDictionary(defReportTable, m_DefinedReportDic)
            End Using
            ToolStripLabelMessage.Text = "Report Download FromFile Success"
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LoadLinkReportAndDefineReportFromFile()")
            ToolStripLabelMessage.Text = "Report Download FromFile Error"
        End Try
    End Sub
    Public Sub ShowStatusBar(message As String)
        ToolStripLabelMessage.Text = message
    End Sub
#End Region
#Region "UserInterface"
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CascadeToolStripMenuItem.Click
        ProductionFrmDockNone()
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub
    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileVerticalToolStripMenuItem.Click
        ProductionFrmDockNone()
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        ProductionFrmDockNone()
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub MiniToolStrip_Click(sender As System.Object, e As System.EventArgs) Handles MiniToolStrip.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub MaximizeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MaximizeToolStripMenuItem.Click
        For Each child As FormProduction In Me.MdiChildren
            child.Dock = DockStyle.Fill
        Next
    End Sub
    Private Sub FormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormToolStripMenuItem.Click
        Dim frm As FormFloatingMenu = FormFloatingMenu.GetInstance()
        frm.Width = Me.Width - 5
        If Not frm.Visible Then
            frm.Show()
        End If
        If frm.WindowState = FormWindowState.Minimized Then
            frm.WindowState = FormWindowState.Normal
            frm.Select()
        End If
        frm.BringToFront()
        frm.Location = New Point(MenuStrip.Location.X, MenuStrip.Location.Y + 25)
    End Sub
    Private Sub MinimizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizeToolStripMenuItem.Click
        Me.SendToBack()
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        MsgBox("Cellcon Software version " & CelconVer, MsgBoxStyle.Information, NetVerSion)
    End Sub
    Private Sub ProductionFrmDockNone()
        For Each child As FormProduction In Me.MdiChildren
            child.Dock = DockStyle.None
        Next
    End Sub
#End Region
    'Private Sub S2F13(ByVal UList As List(Of UInt32)) Handles FrmSecs.E_S2F13
    '    Send_S2F13(UList)
    'End Sub
    'Private Sub S5F3(ByVal Enable As Boolean, Optional ByVal ALID As UInteger = Nothing)
    '    Send_S5F3(Enable, ALID)
    'End Sub
    'Private Sub S10F3(ByVal ID As Integer, ByVal msg As String) Handles FrmSecs.E_S10F3
    '    Send_S10F3(CByte(ID), msg)
    'End Sub
    'Private m_SlbS As UpdateTextDelegate = New UpdateTextDelegate(AddressOf SlbStatusFrmSecs)
    'Private Sub SlbStatusFrmSecs(ByVal informationText As String)
    '    If Me.InvokeRequired Then
    '        'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
    '        Me.BeginInvoke(m_SlbS, informationText)
    '        Exit Sub
    '    End If
    '    FrmSecs.sblStatus.Text = informationText
    'End Sub
#Region "MDI children to Tab page"
    Private Sub CreateFormProductionAndTabPage(ByVal mcNo As String, ByVal ipAddress As String)
        'Creating MDI child form and initialize its fields
        Dim childForm As New FormProduction(mcNo, ipAddress, My.Settings.SECS_PortNumber)
        childForm.Text = mcNo
        childForm.MdiParent = Me
        childForm.lblIPAddress.Text = ipAddress
        childForm.lblLocalPort.Text = My.Settings.SECS_PortNumber.ToString
        'child Form will now hold a reference value to the tab control
        childForm.TabCtrl = TabControlFormProduction
        'Add a Tabpage and enables it
        Dim tp As New TabPage()
        tp.Parent = TabControlFormProduction
        tp.Text = childForm.Text
        tp.Show()
        'child Form will now hold a reference value to a tabpage
        childForm.TabPage = tp
        'Activate the MDI child form
        childForm.Show()
        childForm.Dock = DockStyle.Fill
        'Activate the newly created Tabpage
        TabControlFormProduction.SelectedTab = tp
    End Sub
    Private Sub TabControlFormProduction_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TabControlFormProduction.SelectedIndexChanged
        For Each child As Form In Me.MdiChildren
            Dim pForm As FormProduction = DirectCast(child, FormProduction)
            If pForm IsNot Nothing AndAlso pForm.TabPage.Equals(TabControlFormProduction.SelectedTab) Then
                child.Select()
                Exit For
            End If
        Next
    End Sub
    Function SelectedFormProduction() As FormProduction
        For Each child As Form In Me.MdiChildren
            Dim pForm As FormProduction = DirectCast(child, FormProduction)
            If pForm IsNot Nothing AndAlso pForm.TabPage.Equals(TabControlFormProduction.SelectedTab) Then
                Return pForm
            End If
        Next
        Return Nothing
    End Function


    Function MachineList() As List(Of String)
        Dim mc As New List(Of String)
        For Each child As FormProduction In Me.MdiChildren
            If child IsNot Nothing Then
                mc.Add(child.IP & "  (" & child.MCNo & ")")
            End If
        Next
        Return mc
    End Function

#End Region


#Region "WCF Service"

    Public Sub DiebondReceiveDataClient(McNo As String, LotNo As String, Package As String, Device As String, InputQty As Integer, Recipe As String, OpID As String) Implements CellcontrollerDataAdapter.IDiebondDataAdaptorClient.DiebondReceiveDataClient
        For Each child As Form In Me.MdiChildren
            Dim pForm As FormProduction = DirectCast(child, FormProduction)
            If pForm.MCNo = McNo Then
                pForm.LotRequest(McNo, LotNo, Package, Device, InputQty, Recipe, OpID)
            End If
        Next
    End Sub
    Private Sub MachineInterfaceWCFLinkTest()
        Try
            c_ServiceProxy.LinkTestToXcellcon("S1F13")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TimerServiceLinkTest_Tick(sender As System.Object, e As System.EventArgs) Handles TimerServiceLinkTest.Tick
        Try
            c_ServiceProxy.LinkTestToXcellcon("LinkTest")
        Catch ex As Exception
            'ShowStatusBar(ex.Message)
        End Try
    End Sub
    Public Sub LinkTestToLegacy(message As String) Implements CellcontrollerDataAdapter.IDiebondDataAdaptorClient.LinkTestToLegacy
        If message = "S1F14" Then

        End If
    End Sub

#End Region





End Class
