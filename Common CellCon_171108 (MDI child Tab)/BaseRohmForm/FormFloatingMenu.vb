Imports XtraLibrary.SecsGem

Public Class FormFloatingMenu

#Region "Single Instance"

    Private Shared c_Instance As FormFloatingMenu

    Public Shared Function GetInstance() As FormFloatingMenu
        If c_Instance Is Nothing OrElse c_Instance.IsDisposed Then
            c_Instance = New FormFloatingMenu()
        End If
        Return c_Instance
    End Function

#End Region

#Region "Commomn Define"

    'Event MeCLose()
    'Event ProductionClick()
    'Event ProdTableClick(ByVal tabpages As String)
    'Event SettingClick()
    'Event SecsGemClick()
    'Event TCPClientClick()
    'Event OpenComForm()
    'Event E_SlInfo(ByVal msg As String)             '160624 AddMsg to MDI  \783

#End Region

    Private c_OskProcess As Process

    Private Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnLogin_Click(sender As System.Object, e As System.EventArgs) _
        Handles btnLogin.Click, PictureBox4.Click
        Me.Hide()
        Using frm As New FormLogin
            If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                btnLogin.Text = User.CurrentUser.Level.ToString
                FormMain.GetInstance().ToolStripLabelMessage.Text = "Login successful :" & btnLogin.Text
            Else
                btnLogin.Text = User.CurrentUser.Level.ToString
                FormMain.GetInstance().ToolStripLabelMessage.Text = "Login Fail :" & btnLogin.Text
            End If

        End Using
    End Sub

    Private Sub ButtonCloseApplication_Click(sender As System.Object, e As System.EventArgs) Handles ButtonCloseApplication.Click
        FormMain.GetInstance().Close()
    End Sub

   


    Private Sub Form1_Deactivate(sender As Object, e As System.EventArgs) Handles Me.Deactivate

        Timer1.Enabled = False
        Timer1.Enabled = True
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Me.WindowState = FormWindowState.Minimized  '160624 Fix Display

        Timer1.Stop()
        Timer1.Enabled = False
    End Sub

    Private Sub btnSetting_Click(sender As System.Object, e As System.EventArgs) _
        Handles btnSetting.Click, pbSetting.Click

        If User.CurrentUser.Level <> User.UserLevel.ADMIN Then
            Exit Sub
        End If

        Me.Hide()

        Using frm As FormSetting = New FormSetting()
            frm.ShowDialog()
        End Using

    End Sub


    Private Sub btnSecsGem_Click(sender As System.Object, e As System.EventArgs) _
        Handles btnSecsGem.Click, pbSecsGem.Click

        Me.Hide()

        Dim frm As FormMain = FormMain.GetInstance()
        Dim frmProduction As FormProduction = frm.SelectedFormProduction
        frmProduction.ShowFormManageSecsHost()


    End Sub

    Private Sub pbxKeyBoard_Click(sender As System.Object, e As System.EventArgs) _
        Handles pbxKeyBoard.Click, btnKeyboard.Click
        KeyBoardOpen()
    End Sub


    Private Sub KeyBoardOpen()
        Try
            If Me.c_OskProcess Is Nothing OrElse Me.c_OskProcess.HasExited Then
                If Me.c_OskProcess IsNot Nothing AndAlso Me.c_OskProcess.HasExited Then
                    Me.c_OskProcess.Close()
                End If
                Me.c_OskProcess = Process.Start("C:\Windows\System32\OSK.EXE")

            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub pbxTCPClient_Click(sender As System.Object, e As System.EventArgs) _
        Handles pbxTCPClient.Click, btnTCPClient.Click

    End Sub

    Private Sub btnTDCLock_Click(sender As System.Object, e As System.EventArgs)

    End Sub


End Class
