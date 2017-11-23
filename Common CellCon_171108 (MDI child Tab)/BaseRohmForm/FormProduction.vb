Imports System.Threading
Imports System.ComponentModel
Imports System.IO
Imports Rohm.Apcs.Tdc
Imports XtraLibrary.SecsGem
Imports MapConverterForCanon
Imports yezpedLibrary.WaferSlip
Imports CellcontrollerDataAdapter
Imports System.ServiceModel
Imports Rohm.Common.Model
'<ServiceBehavior(InstanceContextMode:=ServiceModel.InstanceContextMode.Single)>
Public Class FormProduction
#Region "Commomn Define"
    Private c_FormManageHsmsHost As FormManageHsmsHost
    Private c_Locker As Object
    Private m_MessageQueue As Queue(Of SecsMessageBase)
    Public CommuniationState As String = "NOT_CONNECTED"
    Private Delegate Sub UpdateTextDelegate1(ByVal text As String)
    'Private c_ServiceProxy As DiebondDataAdaptorServiceProxy
    Dim CellConTag As New CellConObj
    Private Delegate Sub S6F11Delegate(ByVal Obj As S6F11)
    Private Delegate Sub S2F42Delegate(ByVal CMD As String, ByVal Reply As S2F42)
    Private Delegate Sub SxFxxDelegate(ByVal e As SecondarySecsMessageEventArgs)
    Private Delegate Sub SxFxxPriDelegate(ByVal state As Object)

    Dim m_Equipment As New Equipment
    Dim WaferMapData As New List(Of String)

    Dim AlarmHashT As New Hashtable
    Structure AlarmKeys
        Dim AlarmID As Integer
        Dim AlarmMessage As String
        Dim AlarmNo As Integer
        Dim AlarmSet As Boolean
    End Structure
#End Region
   

    

#Region "Secs Events"

    Private Sub c_Host_CommLog(ByVal sender As Object, ByVal e As TraceLogEventArgs)  '160930 \783 Revise FrmSecsDisplay
        'save log
    End Sub

    Private Sub c_Host_ReceivedPrimaryMessage(ByVal sender As Object, ByVal e As PrimarySecsMessageEventArgs)
        Dim msg As SecsMessageBase = e.Primary

        Try
            Select Case msg.Stream
                Case 1
                    Select Case msg.Function
                        Case 1 'Are You Online?
                            Dim msgS1F2 As SecsMessage = New SecsMessage(1, 2, False)
                            msgS1F2.Items.Add(New SecsItemList("L0"))


                        Case 13 'Establish Communications Request
                            'Dim msgS1F14 As S1F14 = New S1F14()
                            'c_Host.Reply(msg, msgS1F14)
                            Reply_S1F13(msg)
                    End Select
                Case 2
                    Select Case msg.Function
                        Case 17 'date time request
                            Dim msgS2F18 As S2F18 = New S2F18(TimeFormat.A16)
                            c_Host.Reply(msg, msgS2F18)

                    End Select
                Case 5
                    Select Case msg.Function
                        Case 1 'Alarm Report Send
                            Dim msgS5F1 As S5F1 = DirectCast(msg, S5F1)
                            c_Host.Reply(msgS5F1, New S5F2())

                    End Select
                Case 6
                    Select Case msg.Function
                        Case 11
                            Perform_S6F11(CType(msg, S6F11))
                            Exit Sub           '160906 \783 Call productionn form
                    End Select
                Case 9
                    'dont need acknowledge

                Case 10
                    Perform_S10(msg)

                Case 64
                    Select Case msg.Function
                        Case 1 'supply tube id was read
                            'Perform_S64F1(CType(msg, S64F1))
                        Case 3 'tube changed
                            'Perform_S64F3(CType(msg, S64F3))
                        Case 11 'request new tube id for print
                            'Perform_S64F11(CType(msg, S64F11))
                    End Select
            End Select

        Catch ex As Exception
            SaveCatchLog("ProcessSecsMessage() Primary message recieve  : S" & msg.Stream & "F" & msg.Function, ex.ToString)
        End Try

    End Sub

    Private Sub c_Host_ReceivedSecondaryMessage(ByVal sender As Object, ByVal e As SecondarySecsMessageEventArgs)

        Dim priMsg As SecsMessageBase = e.Primary
        Dim sndMsg As SecsMessageBase = e.Secondary

        Select Case priMsg.Stream
            Case 1
                Select Case priMsg.Function
                    Case 13
                        Dim reply As S1F14E = DirectCast(sndMsg, S1F14E)
                        If reply.COMMACK = COMMACK.OK Then
                            UpdateStateThreadSafe("COMMUNICATING (Host Init)")
                            'GoOnline()     ' Eq Communication Revise  160627 \783
                        End If

                    Case 3

                    Case 17

                End Select 'End Select S1Fx

            Case 2
                Select Case priMsg.Function

                    Case 13

                    Case 15

                    Case 33

                    Case 35

                    Case 37

                    Case 43

                End Select 'End of Select Function of Stream 2

            Case 5
                Select Case priMsg.Function
                    Case 3

                End Select

            Case 6
                Select Case priMsg.Function
                    Case 23

                End Select


        End Select 'Select Stream end

 

    End Sub

    Private Sub c_Host_ErrorNotification(ByVal sender As Object, ByVal e As SecsErrorNotificationEventArgs)
     
    End Sub

    Private Sub c_Host_ConversionErrored(ByVal sender As Object, ByVal e As ConversionErrorEventArgs)
        
    End Sub

   

#End Region



#Region "Form main zone"
    Public Sub New(mcNo As String, ip As String, portNo As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        c_Locker = New Object()
        m_MessageQueue = New Queue(Of SecsMessageBase)

        ' Add any initialization after the InitializeComponent() call.
        c_McNo = mcNo
        c_IP = ip
        lbMcNo.Text = mcNo

        Dim gemOption As New GemOption

        gemOption.DeviceId = My.Settings.GEM_DeviceID 'must be same for ROHM standard
        gemOption.Protocol = GemProtocol.HSMS 'fixed---- Setting at Code

        Dim hshsParams As HsmsParameters = New HsmsParameters()
        gemOption.HsmsParameters = hshsParams

        hshsParams.IPAddress = ip
        hshsParams.PortNo = portNo
        hshsParams.Mode = HsmsConnectProcedure.ACTIVE

        hshsParams.T3_Interval = My.Settings.GEM_T3_Interval
        hshsParams.T5_Interval = My.Settings.GEM_T5_Interval
        'In case of equipment consumed sending time of massive SECS message more than T6
        'the equipment can not reply Linktest.Resp within 5 secs (default)
        'or the host received but can not process it (busy as same as not received)
        'I have to increase T6 interval from 5 to 20 secsonds
        hshsParams.T6_Interval = My.Settings.GEM_T6_Interval
        hshsParams.T7_Interval = My.Settings.GEM_T7_Interval
        hshsParams.Linktest_Interval = My.Settings.GEM_LinkTest_Interval
        hshsParams.LinktestEnabled = My.Settings.GEM_LinkTest_Enabled

        Dim equipmentModel As EquipmentModel = New EquipmentModel(My.Settings.MCType)
        equipmentModel.Connection = gemOption
        Dim factory As SecsHostFactory = New SecsHostFactory()

        c_Host = CType(factory.Create(equipmentModel), XtraLibrary.SecsGem.HsmsHost)

        If (Not System.IO.Directory.Exists(DIR_LOG & "\" & mcNo)) Then
            System.IO.Directory.CreateDirectory(DIR_LOG & "\" & mcNo)
        End If

        c_Host.LogDirectory = DIR_LOG & "\" & mcNo

        c_Host.HsmsLogEnabled = True

        AddHandler c_Host.ReceivedPrimaryMessage, AddressOf c_Host_ReceivedPrimaryMessage
        AddHandler c_Host.ReceivedSecondaryMessage, AddressOf c_Host_ReceivedSecondaryMessage
        AddHandler c_Host.HsmsStateChanged, AddressOf c_Host_HsmsStateChanged
        AddHandler c_Host.ErrorNotification, AddressOf c_Host_ErrorNotification
        AddHandler c_Host.ConversionErrored, AddressOf c_Host_ConversionErrored
        AddHandler c_Host.TracedSmlLog, AddressOf c_Host_CommLog        '160930 \783 Add comlog FrmSecs

        Dim secsParser As SecsMessageParserBase = c_Host.MessageParser

        ''Example ==='
        'Dim msg As SecsMessageBase = m_Host.MessageParser.ToSecsMessage(Nothing)
        'Dim smlText As String = SmlBuilder.ToSmlString(msg)
        'm_Host.Send(msg)
        '============
        secsParser.RegisterCustomSecsMessage(GetType(S1F13E))
        secsParser.RegisterCustomSecsMessage(GetType(S1F4))
        secsParser.RegisterCustomSecsMessage(GetType(S1F14E))   ' Regis in Recive data from eqiptment for change to class format. 
        secsParser.RegisterCustomSecsMessage(GetType(S1F18))    '160630 \783 AutoLoad Revise
        secsParser.RegisterCustomSecsMessage(GetType(S2F34))
        secsParser.RegisterCustomSecsMessage(GetType(S2F36))
        secsParser.RegisterCustomSecsMessage(GetType(S2F38))
        secsParser.RegisterCustomSecsMessage(GetType(S2F41))
        secsParser.RegisterCustomSecsMessage(GetType(S2F42))     '160903 \783 Add S2F42
        secsParser.RegisterCustomSecsMessage(GetType(S2F50))
        secsParser.RegisterCustomSecsMessage(GetType(S2F14))
        secsParser.RegisterCustomSecsMessage(GetType(S2F16))
        secsParser.RegisterCustomSecsMessage(GetType(S2F17))
        secsParser.RegisterCustomSecsMessage(GetType(S5F1))
        secsParser.RegisterCustomSecsMessage(GetType(S5F4))
        secsParser.RegisterCustomSecsMessage(GetType(S6F11))
        secsParser.RegisterCustomSecsMessage(GetType(S6F24))
        secsParser.RegisterCustomSecsMessage(GetType(S7F2))
        secsParser.RegisterCustomSecsMessage(GetType(S7F4))
        secsParser.RegisterCustomSecsMessage(GetType(S7F6))
        secsParser.RegisterCustomSecsMessage(GetType(S7F18))
        secsParser.RegisterCustomSecsMessage(GetType(S7F20))
        secsParser.RegisterCustomSecsMessage(GetType(S10F1))
        secsParser.RegisterCustomSecsMessage(GetType(S12F1Esec))   '160912 \783 Add property REPP  170214 \783 Ignore result
        secsParser.RegisterCustomSecsMessage(GetType(S12F3))
        secsParser.RegisterCustomSecsMessage(GetType(S12F5Esec))   '170214 \783 Ignore result
        secsParser.RegisterCustomSecsMessage(GetType(S12F9))       '170214 \783 Ignore result
        secsParser.RegisterCustomSecsMessage(GetType(S12F15))

    End Sub
    Private Sub FormProduction_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        Me.c_TabCtrl.SelectedTab = Me.c_TabPage

        If Not c_TabCtrl.Visible Then
            c_TabCtrl.Visible = True
        End If

    End Sub

    Private Sub ProcessForm_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim myPen As Pen
        'myPen = New Pen(Color.RoyalBlue, 17)
        'e.Graphics.DrawLine(myPen, 0, 10, Me.Width, 10)
        'myPen = New Pen(Color.MidnightBlue, 1)
        'e.Graphics.DrawLine(myPen, 0, 19, Me.Width, 19)
        myPen = New Pen(Color.PowderBlue, 25)
        e.Graphics.DrawLine(myPen, 0, 110, Me.Width, 110)
        myPen = New Pen(Color.CadetBlue, 1)
        e.Graphics.DrawLine(myPen, 1, 122, Me.Width, 122)

    End Sub

    Protected Overrides ReadOnly Property CreateParams() As CreateParams   'Disable Close(x) Button
        Get
            Dim param As CreateParams = MyBase.CreateParams
            param.ClassStyle = param.ClassStyle Or &H200
            Return param
        End Get
    End Property

    ' Command For Clear all data table


    Private Sub EqConnectToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles EqConnectToolStripMenuItem.Click

    End Sub

    Private Sub pbxLogo_Click(sender As System.Object, e As System.EventArgs) Handles pbxLogo.Click, MaximizeToolStripMenuItem.Click

    End Sub


    Private Sub ProcessForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        c_Host.Connect()
        If Not CommuniationState Like "COMMUNICATING*" Then
            Me.BackColor = Color.Red
        End If
        ComboBox1.DataSource = (System.[Enum].GetValues(GetType(m_ProcessingStates)))


    End Sub


    Private Sub SecsConsoleToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SecsConsoleToolStripMenuItem.Click
        ShowFormManageSecsHost()
        c_FormManageHsmsHost.TabAdmin.SelectedTab = c_FormManageHsmsHost.TabPageCommLog
    End Sub


    Private Sub BMRequestToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles BMRequestToolStripMenuItem.Click
        Dim tmpStr As String

        tmpStr = "MCNo=" & My.Settings.EquipmentNo
        tmpStr = tmpStr & "&LotNo=" & lbLotNo.Text
        If lbStartTime.Text <> "" Then 'AndAlso lbEndTime.Text = "" Then
            tmpStr = tmpStr & "&MCStatus=Running"
        Else
            tmpStr = tmpStr & "&MCStatus=Stop"
        End If

        tmpStr = tmpStr & "&AlarmNo="
        tmpStr = tmpStr & "&AlarmName="

        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/LsiPETE/LSI_Prog/Maintenance/MainloginPD.asp?" & tmpStr, vbNormalFocus)
        'Process.Start("C:\WINDOWS\system32\osk.exe")
    End Sub

    Private Sub PMRepairToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PMRepairToolStripMenuItem.Click
        Dim MCNo As String = My.Settings.EquipmentNo
        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/LsiPETE/LSI_Prog/Maintenance/MainPMlogin.asp?" & "MCNo=" & MCNo, vbNormalFocus)
    End Sub


    Private Sub ByAutoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ByAutoToolStripMenuItem.Click  '161019 \783
        Try
            Dim requestUrl As String             'Call Andon by pass parameter 161029 \783
            requestUrl = String.Format("http://webserv.thematrix.net/andontmn/Client/Default.aspx?p={0}&mc={1}&lot={2}&pkg={3}&dv={4}&line={5}&op={6}",
                                        My.Settings.ProcessName, lbMcNo.Text, lbLotNo.Text, lbPackage.Text, lbDevice.Text, "", lbOPID.Text)
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe " & requestUrl, AppWinStyle.NormalFocus)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ByManualToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ByManualToolStripMenuItem.Click
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/andontmn", AppWinStyle.NormalFocus) 'Web andon for manual M/C     'Maual input
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub APCSStaffToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles APCSStaffToolStripMenuItem.Click
        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/ApcsStaff", AppWinStyle.NormalFocus)

    End Sub

    Private Sub WorkRecordToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles WorkRecordToolStripMenuItem.Click
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/ERECORD/", AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "Xml File Manage (ObjClass serailize in folder EquipmentObj)"

    '1. 'WrXml()   For Write to Serailize file to path PathXmlObj
    '    RdXml()   For Read to Serailize file
    '2. After lotend 




    ' -------------------Keep all file of  PathXmlObj in LotNo and move to BackUpObj Directory
    Private Sub MakeLotFolderToBackUp(ByVal LotNo As String)
        Dim LotDirName As String = BackUpObj & "\" & LotNo & "_" & Format(Now, "yyyyMMddTHHmmss")
        System.IO.Directory.CreateDirectory(LotDirName)
        For Each fi As FileInfo In New DirectoryInfo(CellconObjPath).GetFiles
            File.Move(fi.FullName, LotDirName & "\" & fi.Name)
        Next
        BackUpLotClean()
    End Sub

    ''' Clean log by limit folder size  delete form old to new defualt 200M
    '''

    Private Sub CleanLog(Optional ByVal DirSizeLimit_Mbyte As Integer = 200)   '161212 \783 Add clean log

        Try
            Dim Mlmt As Integer = DirSizeLimit_Mbyte * 1000000
            Dim orderedFiles = New System.IO.DirectoryInfo(DIR_LOG).GetFiles().OrderByDescending(Function(x) x.CreationTime).ToArray   'Order by create data(Not Modify) 
            Dim index As Integer = 0
            Dim DirSize As Integer = 0

            For i = 0 To orderedFiles.Length - 1
                DirSize = CInt(DirSize + orderedFiles(i).Length)  'File Over Size count
                index = +1
                If Mlmt < DirSize Then
                    index = i
                    Exit For
                End If
            Next
            If index >= orderedFiles.Length - 1 Then   'index = files content >> files are under size limit (exitisub) 
                Exit Sub
            End If

            For i = index To orderedFiles.Length - 1  'Delete from index that over size
                File.Delete(orderedFiles(i).FullName)
            Next


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "CleanLog()")
        End Try

    End Sub

    '--------------------- Cleanning Directory in  backup 
    Private WithEvents BackUp As New BackgroundWorker

    Friend Sub BackUpLotClean()
        BackUp.RunWorkerAsync()
    End Sub
    Private Sub DoBackup(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackUp.DoWork
        Try
            e.Result = ""
            Dim Dirs = Directory.GetDirectories(BackUpObj)
            If Dirs.Count > 100 Then   'Store 100 Folders over then move to BackUpOld 100 files
                For Each DirSo In Dirs
                    Dim DirInfo As New System.IO.DirectoryInfo(DirSo)
                    Directory.Move(DirSo, Path.Combine(BackUpObjOld, DirInfo.Name))
                Next
                e.Result = "Move Backup file success (100Files)"
            End If
            Dim OldDirs = Directory.GetDirectories(BackUpObjOld)
            If OldDirs.Count > 100 Then           'if over 100  Folders del 10 Folders of BackUpOld
                Dim DirDes = From l In OldDirs Order By Directory.GetCreationTime(l) Ascending    'SortFile by Modify time
                For i = 0 To 10
                    Directory.Delete(DirDes(i), True)             'Del 10 Folders
                Next
                e.Result = "Del BackupOld file success (10 Folders)"
            End If


        Catch ex As Exception
            e.Result = ex.ToString
        End Try

    End Sub
    Private Sub Backup_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackUp.RunWorkerCompleted
        Dim result As String = CStr(e.Result)
        If result <> "" Then
            SaveCatchLog(result, "DoBackUp()")
        End If

    End Sub



#End Region



#Region "===AuthenticationUser"


    'Dim ETC2 As String                          'From QR Code ,Check ETC2 = BDXX-M/BJ/C is auto motive
    'Dim strNextOperatorNo As String              'OP No.
    'Dim GetUserAuthenGroupByMCType As String       'M/C Type ( Refer with DBx.Group)
    'Dim GL_Group As String                         'GL Gruop ( Refer with DBx.Group)
    'Dim Process As String                        'Process Ex. "FL"
    'Dim MCNo As String                           'MC No Ex "FL-V-01"
    '    Friend ErrMesETG As String

    '    Friend Function CheckProductionCondition(ByVal ETC2 As String, ByVal strNextOperatorNo As String, ByVal GetUserAuthenGroupByMCType As String, ByVal GL_Group As String, ByVal Procees As String, ByVal MCNo As String) As Boolean

    '        Dim permission As New AuthenticationUser.AuthenUser
    '        Dim AuthenPass As Boolean
    '        ErrMesETG = ""
    '        OprData.PermitCheckResult = "Check"
    '        Try

    '            If permission.CheckAutomotiveLot(ETC2) Then
    '                OprData.AutoMotiveLot = True
    '                'This lot is Automotive
    '                If Not permission.CheckMachineAutomotive(Procees, MCNo) Then          '(EQP.Machine.MCNo = @MCNo) AND (LSIProcess.Name = @ProcessName) AND (EQP.Machine.Automotive = 'true')
    '                    ErrMesETG = "MC No.นี้ไม่สามารถรัน Lot Automotive ได้ "
    '                    '_OperatorAlarm = "Machine cannot run the automotive lot,Please contact ETG"
    '                    'MsgBox("MC No.นี้ไม่สามารถรัน Lot Automotive ได้  กรุณาติดต่อ ETG/SYSTEM")
    '                    OprData.PermitCheckResult = "False : Not Machine AutoMotive (Auotmotive Lot) MC No. " & MCNo
    '                    Return False
    '                End If

    '                Dim UserAuthen As Boolean = permission.AuthenUser(strNextOperatorNo, GetUserAuthenGroupByMCType)        '170408 \783 Authen Detail warning
    '                Dim UserAutoMotive As Boolean = permission.AuthenUser(strNextOperatorNo, "AUTOMOTIVE")                  '170408 \783 Authen Detail warning
    '                Dim UserGL As Boolean = permission.AuthenUser(strNextOperatorNo, GL_Group) 'GL Can run every condition  '170408 \783 Authen Detail warning

    '                AuthenPass = UserAuthen And UserAutoMotive

    '                If AuthenPass = False Then AuthenPass = UserGL 'GL Can run every condition
    '                If AuthenPass = False Then
    '                    If Not UserAuthen Then
    '                        ErrMesETG = "OP No.นี้ไม่สามารถรัน Lot Automotive ได้ เนื่องจาก license หมดอายุ หรือ ไม่มี license ( GroupCheck: " & GetUserAuthenGroupByMCType & ")" & "กรุณาติดต่อ ETG"
    '                        GoTo AutoLotCheckEndLoop    '170727 Warning Bug
    '                    End If
    '                    If Not UserAutoMotive Then
    '                        ErrMesETG = "OP No.นี้ไม่สามารถรัน Lot Automotive ได้ เนื่องจากไม่มีสิทธิรัน AUTOMOTIVE ( GroupCheck: AUTOMOTIVE ) กรุณาติดต่อ ETG"
    '                        GoTo AutoLotCheckEndLoop  '170727 Warning Bug
    '                    End If
    '                    If Not UserGL Then
    '                        ErrMesETG = "OP No(GL).นี้ไม่สามารถรัน Lot Automotive ได้ เนื่องจากไม่มีสิทธิ ในกลุ่ม GL ( GroupCheck: " & GL_Group & ")" & "กรุณาติดต่อ ETG"

    '                    End If
    'AutoLotCheckEndLoop:
    '                    OprData.PermitCheckResult = "False : Not Operaotr AutoMotive OP ID. " & strNextOperatorNo
    '                End If
    '                If AuthenPass Then
    '                    OprData.PermitCheckResult = "True : (Automotive Lot)"

    '                End If
    '            Else
    '                OprData.PermitCheckResult = "False : Not Operaotr AutoMotive (Auotmotive Lot) OP ID. " & strNextOperatorNo
    '                OprData.AutoMotiveLot = False
    '                'This lot isn't Automotive
    '                AuthenPass = permission.AuthenUser(strNextOperatorNo, GetUserAuthenGroupByMCType)
    '                If AuthenPass = False Then AuthenPass = permission.AuthenUser(strNextOperatorNo, GL_Group)
    '                If AuthenPass = False Then
    '                    ErrMesETG = "OP No.นี้ไม่สามารถรัน Lotนี้ ได้ (license หมดอายุ หรือ ไม่มี license ,GroupCheck: " & GetUserAuthenGroupByMCType & ")" & "กรุณาติดต่อ ETG"   '170408 \783 Authen Detail warning
    '                    '_OperatorAlarm = "OP No cannot run ,Please contact ETG"
    '                    OprData.PermitCheckResult = "False : OP ID No license or expire license (Normal Lot) OP ID. " & strNextOperatorNo
    '                End If
    '                If AuthenPass Then
    '                    OprData.PermitCheckResult = "True : (Normal Lot)"

    '                End If
    '            End If

    '            Return AuthenPass
    '        Catch ex As Exception 'Network Error
    '            ErrMesETG = "Connection Error"
    '            Return False
    '        End Try
    '    End Function


    Public Function CheckUserPermission(ByVal operatorNo As String, ByVal machineType As String, ByVal groupName As String) As CheckUserPermissionResult

        Dim permission As AuthenticationUser.AuthenUser = New AuthenticationUser.AuthenUser()
        Dim ret As CheckUserPermissionResult = New CheckUserPermissionResult()

        ret.IsPermit = permission.AuthenUser(operatorNo, machineType)

        If ret.IsPermit = False Then
            ret.IsPermit = permission.AuthenUser(operatorNo, groupName)
        End If

        If ret.IsPermit = False Then
            ret.ErrorMessage = "OP No.นี้ไม่สามารถรันได้  กรุณาติดต่อ ETG"
        End If

        Return ret
    End Function

    Public Function IsMachineAuomotive(ByVal Procees As String, ByVal MCNo As String) As Boolean
        Dim permission As New AuthenticationUser.AuthenUser
        Return permission.CheckMachineAutomotive(Procees, MCNo)           '(EQP.Machine.MCNo = @MCNo) AND (LSIProcess.Name = @ProcessName) AND (EQP.Machine.Automotive = 'true')
    End Function


#End Region


#Region "Custom Protolcol"

    Friend Sub RcvManage(ByVal data As String)       'If  My.Settings.CsProtocol Disable data will not come

        Dim Parameter As String
        Parameter = data
        Dim RcvManage_Task As New BackgroundWorker
        AddHandler RcvManage_Task.DoWork, AddressOf RcvManage_Dowork
        AddHandler RcvManage_Task.RunWorkerCompleted, AddressOf RcvManage_RunComplete
        RcvManage_Task.RunWorkerAsync(Parameter)
    End Sub


    Private Sub RcvManage_Dowork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim Agr As String
        Agr = CType(e.Argument, String)

        Dim CmdHeader As String
        Dim Cmddata() As String = Agr.Split(CChar(","))
        CmdHeader = Cmddata(0)

        Select Case CmdHeader
            'Case "LR "
            '    LR(Agr)

            'Case "LS "
            '    Send("LS,00")       'LS No use Equiptment can not stop after sent LS to Cellcon

            'Case "LE "
            '    LE(Agr)
            'BackUpLotClean()

            'Case "SC "

            '    SC(Agr)

        End Select


        e.Result = Agr
    End Sub

    Private Sub RcvManage_RunComplete(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        Dim Agr As String
        Agr = CType(e.Result, String)

    End Sub

    'Private Sub Send(ByVal str As String)              ''If  My.Settings.CsProtocol Disable data will not go
    '    If Not My.Settings.CsProtocol_Enable Then
    '        Exit Sub
    '    End If

    '    RaiseEvent E_CsProtocol_SendMsg(str & vbCr)
    'End Sub

    Delegate Sub AccessControlDelg(ByVal data As String)
    Private m_LR As AccessControlDelg = New AccessControlDelg(AddressOf LR)
    Private m_SC As AccessControlDelg = New AccessControlDelg(AddressOf SC)
    Private m_LE As AccessControlDelg = New AccessControlDelg(AddressOf LE)

    'LR ,QR CodeData,OpNo,InputQty,RecipeName,EqStationNo[CR]
    Private Sub LR(ByVal Cmddatax As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_LR, Cmddatax)
            Exit Sub
        End If
        Try
            'USE Raise Event for Send TDC
            'Event E_LRCheck(ByVal EqNo As String, ByVal LotNo As String)
            'LotRequest()

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LR()")
        End Try

    End Sub


    'SC ,99,AlarmNo[Cr]   --- 01 AlarmSet,00 AalrmClear
    Private Sub SC(ByVal Cmddatax As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_SC, Cmddatax)
            Exit Sub
        End If
        Try

        Catch ex As Exception
            SaveCatchLog(ex.ToString, "SC()")
        End Try


    End Sub


    ' LE ,LotNo.[CR]

    Private Sub LE(ByVal Cmddatax As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(m_LE, Cmddatax)
            Exit Sub
        End If

        Try
            'USE Raise Event for Send TDC
            'Event E_LECheck(ByVal EQNo As String, ByVal LotNo As String, ByVal EndTime As Date, ByVal GoodPcs As Integer, ByVal NgPcs As Integer, ByVal OPID As String, ByVal EndMode As EndModeType)


        Catch ex As Exception
            SaveCatchLog(ex.ToString, "LE()")
        End Try


    End Sub


    Private Sub SendRM_LOCK()
        'Send("RM,LOCK")
    End Sub


#End Region

#Region "Lot Management"

    'Public Sub DataInitialSucess(Optional ByVal WorkSlipQR As WorkingSlipQRCode = Nothing, Optional ByVal DCSlipQR As WaferSlip = Nothing)

    'FrmTableDataClear()
    'If My.Settings.DCSlip Then


    'Else        'WorkingSlip
    '    lbLotNo.Text = WorkSlipQR.LotNo
    '    lbPackage.Text = WorkSlipQR.Package
    '    lbDevice.Text = WorkSlipQR.Device
    '    lbRecipe.Text = WorkSlipQR.Code
    '    OprData.WaferLotID = WorkSlipQR.WFLotNo
    '    'lbWaferLotNo.Text = WorkSlipQR.WFLotNo
    '    LotRequest()

    'End If


    'End Sub



    Public Sub LotRequest(ByVal McNo As String, ByVal LotNo As String, ByVal Package As String, ByVal Device As String, ByVal InputQty As Integer, ByVal Recipe As String, ByVal OpID As String)
        'If lbLotNo.Text = "" Then
        '    'MakeAlarmCellCon("LotNo is nothing", "TDC Lot Request")
        '    'RaiseEvent E_SlInfo("LotNo is nothing")
        '    Exit Sub
        'End If
        CellConTag = New CellConObj    'Clear CellconTag   '170126 \783 CellconTag
        CellConTag.Process = My.Settings.ProcessName
        CellConTag.LotID = LotNo
        CellConTag.Package = Package
        CellConTag.DeviceName = Device
        CellConTag.Recipe = Recipe
        'CellConTag.QrData = OprData.QrData
        CellConTag.MCNo = McNo
        CellConTag.OPID = OpID
        CellConTag.INPUTQty = CStr(InputQty)
        'CellConTag.WaferLotID = Lotdata.WaferLotNo
        'CellConTag.LSMode = OprData.LSMode

        UpdateLabelsText()




        'If OprData.WaferID <> "" And My.Settings.WaferMappingUse Then
        '    CellConTag.CurrentWaferID = OprData.WaferID
        '    If (CellConTag.WaferID.Exists(Function(x) x = OprData.WaferID)) Then     'if exist remove  and new add
        '        CellConTag.WaferID.Remove(OprData.WaferID)
        '        CellConTag.WaferID.Add(OprData.WaferID)
        '    Else
        '        CellConTag.WaferID.Add(OprData.WaferID)
        '    End If
        '    OprData.WaferID = ""                     'Clear Data after save
        'End If

        'CellConTag.PermitCheckResult = OprData.PermitCheckResult
        'OprData.PermitCheckResult = ""
        'If Not My.Settings.TDC_Enable Then
        '    CellConTag.LRReply = "TDC Disable"
        'End If

        'RaiseEvent E_LRCheck(lbMcNo.Text, lbLotNo.Text)


    End Sub


    Private Sub Lotstart(Optional ByVal LsMode As RunModeType = RunModeType.Normal)

        Dim datex As Date = Now
        lbStartTime.Text = Format(datex, "yyyy/MM/dd HH:mm:ss")
        CellConTag.LotStartTime = lbStartTime.Text
        'RaiseEvent E_LSCheck(CellConTag.MCNo, CellConTag.LotID, datex, CellConTag.OPID, LsMode)
        If Not My.Settings.TDC_Enable Then
            CellConTag.LSReply = "TDC Disable"
            If CellConTag.LotID <> "" Then    '170404 \783
                WriteToXmlCellcon(CellconObjPath & "\" & lbLotNo.Text & ".xml", CellConTag)  '170126 \783 CellconTag
            End If

        End If
        FormMain.GetInstance.c_ServiceProxy.DiebondLotStart(lbMcNo.Text, datex)
    End Sub


    Private Sub LotEnd(Optional ByVal LeMode As EndModeType = EndModeType.Normal)

        Dim datex As Date = Now
        lbEndTime.Text = Format(datex, "yyyy/MM/dd HH:mm:ss")
        CellConTag.LotEndTime = lbEndTime.Text

        CellConTag.TotalGoodPcs = CInt(CellConTag.GoodCat1 + CellConTag.GoodCat2)
        CellConTag.TotalNGPcs = CInt(CellConTag.NGbin1 + CellConTag.NGbin2 + CellConTag.NGbin3 + CellConTag.NGbin4 + CellConTag.NGbin5 + CellConTag.NGbin6)
        lbNGTotal.Text = CStr(CellConTag.TotalNGPcs)
        lbGoodTotal.Text = CStr(CellConTag.TotalGoodPcs)

        FormMain.GetInstance.c_ServiceProxy.DiebondLotEnd(lbMcNo.Text, datex, CellConTag.TotalGoodPcs, CellConTag.TotalNGPcs)
        'RaiseEvent E_LECheck(CellConTag.MCNo, CellConTag.LotID, datex, CellConTag.TotalGoodPcs, CellConTag.TotalNGPcs, CellConTag.OPID, LeMode)
        CleanLog(1000) '1G backup limit
        BackUpLotClean()
        If Not My.Settings.TDC_Enable Then
            CellConTag.LEReply = "TDC Disable"
            If CellConTag.LotID <> "" Then    '170404 \783
                WriteToXmlCellcon(CellconObjPath & "\" & CellConTag.LotID & ".xml", CellConTag)  '170126 \783 CellconTag
            End If

            CellConTag = New CellConObj              'Clear data
        End If
    End Sub

    Private Sub Reload(Optional ByVal LotId As String = "")  '170330 \783 Reload File Addition
        'Try
        '    If LotId = "" Then
        '        Dim QrForm As New QrInput(QrInput.QrType.WorkingSlip)
        '        QrForm.Label1.Text = "สแกน QR ของ Working Slip"
        '        QrForm.ShowDialog()
        '        If OprData.ReloadLot = "" Then
        '            MakeAlarmCellCon("อ่านค่า Working Slip ล้มเหลว", "Reload Data", "OprData.ReloadLot ไม่มีค่า")
        '            Exit Sub
        '        End If
        '        LotId = OprData.ReloadLot

        '    End If

        '    If Not File.Exists(CellconObjPath & "\" & LotId & ".xml") Then
        '        MakeAlarmCellCon("ไม่พบ File ที่ต้องการ Load (" & LotId & ".xml)", "Reload Data", "File.exists()")
        '        Exit Sub
        '    End If
        '    CellConTag = ReadFromXmlCellcon(CellconObjPath & "\" & LotId & ".xml")

        '    CellConTag.ReloadLot = OprData.ReloadLot & "_" & Format(Now, "yyyy/MM/ddTHH:mm:ss")
        '    RefreshLb()


        'Catch ex As Exception
        '    SaveCatchLog(ex.ToString, " Reload()")
        'End Try
    End Sub


    'Public Function WaferMapReadFromZion() As Boolean

    'Try

    '    If Not My.Computer.Network.IsAvailable Then                'unplug check
    '        'MakeAlarmCellCon("PC Nework point unplug")
    '        Return False
    '    End If

    '    If Not My.Computer.Network.Ping(_ipDbxUser) Then            'Can Pink if Computer Connect only
    '        'MakeAlarmCellCon("การเชื่อมต่อกับ" & _ipServer & "ล้มเหลวไม่สามารถดำเนินการต่อได้")
    '        Return False
    '    End If

    '    If Directory.Exists(WaferMapDir) Then                    'Delete All 
    '        Directory.Delete(WaferMapDir, True)
    '    End If

    '    If Not Directory.Exists("\\" & _ipServer & "\WaferMapping\" & OprData.WaferLotID) Then
    '        'MakeAlarmCellCon("ไม่พบ Wafer LotID " & OprData.WaferLotID & " ใน Server(" & _ipServer & ")")
    '        Return False
    '    End If

    '    Directory.CreateDirectory("\\" & _ipServer & "\WaferMapping\" & OprData.WaferLotID)
    '    My.Computer.FileSystem.CopyDirectory("\\" & _ipServer & "\WaferMapping\" & OprData.WaferLotID & "\", WaferMapDir & "\" & OprData.WaferLotID)
    '    'RaiseEvent E_SlInfo("WaferMapping load from Server Successful")
    '    Return True

    'Catch ex As Exception
    '    SaveCatchLog(ex.ToString, "WaferMapReadFromZion()")
    '    'MakeAlarmCellCon("Copy directory WaferMapping  from Server Err")
    '    Return False
    'End Try


    'End Function
#End Region

#Region "===  KeyBoard Control"

    Dim KYB As KeyBoard

    Private Sub KeyBoardCall(ByVal OBJ As TextBox, ByVal NumpadKeys As Boolean, Optional ByVal infoImage As System.Drawing.Image = Nothing, Optional ByVal Tag As String = "")
        If KYB Is Nothing Then
            KYB = New KeyBoard
        ElseIf KYB.IsDisposed Then
            KYB = New KeyBoard
        End If
        KYB.TargetTextBox = OBJ
        KYB.tbxMonitorx.Text = OBJ.Text
        KYB.tbxMonitorx.Select(KYB.tbxMonitorx.Text.Length, 0)
        KYB.Owner = Me
        KYB.StartPosition = FormStartPosition.Manual
        Dim xsize As Rectangle = Screen.PrimaryScreen.Bounds
        KYB.Left = 10
        KYB.Top = 0
        KYB.TopMost = True
        KYB.NumPad = NumpadKeys                        'Numpad =True , Keyboard = False
        KYB.pbxHelper.BackgroundImage = infoImage
        KYB.TagID = Tag

        KYB.Show()
        AddHandler KYB.FormClosed, AddressOf KYB_close

    End Sub


    Private Sub KeyBoardCall(ByVal OBJ As Label, ByVal NumpadKeys As Boolean, Optional ByVal infoImage As System.Drawing.Image = Nothing, Optional ByVal Tag As String = "")

        If KYB Is Nothing Then
            KYB = New KeyBoard
        ElseIf KYB.IsDisposed Then
            KYB = New KeyBoard
        End If

        KYB.TargetLabel = OBJ
        KYB.tbxMonitorx.Text = OBJ.Text
        KYB.tbxMonitorx.Select(KYB.tbxMonitorx.Text.Length, 0)
        KYB.Owner = Me
        KYB.StartPosition = FormStartPosition.Manual
        Dim xsize As Rectangle = Screen.PrimaryScreen.Bounds
        KYB.Left = 10
        KYB.Top = 0
        KYB.TopMost = True
        KYB.NumPad = NumpadKeys                        'Numpad =True , Keyboard = False
        KYB.pbxHelper.BackgroundImage = infoImage
        KYB.TagID = Tag
        KYB.Show()
        AddHandler KYB.FormClosed, AddressOf KYB_close

    End Sub

    Private Sub KeyBoardCallDialog(ByVal OBJ As Label, ByVal NumpadKeys As Boolean, Optional ByVal infoImage As System.Drawing.Image = Nothing, Optional ByVal Tag As String = "")

        If KYB Is Nothing Then
            KYB = New KeyBoard
        ElseIf KYB.IsDisposed Then
            KYB = New KeyBoard
        End If
        KYB.TargetLabel = OBJ
        KYB.tbxMonitorx.Text = OBJ.Text
        KYB.tbxMonitorx.Select(KYB.tbxMonitorx.Text.Length, 0)
        KYB.StartPosition = FormStartPosition.Manual
        Dim xsize As Rectangle = Screen.PrimaryScreen.Bounds
        KYB.Left = 10
        KYB.Top = 0
        KYB.TopMost = True
        KYB.NumPad = NumpadKeys                        'Numpad =True , Keyboard = False
        KYB.pbxHelper.BackgroundImage = infoImage
        KYB.TagID = Tag

        KYB.ShowDialog()
        KYB.Close()
        KYB.TagID = ""
    End Sub

    Private Sub KYB_close(sender As Object, e As FormClosedEventArgs)
        KYB.TagID = ""
    End Sub

#End Region



#Region "User Coding Area"
    Function QRWorkingSlipInputInitailCheck(ByVal BeAfRead As Boolean, Optional ByVal WorkSlipQR As WorkingSlipQRCode = Nothing) As Boolean   'Before Read = True, After Read =False

        'Coding Here
        'Before Read Working Slip check ===================================================================================================
        '------------------------------
        If BeAfRead Then
            'After Read Working Slip check ========================================================================================================
            '----------------------------
        Else

        End If

        Return True
    End Function


    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Lotstart(CType(CellConTag.LSMode, RunModeType))
        'RaiseEvent E_MakeAlarmCellCon("5555", "", "", "")

    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        LotEnd(EndModeType.Normal)
    End Sub

    Private Sub FicoAddLot(ByVal LotNo As String)
        Send_S2F49_EnhancedRemoteCommand("ADD_LOT", "LotID", LotNo)
    End Sub

    Private Sub UpdateLabelsText()  '170330 \783 Reload File Addition

        lbLotNo.Text = CellConTag.LotID
        lbPackage.Text = CellConTag.Package
        lbDevice.Text = CellConTag.DeviceName
        lbRecipe.Text = CellConTag.Recipe
        lbMcNo.Text = CellConTag.MCNo
        lbOPID.Text = CellConTag.OPID
        lbInputTotal.Text = CellConTag.INPUTQty
        lbStartTime.Text = CellConTag.LotStartTime
        lbEndTime.Text = CellConTag.LotEndTime
        'lbWaferLotNo.Text = CellConTag.WaferLotID
        'lbWaferNo.Text = CellConTag.CurrentWaferID

    End Sub





#End Region


#Region "SECS/GEM communication"


    Private Sub c_Host_HsmsStateChanged(ByVal sender As Object, ByVal e As HsmsStateChangedEventArgs)
        UpdateStateThreadSafe(e.State.ToString)
        If e.State = HsmsState.SELECTED And My.Settings.S1F13_Setting Then  '160627 \783 Eq comm revise
            Send_S1F13_EstablishCommunication()
            Exit Sub
        End If

    End Sub


    Private m_S6F11 As S6F11Delegate = New S6F11Delegate(AddressOf OnS6F11)

    Public Sub OnS6F11(ByVal request As S6F11) '160801 \783 Add parameter m_Equipment

        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_S6F11, request)
            Exit Sub
        End If

        request.ApplyStatusVariableValue(m_Equipment, m_DefinedReportDic)  'Macthing  S6F11 with  Define report for decode SVID

        'm_Equipment are object of SVIDs if usage by S6F11  , Please define property of equiptment object refer to SVID Name in equipment specification. 
        'Must set SVID value in S6F11 Class to m_Equipment object

        If My.Settings.EventReportEnable Then
            Select Case request.CEID ''Control Status
                'm_equipment are SVID of CEID that define in report.
                Case CStr(SecsID.LotStartCEID)
                    Lotstart(CType(CellConTag.LSMode, Rohm.Apcs.Tdc.RunModeType))

                Case CStr(SecsID.LotEndCEID)
                    CellConTag.NGbin1 = m_Equipment.NGbin1
                    CellConTag.NGbin2 = m_Equipment.NGbin2
                    CellConTag.NGbin3 = m_Equipment.NGbin3
                    CellConTag.NGbin4 = m_Equipment.NGbin4
                    CellConTag.NGbin5 = m_Equipment.NGbin5
                    CellConTag.NGbin6 = m_Equipment.NGbin6
                    CellConTag.GoodCat1 = m_Equipment.GoodCat1
                    CellConTag.GoodCat2 = m_Equipment.GoodCat2
                    LotEnd()
            End Select
        End If

        Select Case request.CEID ''Control Status
            'm_equipment are SVID of CEID that define in report.
        End Select

    End Sub

    Private Sub HostRequestDownLoadToEq(ByVal PPID As String) '160727 RecipeBodyManage
        'Dim msg As New S7F1
        'If File.Exists(RecipeDir & "\" & PPID & ".xml") = True Then
        '    OprData.PPIDMange = PPID
        '    Dim bFile As Byte() = ReadFromXmlPDE(RecipeDir & "\" & PPID & ".xml").PPBody     '160811 \783 recipe Program Definition Element
        '    msg.Setparameter(PPID, bFile.Length)
        '    RaiseEvent E_HostSend(msg)
        'Else
        '    RaiseEvent E_SlInfo("ไม่พบไฟล์ชื่อ " & PPID & "ใน Folder" & RecipeDir)
        'End If
    End Sub

    Public Sub Send_S7F5_HostRequestUplaodToHost(ByVal ppId As String)   '160727 RecipeBodyManage
        Dim msg As New S7F5(ppId)
        c_Host.Send(msg)
    End Sub

    Private Sub WaferMapDownLoadToEq(ByVal S12F3R As S12F3)  'Request by Eq download to Eq.

        'Dim WaferIndex As Integer
        'Dim res As New MapData

        'If S12F3R.MID.Length <> 7 Then   'Check format 9999-99
        '    MakeAlarmCellCon("Wafer No. format Err(9999-99) : " & S12F3R.MID, "Machine Ring WaferNo. Read")
        '    Dim S12F19x As New S12F19(MAPER.FormatError, 0)
        '    RaiseEvent E_HostReply(S12F3R, S12F19x)
        '    Exit Sub
        'End If

        'If Not Directory.Exists(WaferMapDir & "\" & OprData.WaferLotID) Then   'Check target Map file 
        '    MakeAlarmCellCon("ไม่พบ Directory " & WaferMapDir & "\" & OprData.WaferLotID)
        '    Dim S12F19x As New S12F19(MAPER.IDNoFound, 0)
        '    RaiseEvent E_HostReply(S12F3R, S12F19x)
        '    Exit Sub
        'End If

        'If Not IsNumeric(Microsoft.VisualBasic.Right(S12F3R.MID, 2)) Then     'Check wafer no. is numberic ?
        '    MakeAlarmCellCon("Wafer No. not found : " & S12F3R.MID, "Ring WaferNo. Read")
        '    Dim S12F19x As New S12F19(MAPER.IDNoFound, 0)
        '    RaiseEvent E_HostReply(S12F3R, S12F19x)
        '    Exit Sub
        'End If

        'If Not OprData.WaferLotID Like "*" & Microsoft.VisualBasic.Left(S12F3R.MID, 4) Then 'Check Read ID and Work Slip ID
        '    MakeAlarmCellCon("Wafer No (" & S12F3R.MID & ").ไม่ตรงกับข้อมูลใน Working Slip WaferID (" & OprData.WaferLotID & ")", "Ring WaferNo. Read")
        '    Dim S12F19x As New S12F19(MAPER.IDNoFound, 0)
        '    RaiseEvent E_HostReply(S12F3R, S12F19x)
        '    Exit Sub

        'End If

        'WaferIndex = CInt(Microsoft.VisualBasic.Right(S12F3R.MID, 2))
        'WaferMapData.Clear()

        'res = RohmMapConvert.Read(WaferMapDir & "\" & OprData.WaferLotID, S12F3R.FNLOC, S12F3R.NULBC, S12F3R.BCEQU, "M", WaferIndex)

        ''lbWaferNo.Text = S12F3R.MID
        'OprData.WaferID = S12F3R.MID

        'If (CellConTag.WaferID.Exists(Function(x) x = OprData.WaferID)) Then     'if exist remove  and new add
        '    CellConTag.WaferID.Remove(OprData.WaferID)
        '    CellConTag.WaferID.Add(OprData.WaferID)
        'Else
        '    CellConTag.WaferID.Add(OprData.WaferID)
        'End If

        'Dim S12F4 As New S12F4
        'Dim RefpList As New List(Of Point)
        'RefpList.Add(res.REFP)
        'S12F4.SetS12F4_Esec(S12F3R.MID, S12F3R.IDTYP, S12F3R.FNLOC, S12F3R.ORLOC, RefpList, CUShort(res.ROWCT), CUShort(res.COLCT), res.PRDCT, S12F3R.BCEQU, S12F3R.NULBC)
        'RaiseEvent E_HostReply(S12F3R, S12F4)
        'WaferMapData = res.BINLT
        'RaiseEvent E_SlInfo("Host sends Map set up data")

    End Sub



    Public Sub Send_S2F33_DeleteAllReport()
        Dim msg As S2F33 = New S2F33(CStr(0))
        msg.SetDeleteAllReport()
        c_Host.Send(msg)
    End Sub

    Public Sub Send_S2F33_DefineReport()

        Dim msg As S2F33 = New S2F33(CStr(0))

        For Each rpt As SecsDataType In m_DefinedReportDic.Values   '160722 \783  common secs data type
            msg.AddReport(rpt.RPTID(0), rpt.VID.ToArray())
        Next

        c_Host.Send(msg)

    End Sub

    Public Sub Send_S2F35_LinkReport()
        Dim msg As S2F35 = New S2F35(CStr(0))
        For Each lr As SecsDataType In m_LinkedReportDic.Values

            msg.AddLink(lr.CEID, lr.RPTID.ToArray())

        Next
        c_Host.Send(msg)
    End Sub

    Public Sub Send_S2F37_EnableAllReport()
        Dim msg As S2F37 = New S2F37()
        msg.SetEnable()
        c_Host.Send(msg)
    End Sub

    Public Sub Send_S1F13_EstablishCommunication()
        If Not My.Computer.Network.IsAvailable Then                '160628 \783 Eq comm revise
            FormMain.GetInstance.ShowStatusBar("PC Nework point unplug" & Format(Now, " |HH:mm:ss.fff"))
            Exit Sub
        End If
        If Not My.Computer.Network.Ping("10.1.1.50") Then            '160628 \783 Eq comm revise  Can Ping if Computer Connect only
            MsgBox("Ping to 10.1.1.50 fail")
        End If

        If CommuniationState = "NOT_CONNECTED" Then     '160628 \783 Eq comm revise
            FormMain.GetInstance.ShowStatusBar("HSMS Data Message Activity can not do now.  Now Comm State is " & CommuniationState & Format(Now, " |HH:mm:ss.fff"))
            Exit Sub
        End If
        If CommuniationState = "NOT_SELECTED" Then     '160627 \783 Eq comm revise
            FormMain.GetInstance.ShowStatusBar("HSMS Data Message Activity can not do now.  Now Comm State is " & CommuniationState & Format(Now, " |HH:mm:ss.fff"))
            Exit Sub
        End If


        Dim msg As S1F13 = New S1F13()
        c_Host.Send(msg)
        UpdateStateThreadSafe("NOT COMMUNICATING")
    End Sub

    Public Sub ConvertAndSendFromHexString(ByVal strHex As String)

        If strHex = "" Then
            Exit Sub
        End If

        Dim byteArray As Byte() = ConvertStringToByte(strHex)
        Dim msg As SecsMessageBase = c_Host.MessageParser.ToSecsMessage(byteArray)
        c_Host.Send(msg)

    End Sub

    ''' <summary>
    ''' S1F17 Request ON-LINE
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Send_S1F17_OnlineRequest()
        Dim msg As SecsMessage = New SecsMessage(1, 17, True)
        c_Host.Send(msg)
    End Sub

    ''' <summary>
    ''' S1F15R	Request OFF-LINE
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Send_S1F15_OfflineRequest()
        Dim msg As SecsMessage = New SecsMessage(1, 15, True)
        c_Host.Send(msg)
    End Sub

    ''' <summary>
    ''' S2F41 Host Command Send (HCS) [PP-SELECT]
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Send_S2F41_RemoteCommand(ByVal RCmd As String, Optional ByVal CmdPName As String = "", Optional ByVal CmdPVal As String = "")
        Dim cmd As S2F41 = New S2F41()
        cmd.RemoteCommand = RCmd
        If CmdPName <> "" And CmdPVal <> "" Then
            cmd.AddVariable(CmdPName, CmdPVal)
        End If
        c_Host.Send(cmd)
    End Sub

    ''' <summary>
    ''' S2F49	Enhanced Remote Command
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Send_S2F49_EnhancedRemoteCommand(ByVal RCmd As String, Optional ByVal CmdPName As String = "", Optional ByVal CmdPVal As String = "")
        Dim cmd As S2F49 = New S2F49(CStr(1))
        cmd.RemoteCommand = RCmd
        If CmdPName <> "" And CmdPVal <> "" Then
            cmd.AddVariable(CmdPName, CmdPVal)
        End If
        c_Host.Send(cmd)
    End Sub

    Public Sub Send_S2F13(ByVal U32List As List(Of UInt32))

        Dim msg As S2F13 = New S2F13()
        For Each U32 As UInt32 In U32List
            msg.AddEcid(U32)
        Next
        c_Host.Send(msg)

    End Sub

    Public Sub Send_S5F3_EnableAllAlarm()
        Dim msg As New S5F3(True, 0)
        c_Host.Send(msg)
    End Sub

    Public Sub Send_S2F15(ByVal U32 As UInt32, ByVal Ev As String, ByVal Format As SecsFormat)
        Dim msg As S2F15 = New S2F15()
        msg.AddListEcid(U32, Ev, Format)
        c_Host.Send(msg)

    End Sub

    Public Sub Send_S5F3(ByVal Enable As Boolean, Optional ByVal ALID As UInteger = Nothing)
        Dim msg As S5F3 = New S5F3(Enable, ALID)
        c_Host.Send(msg)
    End Sub

    Public Sub Send_S10F3(ByVal ID As Byte, ByVal msgText As String)
        Dim msg As S10F3 = New S10F3(ID, msgText)
        c_Host.Send(msg)
    End Sub

    Public Sub Send_S1F1()
        Dim msg As S1F1 = New S1F1()
        c_Host.Send(msg)
    End Sub



    Private Sub Reply_S1F13(ByVal request As SecsMessageBase)
        'm_Equipment.DeviceId = request.DeviceId
        'reply
        Dim reply As S1F14 = New S1F14()
        c_Host.Reply(request, reply)
        UpdateStateThreadSafe("COMMUNICATING (Equip Init)")
    End Sub

    Public Sub Perform_S6F11(ByVal request As S6F11)

        'reply(acknowledge)
        Dim s6f12 As S6F12 = New S6F12()
        c_Host.Reply(request, s6f12)
        'request.ApplyStatusVariableValue(m_Equipment, m_DefinedReportDic)

        'Try

        '    Select Case request.CEID

        '        ''Case 1001 'Control Status
        '        ''    UpdateStateThreadSafe(m_Equipment.ControlState.ToString, StatusLabel.FrmSecs_slbControlState)
        '        '    ''Case 1002 'EQ Status           
        '        '    ''    OnEQStatusChanged()
        '        '    ''Case 1003 'Lot Start(End)
        '        '    ''    OnLotStartEnd()
        '        '    ''Case 1102 'Loader End             
        '        '    ''    OnLoaderEnd()
        '        '    ''    'USELESS EVENTS
        '        '    ''    'Case 1004 'ppid change
        '        '    ''    'Case 1100 'Door Locked
        '        '    ''    'Case 1101 'Tube Status
        '        Case Else


        '    End Select

        'Catch ex As Exception
        '    SaveCatchLog(ex.ToString, "Perform_S6F11()")

        'End Try

        'If OprData.FRMProductAlive Then
        '    FrmProduct.OnS6F11(request) '160801 \783 Add parameter m_Equipment
        'End If
    End Sub


    Private m_Slb As UpdateTextDelegate1 = New UpdateTextDelegate1(AddressOf UpdateStateThreadSafe)

    Private Sub UpdateStateThreadSafe(ByVal informationText As String)
        If Me.InvokeRequired Then
            'http://kristofverbiest.blogspot.com/2007/02/avoid-invoke-prefer-begininvoke.html
            Me.BeginInvoke(m_Slb, informationText)
            Exit Sub
        End If
        Try
            'If ControlName = StatusLabel.FrmSecs_slblCnnState Then
            '    CommuniationState = informationText
            '    If FrmProduct IsNot Nothing Then

            If informationText Like "COMMUNICATING*" Then   '160627 \783 Eq Comm Revise
                Me.BackColor = Color.WhiteSmoke
                FormMain.GetInstance.ShowStatusBar("SECS/GEM COMMUNICATION : " & informationText)
            Else
                Me.BackColor = Color.Red
                FormMain.GetInstance.ShowStatusBar("SECS/GEM COMMUNICATION : " & informationText)

            End If

            '    End If

            'End If
            'If ControlName = StatusLabel.FrmSecs_slbControlState Then
            '    ControlState = informationText
            'End If
            'If FrmSecs Is Nothing Then
            '    Exit Sub
            'End If
            'Select Case ControlName
            '    Case StatusLabel.FrmSecs_slblCnnState
            '        FrmSecs.slblCnnState.Text = informationText
            '    Case StatusLabel.FrmSecs_sblStatus
            '        slMessage.Text = informationText
            '        'FrmSecs.slbStatus.Text = informationText    '170110 \783 Nofication only main
            '    Case StatusLabel.FrmSecs_slbControlState
            '        FrmSecs.slbControlState.Text = informationText
            '    Case StatusLabel.FrmSecs_lbS2F44
            '        FrmSecs.lbS2F44.Text = informationText
            '    Case StatusLabel.FrmSecs_lbSpool
            '        FrmSecs.lblSpool.Text = informationText
            'End Select
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "UpdateStateThreadSafe")
        End Try


    End Sub
#End Region



    Public Sub OnDbDataReceived(data As DBData)

    End Sub

    Private Sub Perform_S10(ByVal msg As SecsMessageBase)
        Select Case msg.Function
            Case 1
                Dim s10f2 As S10F2 = New S10F2(ACKC10.Accepted)
                c_Host.Reply(msg, s10f2)
                Dim CommandS10F1 As New S10F1
                CommandS10F1 = CType(msg, S10F1)
                'S10F1ThreadSafe("Message : " & CommandS10F1.Text & "   Terminal ID : " & CommandS10F1.TIDx)
                'S10F1ThreadSafe(CommandS10F1.Text)
        End Select

    End Sub

    Sub ShowFormManageSecsHost()
        If c_FormManageHsmsHost Is Nothing OrElse c_FormManageHsmsHost.IsDisposed Then
            c_FormManageHsmsHost = New FormManageHsmsHost(Me)
            c_FormManageHsmsHost.Text = "SECS/GEM of " & c_McNo
        End If
        c_FormManageHsmsHost.Show()
        c_FormManageHsmsHost.Select()
    End Sub

    Private Sub FormProduction_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If e.CloseReason = CloseReason.MdiFormClosing Then
            c_Host.Disconnect()

            If Not c_TabCtrl.HasChildren Then
                c_TabPage.Dispose()
                c_TabCtrl.Visible = False
            End If
        Else
            e.Cancel = True
            Me.Hide()
        End If
    End Sub

    Sub StartHsmsHost()
        c_Host.Connect()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        CellConTag.TotalGoodPcs += 1
        lbGoodTotal.Text = CStr(CellConTag.TotalGoodPcs)
        FormMain.GetInstance.c_ServiceProxy.DiebondCounterUpdate(lbMcNo.Text, CellConTag.TotalGoodPcs, CellConTag.TotalNGPcs)
    End Sub


    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        CellConTag.TotalNGPcs += 1
        lbNGTotal.Text = CStr(CellConTag.TotalNGPcs)
        FormMain.GetInstance.c_ServiceProxy.DiebondCounterUpdate(lbMcNo.Text, CellConTag.TotalGoodPcs, CellConTag.TotalNGPcs)
    End Sub
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        FormMain.GetInstance.c_ServiceProxy.AlarmReport(lbMcNo.Text, True, "001", "TestAlarmSet")
    End Sub
    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        FormMain.GetInstance.c_ServiceProxy.AlarmReport(lbMcNo.Text, False, "001", "TestAlarmSet")
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim sta As New m_ProcessingStates
        sta = DirectCast(ComboBox1.SelectedIndex, m_ProcessingStates)
        FormMain.GetInstance.c_ServiceProxy.ProcessingState(lbMcNo.Text, sta, Now)
    End Sub

#Region "Property"
    Private c_Host As HsmsHost
    Public ReadOnly Property Host As HsmsHost
        Get
            Return c_Host
        End Get
    End Property

    Private c_McNo As String
    Public ReadOnly Property MCNo As String
        Get
            Return c_McNo
        End Get
    End Property
    Private c_IP As String
    Public ReadOnly Property IP As String
        Get
            Return c_IP
        End Get
    End Property
    Private c_TabCtrl As TabControl
    Public Property TabCtrl() As TabControl
        Get
            Return c_TabCtrl
        End Get
        Set(ByVal value As TabControl)
            c_TabCtrl = value
        End Set
    End Property

    Private c_TabPage As TabPage
    Public Property TabPage() As TabPage
        Get
            Return c_TabPage
        End Get
        Set(ByVal value As TabPage)
            c_TabPage = value
        End Set
    End Property








#End Region





  
    
  
End Class