<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormProduction
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormProduction))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.AndonToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ByAutoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ByManualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BMRequestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PMRepairToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.APCSStaffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaximizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductTableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SecsConsoleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EqConnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbMcNo = New System.Windows.Forms.Label()
        Me.lbOPID = New System.Windows.Forms.Label()
        Me.lbLotNo = New System.Windows.Forms.Label()
        Me.lbPackage = New System.Windows.Forms.Label()
        Me.lbDevice = New System.Windows.Forms.Label()
        Me.lbRecipe = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lbSpMode = New System.Windows.Forms.Label()
        Me.lbStartTime = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lbEndTime = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lbGoodTotal = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbNGTotal = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.lbInputTotal = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.pbxAutoM = New System.Windows.Forms.PictureBox()
        Me.pbxLogo = New System.Windows.Forms.PictureBox()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.pbxAutoM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.Transparent
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AndonToolStripMenuItem, Me.BMRequestToolStripMenuItem, Me.PMRepairToolStripMenuItem, Me.WorkRecordToolStripMenuItem, Me.APCSStaffToolStripMenuItem, Me.WindowToolStripMenuItem, Me.EqConnectToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(1, 98)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(544, 25)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AndonToolStripMenuItem
        '
        Me.AndonToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ByAutoToolStripMenuItem, Me.ByManualToolStripMenuItem})
        Me.AndonToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AndonToolStripMenuItem.Name = "AndonToolStripMenuItem"
        Me.AndonToolStripMenuItem.Size = New System.Drawing.Size(58, 21)
        Me.AndonToolStripMenuItem.Text = "Andon"
        '
        'ByAutoToolStripMenuItem
        '
        Me.ByAutoToolStripMenuItem.Name = "ByAutoToolStripMenuItem"
        Me.ByAutoToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.ByAutoToolStripMenuItem.Text = "By Auto"
        '
        'ByManualToolStripMenuItem
        '
        Me.ByManualToolStripMenuItem.Name = "ByManualToolStripMenuItem"
        Me.ByManualToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.ByManualToolStripMenuItem.Text = "By Manual"
        '
        'BMRequestToolStripMenuItem
        '
        Me.BMRequestToolStripMenuItem.Name = "BMRequestToolStripMenuItem"
        Me.BMRequestToolStripMenuItem.Size = New System.Drawing.Size(82, 21)
        Me.BMRequestToolStripMenuItem.Text = "BM Request"
        '
        'PMRepairToolStripMenuItem
        '
        Me.PMRepairToolStripMenuItem.Name = "PMRepairToolStripMenuItem"
        Me.PMRepairToolStripMenuItem.Size = New System.Drawing.Size(90, 21)
        Me.PMRepairToolStripMenuItem.Text = "PM Repairing"
        '
        'WorkRecordToolStripMenuItem
        '
        Me.WorkRecordToolStripMenuItem.Name = "WorkRecordToolStripMenuItem"
        Me.WorkRecordToolStripMenuItem.Size = New System.Drawing.Size(84, 21)
        Me.WorkRecordToolStripMenuItem.Text = "WorkRecord"
        '
        'APCSStaffToolStripMenuItem
        '
        Me.APCSStaffToolStripMenuItem.Name = "APCSStaffToolStripMenuItem"
        Me.APCSStaffToolStripMenuItem.Size = New System.Drawing.Size(77, 21)
        Me.APCSStaffToolStripMenuItem.Text = "APCS_Staff"
        '
        'WindowToolStripMenuItem
        '
        Me.WindowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MaximizeToolStripMenuItem, Me.ProductTableToolStripMenuItem, Me.SecsConsoleToolStripMenuItem})
        Me.WindowToolStripMenuItem.Name = "WindowToolStripMenuItem"
        Me.WindowToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.WindowToolStripMenuItem.Text = "Windows"
        '
        'MaximizeToolStripMenuItem
        '
        Me.MaximizeToolStripMenuItem.Name = "MaximizeToolStripMenuItem"
        Me.MaximizeToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.MaximizeToolStripMenuItem.Text = "Maximize"
        '
        'ProductTableToolStripMenuItem
        '
        Me.ProductTableToolStripMenuItem.Name = "ProductTableToolStripMenuItem"
        Me.ProductTableToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.ProductTableToolStripMenuItem.Text = "ProductTable"
        '
        'SecsConsoleToolStripMenuItem
        '
        Me.SecsConsoleToolStripMenuItem.Name = "SecsConsoleToolStripMenuItem"
        Me.SecsConsoleToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.SecsConsoleToolStripMenuItem.Text = "SecsConsole"
        '
        'EqConnectToolStripMenuItem
        '
        Me.EqConnectToolStripMenuItem.Name = "EqConnectToolStripMenuItem"
        Me.EqConnectToolStripMenuItem.Size = New System.Drawing.Size(77, 21)
        Me.EqConnectToolStripMenuItem.Text = "EqConnect"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 25)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "MC No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 24)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "OP ID"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(164, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 25)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Lot No."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(164, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 21)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Package"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(164, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 22)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Device"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(399, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 20)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "StartTime"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbMcNo
        '
        Me.lbMcNo.BackColor = System.Drawing.Color.Transparent
        Me.lbMcNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbMcNo.ForeColor = System.Drawing.Color.Blue
        Me.lbMcNo.Location = New System.Drawing.Point(75, 3)
        Me.lbMcNo.Name = "lbMcNo"
        Me.lbMcNo.Size = New System.Drawing.Size(80, 20)
        Me.lbMcNo.TabIndex = 51
        Me.lbMcNo.Text = "Label7"
        Me.lbMcNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbOPID
        '
        Me.lbOPID.BackColor = System.Drawing.Color.Transparent
        Me.lbOPID.Location = New System.Drawing.Point(75, 31)
        Me.lbOPID.Name = "lbOPID"
        Me.lbOPID.Size = New System.Drawing.Size(80, 20)
        Me.lbOPID.TabIndex = 51
        Me.lbOPID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbLotNo
        '
        Me.lbLotNo.BackColor = System.Drawing.Color.Transparent
        Me.lbLotNo.Location = New System.Drawing.Point(244, 3)
        Me.lbLotNo.Name = "lbLotNo"
        Me.lbLotNo.Size = New System.Drawing.Size(123, 25)
        Me.lbLotNo.TabIndex = 51
        Me.lbLotNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbPackage
        '
        Me.lbPackage.BackColor = System.Drawing.Color.Transparent
        Me.lbPackage.Location = New System.Drawing.Point(244, 31)
        Me.lbPackage.Name = "lbPackage"
        Me.lbPackage.Size = New System.Drawing.Size(146, 25)
        Me.lbPackage.TabIndex = 51
        Me.lbPackage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbDevice
        '
        Me.lbDevice.BackColor = System.Drawing.Color.Transparent
        Me.lbDevice.Location = New System.Drawing.Point(244, 59)
        Me.lbDevice.Name = "lbDevice"
        Me.lbDevice.Size = New System.Drawing.Size(137, 25)
        Me.lbDevice.TabIndex = 51
        Me.lbDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbRecipe
        '
        Me.lbRecipe.BackColor = System.Drawing.Color.Transparent
        Me.lbRecipe.Location = New System.Drawing.Point(75, 59)
        Me.lbRecipe.Name = "lbRecipe"
        Me.lbRecipe.Size = New System.Drawing.Size(80, 20)
        Me.lbRecipe.TabIndex = 51
        Me.lbRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 347)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(0, 13)
        Me.Label12.TabIndex = 50
        '
        'lbSpMode
        '
        Me.lbSpMode.BackColor = System.Drawing.Color.Transparent
        Me.lbSpMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbSpMode.ForeColor = System.Drawing.Color.Red
        Me.lbSpMode.Location = New System.Drawing.Point(470, 59)
        Me.lbSpMode.Name = "lbSpMode"
        Me.lbSpMode.Size = New System.Drawing.Size(126, 25)
        Me.lbSpMode.TabIndex = 53
        Me.lbSpMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbStartTime
        '
        Me.lbStartTime.BackColor = System.Drawing.Color.Transparent
        Me.lbStartTime.Location = New System.Drawing.Point(470, 3)
        Me.lbStartTime.Name = "lbStartTime"
        Me.lbStartTime.Size = New System.Drawing.Size(126, 25)
        Me.lbStartTime.TabIndex = 51
        Me.lbStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 59)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 22)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Recipe"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbEndTime
        '
        Me.lbEndTime.BackColor = System.Drawing.Color.Transparent
        Me.lbEndTime.Location = New System.Drawing.Point(470, 31)
        Me.lbEndTime.Name = "lbEndTime"
        Me.lbEndTime.Size = New System.Drawing.Size(126, 24)
        Me.lbEndTime.TabIndex = 51
        Me.lbEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(399, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 23)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "EndTime"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbGoodTotal
        '
        Me.lbGoodTotal.BackColor = System.Drawing.Color.Transparent
        Me.lbGoodTotal.Location = New System.Drawing.Point(686, 31)
        Me.lbGoodTotal.Name = "lbGoodTotal"
        Me.lbGoodTotal.Size = New System.Drawing.Size(63, 25)
        Me.lbGoodTotal.TabIndex = 51
        Me.lbGoodTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(605, 31)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 23)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "GoodTotal"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbNGTotal
        '
        Me.lbNGTotal.BackColor = System.Drawing.Color.Transparent
        Me.lbNGTotal.Location = New System.Drawing.Point(686, 59)
        Me.lbNGTotal.Name = "lbNGTotal"
        Me.lbNGTotal.Size = New System.Drawing.Size(63, 25)
        Me.lbNGTotal.TabIndex = 51
        Me.lbNGTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(605, 59)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(66, 22)
        Me.Label14.TabIndex = 55
        Me.Label14.Text = "NGTotal"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'lbInputTotal
        '
        Me.lbInputTotal.BackColor = System.Drawing.Color.Transparent
        Me.lbInputTotal.Location = New System.Drawing.Point(686, 3)
        Me.lbInputTotal.Name = "lbInputTotal"
        Me.lbInputTotal.Size = New System.Drawing.Size(63, 23)
        Me.lbInputTotal.TabIndex = 51
        Me.lbInputTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(605, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 23)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "InputTotal"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(133, 153)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(112, 51)
        Me.Button4.TabIndex = 54
        Me.Button4.Text = "LS"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(251, 153)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(112, 51)
        Me.Button5.TabIndex = 54
        Me.Button5.Text = "LE1"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'pbxAutoM
        '
        Me.pbxAutoM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbxAutoM.BackColor = System.Drawing.Color.White
        Me.pbxAutoM.BackgroundImage = Global.CellController.My.Resources.Resources.HR
        Me.pbxAutoM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbxAutoM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pbxAutoM.Location = New System.Drawing.Point(1066, 12)
        Me.pbxAutoM.Name = "pbxAutoM"
        Me.pbxAutoM.Size = New System.Drawing.Size(94, 65)
        Me.pbxAutoM.TabIndex = 10
        Me.pbxAutoM.TabStop = False
        Me.pbxAutoM.Visible = False
        '
        'pbxLogo
        '
        Me.pbxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbxLogo.BackgroundImage = CType(resources.GetObject("pbxLogo.BackgroundImage"), System.Drawing.Image)
        Me.pbxLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbxLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pbxLogo.Location = New System.Drawing.Point(1175, 12)
        Me.pbxLogo.Name = "pbxLogo"
        Me.pbxLogo.Size = New System.Drawing.Size(96, 65)
        Me.pbxLogo.TabIndex = 2
        Me.pbxLogo.TabStop = False
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(15, 153)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(112, 51)
        Me.Button8.TabIndex = 122
        Me.Button8.Text = "LR"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble
        Me.TableLayoutPanel1.ColumnCount = 8
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 365.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbMcNo, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbOPID, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label14, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbSpMode, 5, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label11, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbNGTotal, 7, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbInputTotal, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbGoodTotal, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbLotNo, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbPackage, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbDevice, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbRecipe, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbEndTime, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbStartTime, 5, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(15, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1051, 87)
        Me.TableLayoutPanel1.TabIndex = 123
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(133, 210)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 51)
        Me.Button3.TabIndex = 125
        Me.Button3.Text = "NgPcsCountUp"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(15, 210)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 51)
        Me.Button2.TabIndex = 124
        Me.Button2.Text = "GoodPcsCountUp"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(251, 210)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 51)
        Me.Button1.TabIndex = 126
        Me.Button1.Text = "AlarmSet"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(369, 210)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(112, 51)
        Me.Button6.TabIndex = 126
        Me.Button6.Text = "AlarmClear"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(487, 210)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(112, 51)
        Me.Button7.TabIndex = 126
        Me.Button7.Text = "Status"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(487, 183)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(112, 21)
        Me.ComboBox1.TabIndex = 127
        '
        'FormProduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1300, 504)
        Me.ControlBox = False
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.pbxAutoM)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.pbxLogo)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FormProduction"
        Me.ShowIcon = False
        Me.Text = "ProcessForm"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.pbxAutoM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents AndonToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BMRequestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MaximizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductTableToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SecsConsoleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbMcNo As System.Windows.Forms.Label
    Friend WithEvents lbOPID As System.Windows.Forms.Label
    Friend WithEvents lbLotNo As System.Windows.Forms.Label
    Friend WithEvents lbPackage As System.Windows.Forms.Label
    Friend WithEvents lbDevice As System.Windows.Forms.Label
    Friend WithEvents lbRecipe As System.Windows.Forms.Label
    Friend WithEvents EqConnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lbSpMode As System.Windows.Forms.Label
    Friend WithEvents lbStartTime As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents PMRepairToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents APCSStaffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pbxAutoM As System.Windows.Forms.PictureBox
    Friend WithEvents lbEndTime As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents WorkRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ByAutoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ByManualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lbGoodTotal As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbNGTotal As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents lbInputTotal As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
