'Option Strict Off                             '160722 \783  common secs data type
Imports XtraLibrary.SecsGem

Public Class S6F11
    Inherits SecsMessageBase

    Private m_L3 As SecsItemList
    'Private m_DATAID As SecsItemU4
    Private m_CEID As New SecsDataType
    Private m_CEIDU4 As SecsItemU4
    Private m_CEIDU2 As SecsItemU2
    Private m_CEIDU1 As SecsItemU1
    Private m_La As SecsItemList


    Public Sub New()
        MyBase.New(6, 11, True)

        m_L3 = New SecsItemList("L3")
        Me.AddItem(m_L3)
        Dim SecsData As New SecsDataType            '160722 \783  common secs data type
        'If SecsData.DATAID.GetType.Name = "UInt32" Then
        '    Dim m_DataId As New SecsItemU4("DATAID")
        '    m_L3.AddItem(m_DataId)
        'ElseIf SecsData.DATAID.GetType.Name = "UInt16" Then
        '    Dim m_DataId As New SecsItemU2("DATAID")
        '    m_L3.AddItem(m_DataId)
        'ElseIf SecsData.DATAID.GetType.Name = "Byte" Then
        '    Dim m_DataId As New SecsItemU1("DATAID")
        '    m_L3.AddItem(m_DataId)
        'End If

        'm_DATAID = New SecsItemU4("DATAID")
        'm_L3.AddItem(m_DATAID)


        If SecsData.DATAID = SecsDataType.U4_1.U4 Then
            Dim m_DataId As New SecsItemU4("DATAID")
            m_L3.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.U4_1.U2 Then
            Dim m_DataId As New SecsItemU2("DATAID")
            m_L3.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.U4_1.U1 Then
            Dim m_DataId As New SecsItemU1("DATAID")
            m_L3.AddItem(m_DataId)
        Else
            Dim m_DataId As New SecsItemU2("DATAID")
            m_L3.AddItem(m_DataId)
        End If



        If m_CEID.CEID.GetType.Name = "UInt32" Then
            m_CEIDU4 = New SecsItemU4("CEID")
            m_L3.AddItem(m_CEIDU4)
        ElseIf m_CEID.CEID.GetType.Name = "UInt16" Then
            m_CEIDU2 = New SecsItemU2("CEID")           's6f11 will throw error if type not U2
            m_L3.AddItem(m_CEIDU2)

            'ElseIf m_CEID.CEID.GetType.Name = "Byte" Then
            '    m_CEIDU1 = New SecsItemU1("CEID")           's6f11 will throw error if type not U2
            '    m_L3.AddItem(m_CEIDU1)
        Else
            m_CEIDU4 = New SecsItemU4("CEID")
            m_L3.AddItem(m_CEIDU4)
        End If

        'La contains L2 and L2 contain "Report "
        'and "List of SV value mapped to Defined report"
        m_La = New SecsItemList("La")
        m_L3.AddItem(m_La)

    End Sub

    Public ReadOnly Property CEID() As String
        Get
            Dim Value As String


            If m_CEID.CEID.GetType.Name = "UInt16" Then
                Value = m_CEIDU2.Value(0).ToString

                'ElseIf m_CEID.CEID.GetType.Name = "Byte" Then
                '    Value = m_CEIDU1.Value(0).ToString

            Else
                Value = m_CEIDU4.Value(0).ToString
            End If

            Return Value
        End Get
    End Property

    '1
    Public Sub ApplyStatusVariableValue(ByVal eq As Equipment, ByVal reportDic As Dictionary(Of String, SecsDataType)) '160722 \783  common secs data type

        Dim secsItem_Lb As SecsItemList
        Dim reportID As String = ""
        Dim definedReport As New SecsDataType
        Dim secsItem_V As SecsItem
        Try

            For Each l2 As SecsItemList In m_La.Value

                'reportID = l2.Value(0).Value(0)         ' if Option Strict Off 
                If definedReport.RPTID.GetType.GetGenericArguments()(0).Name = "UInt32" Then
                    reportID = CType(l2.Value(0), SecsItemU4).Value(0).ToString
                Else
                    reportID = CType(l2.Value(0), SecsItemU2).Value(0).ToString
                End If


                If reportDic.ContainsKey(reportID) Then
                    'get DefinedReport object from dictionary
                    definedReport = reportDic(reportID)

                    secsItem_Lb = CType(l2.Value(1), SecsItemList)
                    For i As Integer = 0 To secsItem_Lb.Value.Count - 1

                        secsItem_V = secsItem_Lb.Value(i)

                        If My.Settings.EventReportEnable Then  '170105 \783 \Config SECSGEM ID
                            Select Case definedReport.VID(i).ToString()

                                Case SecsID.LotIDSVID
                                    eq.LotNumber = CType(secsItem_V.Value, String)
                                Case SecsID.GoodCat1SVID
                                    'eq.GoodCat1 = CType(secsItem_V.Value, String)
                                Case SecsID.GoodCat2SVID
                                    'eq.GoodCat2 = CType(secsItem_V.Value, String)
                                Case SecsID.NgBin1SVID
                                    'eq.NGbin1 = CType(secsItem_V.Value, String)
                                Case SecsID.NgBin2SVID
                                    'eq.NGbin2 = CType(secsItem_V.Value, String)
                                Case SecsID.NgBin3SVID
                                    'eq.NGbin3 = CType(secsItem_V.Value, String)
                                Case SecsID.NgBin4SVID
                                    'eq.NGbin4 = CType(secsItem_V.Value, String)
                                Case SecsID.NgBin5SVID
                                    'eq.NGbin5 = CType(secsItem_V.Value, String)
                                Case SecsID.NgBin6SVID
                                    'eq.NGbin6 = CType(secsItem_V.Value, String)
                            End Select

                        End If


                        'Case 101 'control status
                        '    eq.ControlState = CType(CType(secsItem_V, SecsItemU1).Value(0), ControlStateType)
                        'Case 102 'eq status
                        '    Dim newStatus As EquipmentState = CType(CType(secsItem_V, SecsItemU1).Value(0), EquipmentState)
                        '    If eq.EQStatus <> newStatus Then
                        '        eq.EQStatus = newStatus
                        '    End If
                        'Case 103 'current time
                        '    eq.CurrentTime = CType(secsItem_V.Value, String)
                        'Case 104 'operation mode
                        '    eq.OperationMode = CType(secsItem_V, SecsItemU1).Value(0)
                        'Case 105 'current ppid
                        '    eq.CurrentPPID = CType(secsItem_V.Value, String)
                        'Case 106 'alarm id
                        '    eq.AlarmID = CType(secsItem_V, SecsItemU4).Value(0)
                        'Case 107 'alarm count
                        '    eq.AlarmCount = CType(secsItem_V, SecsItemU4).Value(0)
                        'Case 108 'alarm stop time
                        '    eq.AlarmStop = CType(secsItem_V, SecsItemU4).Value(0)
                        'Case 109 'product run time
                        '    eq.ProductRun = CType(secsItem_V, SecsItemU4).Value(0)
                        'Case 110 'normal stop time
                        '    eq.NormalStop = CType(secsItem_V, SecsItemU4).Value(0)
                        'Case 112 'lot status
                        '    eq.Lotstatus = CType(CType(secsItem_V, SecsItemU1).Value(0), LotState)
                        'Case 113 'lot Number (ID)
                        '    eq.LotNumberID = CType(secsItem_V.Value, String)
                        'Case 114 'lot number (Recieved from HOST)
                        '    eq.LotNumberRecived = CType(secsItem_V.Value, String)
                        'Case 115 'Job number
                        '    eq.JobNumber = CType(secsItem_V.Value, String)
                        'Case 116 'package name
                        '    eq.PackageName = CType(secsItem_V.Value, String)
                        'Case 117 'magazine number
                        '    eq.MagazineNumber = CType(secsItem_V.Value, String)
                        'Case 118 'customer name
                        '    eq.CustomerName = CType(secsItem_V.Value, String)
                        'Case 119 'carrier name
                        '    eq.CarrierName = CType(secsItem_V.Value, String)
                        'Case 120 'input reel id
                        '    eq.InputReelID = CType(secsItem_V.Value, String)
                        'Case 121 'output reel id 1
                        '    eq.OutputReelID1 = CType(secsItem_V.Value, String)
                        'Case 122 'output reel id 2
                        '    eq.OutputReelID2 = CType(secsItem_V.Value, String)
                        'Case 123 'serial id (S6F11)
                        '    eq.SerialID = CType(secsItem_V.Value, String)
                        'Case 124 'output reel id 1 with .R01
                        '    eq.LotNumberBinOutputReelID1 = CType(secsItem_V.Value, String)
                        'Case 125 'output reel id 2 with .R02
                        '    eq.LotNumberBinOutputReelID2 = CType(secsItem_V.Value, String)
                        'Case 191 'EQ ID
                        '    eq.EQID = CType(secsItem_V.Value, String)
                        'Case 192 'EQ Model
                        '    eq.EQModel = CType(secsItem_V.Value, String)
                        'Case 193 'Software Revision
                        '    eq.SoftwareRevision = CType(secsItem_V.Value, String)
                        'Case 270 'Loader Door Lock Status
                        '    eq.LoaderDoorStatus = CType(CType(secsItem_V, SecsItemU1).Value(0), DoorState)
                        'Case 271 'UnLoader Door Lock Status
                        '    eq.UnloaderDoorStatus = CType(CType(secsItem_V, SecsItemU1).Value(0), DoorState)
                        'Case 280 'Suply Tube Go
                        '    eq.SupplyTubeState = CType(CType(secsItem_V, SecsItemU1).Value(0), TubeStateType)
                        'Case 281 'Good Tube Go
                        '    eq.GoodTubeGo = CType(CType(secsItem_V, SecsItemU1).Value(0), TubeStateType)
                        'Case 282 'ID printed  seal End
                        '    eq.IDSealedSticking = CType(CType(secsItem_V, SecsItemU1).Value(0), IDSealedStickingType)
                        'Case 290 'LotEnd Select
                        Select Case definedReport.VID(i) '2017-12-07 / JERVIN
                            'LOT DATA
                            Case 20000 'LOT NUMBER
                                eq.LotNumber = CType(secsItem_V.Value, String)
                            Case 29120 ' PACKAGE NAME
                                eq.PackageName = CType(secsItem_V.Value, String)
                            Case 29121 'ASSY DEVICE NAME
                                eq.AssyDeviceName = CType(secsItem_V.Value, String)
                            Case 29122 ' FRAME TYPE
                                eq.FrameType = CType(secsItem_V.Value, String)
                            Case 29123 'FACET ORIENTATION
                                eq.FacetOrientation = CType(secsItem_V.Value, String)
                            Case 29124 'CODE NUMBER 
                                eq.CodeNumber = CType(secsItem_V.Value, String)
                            Case 29125 'WF LOT NUMBER
                                eq.WFLotNumber = CType(secsItem_V.Value, String)
                            Case 29126 'TAPING DIR
                                eq.TapingDir = CType(secsItem_V.Value, String)
                            Case 29127 'MARK CATEGORY
                                eq.MarkCategory = CType(secsItem_V.Value, String)
                            Case 29128 'MARK SPECS 1
                                eq.MarkSpecs1 = CType(secsItem_V.Value, String)
                            Case 29129 'MARK SPECS 2
                                eq.MarkSpecs2 = CType(secsItem_V.Value, String)
                            Case 29130 'MARK SPECS 3
                                eq.MarkSpecs3 = CType(secsItem_V.Value, String)
                            Case 29131 'LINES FOR MARK
                                eq.LinesForMark = CType(secsItem_V.Value, String)
                            Case 29132 'OS FT SWITCH
                                eq.OSFTSwitch = CType(secsItem_V.Value, String)
                            Case 29133 'OS PROGRAM
                                eq.OSProgram = CType(secsItem_V.Value, String)
                            Case 29134 'RESIN TYPE
                                eq.ResinType = CType(secsItem_V.Value, String)
                            Case 29135 'NEW PACKAGE NAME
                                eq.NewPackageName = CType(secsItem_V.Value, String)
                            Case 29136 'FT DEVICE NAME
                                eq.FTDeviceName = CType(secsItem_V.Value, String)
                            Case 29137 'MARKING NUMBER
                                eq.MarkingNumber = CType(secsItem_V.Value, String)
                            Case 29138 'REEL COLOR
                                eq.ReelColor = CType(secsItem_V.Value, String)
                            Case 29139 'UL MARK
                                eq.ULMark = CType(secsItem_V.Value, String)
                            Case 29140 'QTY PER REEL
                                eq.QtyPerReel = CType(secsItem_V.Value, String)
                            Case 29141 'CLAIM COUNTERMEASURE
                                eq.ClaimCountermeasure = CType(secsItem_V.Value, String)
                            Case 29142 'SUB RANK
                                eq.SubRank = CType(secsItem_V.Value, String)
                            Case 29143 'MASK
                                eq.Mask = CType(secsItem_V.Value, String)
                            Case 20208 'START MODE
                                eq.StartMode = CType(CType(secsItem_V, SecsItemU1).Value(0), StartModeType)
                            Case 1009 'PROCESS STATE CHANGE
                                eq.ProcessState = CType(CType(secsItem_V, SecsItemU1).Value(0), ProcessStateType)
                            Case 1008 'PREVIOUS PROCESS STATE CHANGE
                                eq.ProcessState = CType(CType(secsItem_V, SecsItemU1).Value(0), ProcessStateType)
                            'FTOIS DATA
                            Case 29104 ' FTOIS HEADER
                                eq.FTOISHeader = CType(secsItem_V.Value, String)
                            Case 29105 'FTOIS DEVICE NAME
                                eq.FTOISDeviceName = CType(secsItem_V.Value, String)
                            Case 29106 'FTOIS INPUT RANK
                                eq.FTOISInputRank = CType(secsItem_V.Value, String)
                            Case 29107 'FTOIS PACKAGE NAME
                                eq.FTOISPackageName = CType(secsItem_V.Value, String)
                            Case 29108 'FTOIS TESTER TYPE
                                eq.FTOISTesterType = CType(secsItem_V.Value, String)
                            Case 29110 'FTOIS FT PROGRAM NAME
                                eq.FTOISFTProgramName = CType(secsItem_V.Value, String)
                            Case 29100 'FTOIS TEST TYPE
                                eq.FTOISTestType = CType(secsItem_V.Value, String)
                            Case 29109 'FTOIS BOX
                                eq.FTOISBox = CType(secsItem_V.Value, String)
                            Case 29102 'FTOIS MULTI
                                eq.FTOISMulti = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 29103 'FTOIS PATTERN
                                eq.FTOISPattern = CType(secsItem_V, SecsItemU4).Value(0)
                            'LOT STATISTICS
                            Case 29021 'YIELD
                                'eq.Yield = CType(secsItem_V.Value, Single())
                                eq.Yield = CType(CType(secsItem_V, SecsItemF4).Value(0), String)
                            Case 29020 'UPH
                                eq.UPH = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 29012 'OPERATION RATE
                                'eq.OperationRate = CType(secsItem_V.Value, Single())
                                eq.OperationRate = CType(CType(secsItem_V, SecsItemF4).Value(0), String)
                            Case 29101 'INPUT QTY
                                eq.InputQty = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 20291 'OPERATOR NUMBER
                                eq.OperatorNumber = CType(secsItem_V.Value, String)
                            Case 1006 'PP EXEC NAME
                                eq.PPExecName = CType(secsItem_V.Value, String)
                            'MACHINE COUNTER
                            Case 20402 'GOOD BIN
                                eq.GoodBin = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 20410 'FT NG BIN
                                eq.FTNGBin = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 20412 'OS NG BIN
                                eq.OSNGBin = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 20404 'TOTAL NG
                                eq.TotalNG = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 20401 '
                                eq.Total = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 29023 'ASI LOT QTY
                                eq.ASILotQty = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 29024 'ASI LOT INITIAL
                                eq.ASILotInitial = CType(secsItem_V, SecsItemU1).Value(0)
                            Case 29025 'ASI LOT TERMINAL
                                eq.ASILotTerminal = CType(secsItem_V, SecsItemU1).Value(0)
                            'LOT MONITORING
                            Case 29011 'RUN TIME
                                eq.RunTime = CType(secsItem_V.Value, String)
                            Case 29013 'ALARM TIME
                                eq.AlarmTime = CType(secsItem_V.Value, String)
                            Case 29016 'LOT END WAITING TIME
                                eq.LotWaitTime = CType(secsItem_V.Value, String)
                            Case 29010 'PRODUCTION TIME
                                eq.ProductionTime = CType(secsItem_V.Value, String)
                            Case 29018 'MTBA
                                eq.MTBA = CType(secsItem_V.Value, String)
                            Case 29019 'MTTR
                                eq.MTTR = CType(secsItem_V.Value, String)
                            Case 29014 'ALARM COUNT
                                eq.AlarmCount = CType(secsItem_V, SecsItemU4).Value(0)
                            Case 3000 'ALARM ID
                                eq.ALID = CType(CType(secsItem_V, SecsItemU2).Value(0), String)
                            Case 40200 'ALARM TEXT
                                eq.ALText = CType(secsItem_V.Value, String)
                            Case 40201 'ALARM CODE
                                eq.ALCode = CType(secsItem_V.Value, String)
                            Case 40202 'ALARM TYPE
                                eq.ALType = CType(CType(secsItem_V, SecsItemU1).Value(0), AlarmType)
                            Case 40203 'Unit Name
                                eq.ALUnitName = CType(secsItem_V.Value, String)
                        End Select
                    Next
                End If
            Next
        Catch ex As Exception
            SaveCatchLog(ex.ToString, "ApplyStatusVariableValue()")
        End Try
    End Sub
End Class