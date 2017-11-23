<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormManageUser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridViewUser = New System.Windows.Forms.DataGridView()
        Me.ButtonAddUser = New System.Windows.Forms.Button()
        Me.ButtonRemoveUser = New System.Windows.Forms.Button()
        Me.TextBoxUserName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBoxUserLevel = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.DataGridViewUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewUser
        '
        Me.DataGridViewUser.AllowUserToAddRows = False
        Me.DataGridViewUser.AllowUserToDeleteRows = False
        Me.DataGridViewUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewUser.Location = New System.Drawing.Point(12, 12)
        Me.DataGridViewUser.Name = "DataGridViewUser"
        Me.DataGridViewUser.ReadOnly = True
        Me.DataGridViewUser.RowHeadersVisible = False
        Me.DataGridViewUser.Size = New System.Drawing.Size(304, 298)
        Me.DataGridViewUser.TabIndex = 0
        '
        'ButtonAddUser
        '
        Me.ButtonAddUser.Location = New System.Drawing.Point(369, 250)
        Me.ButtonAddUser.Name = "ButtonAddUser"
        Me.ButtonAddUser.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAddUser.TabIndex = 1
        Me.ButtonAddUser.Text = "Add"
        Me.ButtonAddUser.UseVisualStyleBackColor = True
        '
        'ButtonRemoveUser
        '
        Me.ButtonRemoveUser.Location = New System.Drawing.Point(369, 279)
        Me.ButtonRemoveUser.Name = "ButtonRemoveUser"
        Me.ButtonRemoveUser.Size = New System.Drawing.Size(75, 23)
        Me.ButtonRemoveUser.TabIndex = 2
        Me.ButtonRemoveUser.Text = "Remove"
        Me.ButtonRemoveUser.UseVisualStyleBackColor = True
        '
        'TextBoxUserName
        '
        Me.TextBoxUserName.Location = New System.Drawing.Point(322, 35)
        Me.TextBoxUserName.Name = "TextBoxUserName"
        Me.TextBoxUserName.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxUserName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(323, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "UserName"
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(322, 76)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxPassword.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(324, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password"
        '
        'ComboBoxUserLevel
        '
        Me.ComboBoxUserLevel.FormattingEnabled = True
        Me.ComboBoxUserLevel.Location = New System.Drawing.Point(323, 117)
        Me.ComboBoxUserLevel.Name = "ComboBoxUserLevel"
        Me.ComboBoxUserLevel.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxUserLevel.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(324, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "UserType"
        '
        'FormManageUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 322)
        Me.Controls.Add(Me.ComboBoxUserLevel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxPassword)
        Me.Controls.Add(Me.TextBoxUserName)
        Me.Controls.Add(Me.ButtonRemoveUser)
        Me.Controls.Add(Me.ButtonAddUser)
        Me.Controls.Add(Me.DataGridViewUser)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormManageUser"
        Me.Text = "LoginUser"
        CType(Me.DataGridViewUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridViewUser As System.Windows.Forms.DataGridView
    Friend WithEvents ButtonAddUser As System.Windows.Forms.Button
    Friend WithEvents ButtonRemoveUser As System.Windows.Forms.Button
    Friend WithEvents TextBoxUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxUserLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
