
Public Class FormManageUser

    Private Sub LoginUser_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        'UserTable.TableName = "Userlogin"
        'UserTable.WriteXmlSchema(My.Application.Info.DirectoryPath & "\UserLoginSchema.xml")
        'UserTable.WriteXml(My.Application.Info.DirectoryPath & "\UserLogin.xml")

    End Sub


    Private Sub LoginUser_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
   
        ComboBoxUserLevel.DataSource = [Enum].GetValues(GetType(User.UserLevel))    'Load from common Enum 160624 \783
        ComboBoxUserLevel.SelectedIndex = 0
        DataGridViewUser.DataSource = User.GetAll()


    End Sub

    Private Sub ButtonAddUser_Click(sender As System.Object, e As System.EventArgs) Handles ButtonAddUser.Click
        If TextBoxUserName.Text = "" Then
            Exit Sub
        End If

        If TextBoxPassword.Text = "" Then
            Exit Sub
        End If

        Try
            User.AddUser(TextBoxUserName.Text, TextBoxPassword.Text, CType(ComboBoxUserLevel.SelectedValue, User.UserLevel))
            DataGridViewUser.DataSource = User.GetAll()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub


    Private Sub ButtonRemoveUser_Click(sender As System.Object, e As System.EventArgs) Handles ButtonRemoveUser.Click

        If DataGridViewUser.SelectedRows.Count > 0 Then
            Dim selectedRow As DataGridViewRow = DataGridViewUser.SelectedRows(0)
            Dim u As User = CType(selectedRow.DataBoundItem, User)
            User.RemoveUser(u.UserName)
            DataGridViewUser.Rows.RemoveAt(selectedRow.Index)
        End If

    End Sub


End Class