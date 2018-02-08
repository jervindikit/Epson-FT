Imports XtraLibrary.SecsGem

Public Class S5F3
    Inherits SecsMessageBase
    'CHANGE UInteger TO UShort Due to xtraLibrary Variable set / jdikit 9263
    'Public Sub New(ByVal Enable As Boolean, ByVal ALID As UInteger)
    Public Sub New(ByVal Enable As Boolean, ByVal ALID As UShort)
        MyBase.New(5, 3, True)


        'Dim m_ALID As New SecsItemU4("ALID")
        Dim m_ALID As New SecsItemU2("ALID")
        If Not ALID = Nothing Then
            m_ALID = New SecsItemU2("ALID", ALID)
        End If



        Dim m_ALED As New SecsItemBinary("ALED", &H80)
        If Not Enable Then
            m_ALED.Value(0) = &H0
        End If
        Dim M_LIST As New SecsItemList("L2")
        AddItem(M_LIST)
        M_LIST.AddItem(m_ALED)
        M_LIST.AddItem(m_ALID)

    End Sub
End Class
