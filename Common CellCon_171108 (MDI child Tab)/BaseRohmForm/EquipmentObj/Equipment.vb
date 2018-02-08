<Serializable()>
Public Class Equipment
#Region "Lot Data"
    Private m_LotNumber As String
    Public Property LotNumber() As String
        Get
            Return m_LotNumber
        End Get
        Set(ByVal value As String)
            m_LotNumber = value
        End Set
    End Property
    Private m_PackageName As String
    Public Property PackageName() As String
        Get
            Return m_PackageName
        End Get
        Set(ByVal value As String)
            m_PackageName = value
        End Set
    End Property
    Private m_AssyDeviceName As String
    Public Property AssyDeviceName() As String
        Get
            Return m_AssyDeviceName
        End Get
        Set(ByVal value As String)
            m_AssyDeviceName = value
        End Set
    End Property
    Private m_FrameType As String
    Public Property FrameType() As String
        Get
            Return m_FrameType
        End Get
        Set(ByVal value As String)
            m_FrameType = value
        End Set
    End Property
    Private m_FacetOrientation As String
    Public Property FacetOrientation() As String
        Get
            Return m_FacetOrientation
        End Get
        Set(ByVal value As String)
            m_FacetOrientation = value
        End Set
    End Property
    Private m_CodeNumber As String
    Public Property CodeNumber() As String
        Get
            Return m_CodeNumber
        End Get
        Set(ByVal value As String)
            m_CodeNumber = value
        End Set
    End Property
    Private m_WFLotNumber As String
    Public Property WFLotNumber() As String
        Get
            Return m_WFLotNumber
        End Get
        Set(ByVal value As String)
            m_WFLotNumber = value
        End Set
    End Property
    Private m_TapingDir As String
    Public Property TapingDir() As String
        Get
            Return m_TapingDir
        End Get
        Set(ByVal value As String)
            m_TapingDir = value
        End Set
    End Property
    Private m_MarkCategory As String
    Public Property MarkCategory() As String
        Get
            Return m_MarkCategory
        End Get
        Set(ByVal value As String)
            m_MarkCategory = value
        End Set
    End Property
    Private m_MarkSpecs1 As String
    Public Property MarkSpecs1() As String
        Get
            Return m_MarkSpecs1
        End Get
        Set(ByVal value As String)
            m_MarkSpecs1 = value
        End Set
    End Property
    Private m_MarkSpecs2 As String
    Public Property MarkSpecs2() As String
        Get
            Return m_MarkSpecs2
        End Get
        Set(ByVal value As String)
            m_MarkSpecs2 = value
        End Set
    End Property
    Private m_MarkSpecs3 As String
    Public Property MarkSpecs3() As String
        Get
            Return m_MarkSpecs3
        End Get
        Set(ByVal value As String)
            m_MarkSpecs3 = value
        End Set
    End Property
    Private m_LinesForMark As String
    Public Property LinesForMark() As String
        Get
            Return m_LinesForMark
        End Get
        Set(ByVal value As String)
            m_LinesForMark = value
        End Set
    End Property
    Private m_OSFTSwitch As String
    Public Property OSFTSwitch() As String
        Get
            Return m_OSFTSwitch
        End Get
        Set(ByVal value As String)
            m_OSFTSwitch = value
        End Set
    End Property
    Private m_OSProgram As String
    Public Property OSProgram() As String
        Get
            Return m_OSProgram
        End Get
        Set(ByVal value As String)
            m_OSProgram = value
        End Set
    End Property
    Private m_ResinType As String
    Public Property ResinType() As String
        Get
            Return m_ResinType
        End Get
        Set(ByVal value As String)
            m_ResinType = value
        End Set
    End Property
    Private m_NewPackageName As String
    Public Property NewPackageName() As String
        Get
            Return m_NewPackageName
        End Get
        Set(ByVal value As String)
            m_NewPackageName = value
        End Set
    End Property
    Private m_FTDeviceName As String
    Public Property FTDeviceName() As String
        Get
            Return m_FTDeviceName
        End Get
        Set(ByVal value As String)
            m_FTDeviceName = value
        End Set
    End Property
    Private m_MarkingNumber As String
    Public Property MarkingNumber() As String
        Get
            Return m_MarkingNumber
        End Get
        Set(ByVal value As String)
            m_MarkingNumber = value
        End Set
    End Property
    Private m_ReelColor As String
    Public Property ReelColor() As String
        Get
            Return m_ReelColor
        End Get
        Set(ByVal value As String)
            m_ReelColor = value
        End Set
    End Property
    Private m_ULMark As String
    Public Property ULMark() As String
        Get
            Return m_ULMark
        End Get
        Set(ByVal value As String)
            m_ULMark = value
        End Set
    End Property
    Private m_QtyPerReel As String
    Public Property QtyPerReel() As String
        Get
            Return m_QtyPerReel
        End Get
        Set(ByVal value As String)
            m_QtyPerReel = value
        End Set
    End Property
    Private m_ClaimCountermeasure As String
    Public Property ClaimCountermeasure() As String
        Get
            Return m_ClaimCountermeasure
        End Get
        Set(ByVal value As String)
            m_ClaimCountermeasure = value
        End Set
    End Property
    Private m_SubRank As String
    Public Property SubRank() As String
        Get
            Return m_SubRank
        End Get
        Set(ByVal value As String)
            m_SubRank = value
        End Set
    End Property
    Private m_Mask As String
    Public Property Mask() As String
        Get
            Return m_Mask
        End Get
        Set(ByVal value As String)
            m_Mask = value
        End Set
    End Property
    Private m_StartMode As StartModeType
    Public Property StartMode() As StartModeType
        Get
            Return m_StartMode
        End Get
        Set(ByVal value As StartModeType)
            m_StartMode = value
        End Set
    End Property
    Private m_ProcessState As ProcessStateType
    Public Property ProcessState() As ProcessStateType
        Get
            Return m_ProcessState
        End Get
        Set(ByVal value As ProcessStateType)
            m_ProcessState = value
        End Set
    End Property
#End Region
#Region "FTOIS Data - Lot Statistic - Lot Monitoring - Machine Counter"
    Private m_FTOISHeader As String
    Public Property FTOISHeader() As String
        Get
            Return m_FTOISHeader
        End Get
        Set(ByVal value As String)
            m_FTOISHeader = value
        End Set
    End Property
    Private m_FTOISDeviceName As String
    Public Property FTOISDeviceName() As String
        Get
            Return m_FTOISDeviceName
        End Get
        Set(ByVal value As String)
            m_FTOISDeviceName = value
        End Set
    End Property
    Private m_FTOISInputRank As String
    Public Property FTOISInputRank() As String
        Get
            Return m_FTOISInputRank
        End Get
        Set(ByVal value As String)
            m_FTOISInputRank = value
        End Set
    End Property
    Private m_FTOISPackageName As String
    Public Property FTOISPackageName() As String
        Get
            Return m_FTOISPackageName
        End Get
        Set(ByVal value As String)
            m_FTOISPackageName = value
        End Set
    End Property
    Private m_FTOISTesterType As String
    Public Property FTOISTesterType() As String
        Get
            Return m_FTOISTesterType
        End Get
        Set(ByVal value As String)
            m_FTOISTesterType = value
        End Set
    End Property
    Private m_FTOISBox As String
    Public Property FTOISBox() As String
        Get
            Return m_FTOISBox
        End Get
        Set(ByVal value As String)
            m_FTOISBox = value
        End Set
    End Property
    Private m_FTOISFTProgramName As String
    Public Property FTOISFTProgramName() As String
        Get
            Return m_FTOISFTProgramName
        End Get
        Set(ByVal value As String)
            m_FTOISFTProgramName = value
        End Set
    End Property
    Private m_FTOISTestType As String
    Public Property FTOISTestType() As String
        Get
            Return m_FTOISTestType
        End Get
        Set(ByVal value As String)
            m_FTOISTestType = value
        End Set
    End Property
    Private m_FTOISMulti As UInteger
    Public Property FTOISMulti() As UInteger
        Get
            Return m_FTOISMulti
        End Get
        Set(ByVal value As UInteger)
            m_FTOISMulti = value
        End Set
    End Property
    Private m_FTOISPattern As UInteger
    Public Property FTOISPattern() As UInteger
        Get
            Return m_FTOISPattern
        End Get
        Set(ByVal value As UInteger)
            m_FTOISPattern = value
        End Set
    End Property
    Private m_Yield As String
    Public Property Yield() As String
        Get
            Return m_Yield
        End Get
        Set(ByVal value As String)
            m_Yield = value
        End Set
    End Property
    Private m_UPH As UInteger
    Public Property UPH() As UInteger
        Get
            Return m_UPH
        End Get
        Set(ByVal value As UInteger)
            m_UPH = value
        End Set
    End Property
    Private m_OperationRate As String
    Public Property OperationRate() As String
        Get
            Return m_OperationRate
        End Get
        Set(ByVal value As String)
            m_OperationRate = value
        End Set
    End Property
    Private m_InputQty As UInteger
    Public Property InputQty() As UInteger
        Get
            Return m_InputQty
        End Get
        Set(ByVal value As UInteger)
            m_InputQty = value
        End Set
    End Property
    Private m_OperatorNumber As String
    Public Property OperatorNumber() As String
        Get
            Return m_OperatorNumber
        End Get
        Set(ByVal value As String)
            m_OperatorNumber = value
        End Set
    End Property
    Private m_PPExecName As String
    Public Property PPExecName() As String
        Get
            Return m_PPExecName
        End Get
        Set(ByVal value As String)
            m_PPExecName = value
        End Set
    End Property
    Private m_GoodBin As UInteger
    Public Property GoodBin() As UInteger
        Get
            Return m_GoodBin
        End Get
        Set(ByVal value As UInteger)
            m_GoodBin = value
        End Set
    End Property
    Private m_FTNGBin As UInteger
    Public Property FTNGBin() As UInteger
        Get
            Return m_FTNGBin
        End Get
        Set(ByVal value As UInteger)
            m_FTNGBin = value
        End Set
    End Property
    Private m_OSNGBin As UInteger
    Public Property OSNGBin() As UInteger
        Get
            Return m_OSNGBin
        End Get
        Set(ByVal value As UInteger)
            m_OSNGBin = value
        End Set
    End Property
    Private m_TotalNG As UInteger
    Public Property TotalNG() As UInteger
        Get
            Return m_TotalNG
        End Get
        Set(ByVal value As UInteger)
            m_TotalNG = value
        End Set
    End Property
    Private m_Total As UInteger
    Public Property Total() As UInteger
        Get
            Return m_Total
        End Get
        Set(ByVal value As UInteger)
            m_Total = value
        End Set
    End Property
    Private m_ASILotQty As UInteger
    Public Property ASILotQty() As UInteger
        Get
            Return m_ASILotQty
        End Get
        Set(ByVal value As UInteger)
            m_ASILotQty = value
        End Set
    End Property
    Private m_ASILotInitial As UInteger
    Public Property ASILotInitial() As UInteger
        Get
            Return m_ASILotInitial
        End Get
        Set(ByVal value As UInteger)
            m_ASILotInitial = value
        End Set
    End Property
    Private m_ASILotTerminal As UInteger
    Public Property ASILotTerminal() As UInteger
        Get
            Return m_ASILotTerminal
        End Get
        Set(ByVal value As UInteger)
            m_ASILotTerminal = value
        End Set
    End Property
    Private m_RunTime As String
    Public Property RunTime() As String
        Get
            Return m_RunTime
        End Get
        Set(ByVal value As String)
            m_RunTime = value
        End Set
    End Property
    Private m_AlarmTime As String
    Public Property AlarmTime() As String
        Get
            Return m_AlarmTime
        End Get
        Set(ByVal value As String)
            m_AlarmTime = value
        End Set
    End Property
    Private m_LotWaitTime As String
    Public Property LotWaitTime() As String
        Get
            Return m_LotWaitTime
        End Get
        Set(ByVal value As String)
            m_LotWaitTime = value
        End Set
    End Property
    Private m_ProductionTime As String
    Public Property ProductionTime() As String
        Get
            Return m_ProductionTime
        End Get
        Set(ByVal value As String)
            m_ProductionTime = value
        End Set
    End Property
    Private m_MTBA As String
    Public Property MTBA() As String
        Get
            Return m_MTBA
        End Get
        Set(ByVal value As String)
            m_MTBA = value
        End Set
    End Property
    Private m_MTTR As String
    Public Property MTTR() As String
        Get
            Return m_MTTR
        End Get
        Set(ByVal value As String)
            m_MTTR = value
        End Set
    End Property
    Private m_AlarmCount As UInteger
    Public Property AlarmCount() As UInteger
        Get
            Return m_AlarmCount
        End Get
        Set(ByVal value As UInteger)
            m_AlarmCount = value
        End Set
    End Property
    Private m_ALID As String
    Public Property ALID() As String
        Get
            Return m_ALID
        End Get
        Set(ByVal value As String)
            m_ALID = value
        End Set
    End Property
    Private m_ALText As String
    Public Property ALText() As String
        Get
            Return m_ALText
        End Get
        Set(ByVal value As String)
            m_ALText = value
        End Set
    End Property
    Private m_ALCode As String
    Public Property ALCode() As String
        Get
            Return m_ALCode
        End Get
        Set(ByVal value As String)
            m_ALCode = value
        End Set
    End Property
    Private m_ALType As AlarmType
    Public Property ALType() As AlarmType
        Get
            Return m_ALType
        End Get
        Set(ByVal value As AlarmType)
            m_ALType = value
        End Set
    End Property
    Private m_ALUnitName As String
    Public Property ALUnitName() As String
        Get
            Return m_ALUnitName
        End Get
        Set(ByVal value As String)
            m_ALUnitName = value
        End Set
    End Property
#End Region
End Class
