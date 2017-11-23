Imports XtraLibrary.SecsGem
Public Class S2F49
    Inherits SecsMessageBase
    Private m_RCMD As SecsItemAscii
    Private m_Ln As SecsItemList
    Public Sub New(ByVal DataID As String)
        MyBase.New(2, 49, True)
        Dim m_L4 As SecsItemList = New SecsItemList("L4")
        Dim SecsData As New SecsDataType
        AddItem(m_L4)

        If SecsData.DATAID = SecsDataType.U4_1.U4 Then
            Dim m_DataId As New SecsItemU4("DATAID", CUInt(DataID))
            m_L4.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.U4_1.U2 Then
            Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
            m_L4.AddItem(m_DataId)
        ElseIf SecsData.DATAID = SecsDataType.U4_1.U1 Then
            Dim m_DataId As New SecsItemU1("DATAID", CByte(DataID))
            m_L4.AddItem(m_DataId)
        Else
            Dim m_DataId As New SecsItemU2("DATAID", CUShort(DataID))
            m_L4.AddItem(m_DataId)
        End If

        Dim OBJSPEC As SecsItemAscii = New SecsItemAscii("OBJSPEC", "")
        m_L4.AddItem(OBJSPEC)
        m_RCMD = New SecsItemAscii("RCMD")
        m_L4.AddItem(m_RCMD)

        m_Ln = New SecsItemList("Ln")
        m_L4.AddItem(m_Ln)


    End Sub


    Public Property RemoteCommand() As String
        Get
            Return m_RCMD.Value
        End Get
        Set(ByVal value As String)
            m_RCMD.Value = value
        End Set
    End Property

    Public Sub AddVariable(ByVal cpName As String, ByVal cpValue As String)
        Dim l2 As SecsItemList = New SecsItemList("L2")
        m_Ln.AddItem(l2)

        Dim l2_cpName As SecsItemAscii = New SecsItemAscii("CPNAME" & m_Ln.Value.Count.ToString())
        l2_cpName.Value = cpName
        l2.AddItem(l2_cpName)
        Dim l2_cpVal As SecsItemAscii = New SecsItemAscii("CPVAL" & m_Ln.Value.Count.ToString())
        l2_cpVal.Value = cpValue
        l2.AddItem(l2_cpVal)
    End Sub

    Public Sub AddVariable(ByVal cpValue As String)
        Dim l2 As SecsItemList = New SecsItemList("L1")
        m_Ln.AddItem(l2)
        Dim l2_cpVal As SecsItemAscii = New SecsItemAscii("CPVAL" & m_Ln.Value.Count.ToString())
        l2_cpVal.Value = cpValue
        l2.AddItem(l2_cpVal)
    End Sub

    '-- Example --

    '    Dim cmd As S2F49 = New S2F49(CStr(0))
    '    cmd.RemoteCommand = RCmd 
    '    cmd.AddVariable(CmdPName, CmdPVal)
    '    cmd.AddVariable(CmdPName1, CmdPVal1)
    '    cmd.AddVariable(CmdPName2, CmdPVal2)
    '    RaiseEvent E_HostSend(cmd)

End Class
