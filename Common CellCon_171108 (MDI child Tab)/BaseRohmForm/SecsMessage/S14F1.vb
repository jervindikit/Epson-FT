Imports XtraLibrary.SecsGem
Public Class S14F1
    Inherits SecsMessageBase
    Private m_Li As SecsItemList
    Private m_OBJSPEC As New SecsItemAscii("OBJSPEC")
    Private m_OBJTYPE As New SecsItemAscii("OBJTYPE")
    Private m_Lq As SecsItemList
    Private m_La As SecsItemList

    Public Sub New()
        MyBase.New(14, 1, True)
        Dim m_L5 As New SecsItemList("L5")
        AddItem(m_L5)

        m_L5.AddItem(m_OBJSPEC)
        m_L5.AddItem(m_OBJTYPE)
        m_Li = New SecsItemList("Li")
        m_L5.AddItem(m_Li)
        m_Lq = New SecsItemList("Lq")
        m_L5.AddItem(m_Lq)
        m_La = New SecsItemList("La")
        m_L5.AddItem(m_La)
    End Sub
    Public ReadOnly Property OBJSPEC As String
        Get
            Dim str As String = CType(m_OBJSPEC.Value, String)
            Return str
        End Get
    End Property

    Public ReadOnly Property OBJTYPE As String
        Get
            Dim str As String = CType(m_OBJTYPE.Value, String)
            Return str
        End Get
    End Property

    Public ReadOnly Property OBJID As List(Of String)
        Get
            Dim StrList As New List(Of String)
            For Each item As SecsItem In m_Li.Value
                Dim str As String = item.Value.GetType.Name & " > This data type is  not string (SeeS14F1)"
                If item.Value.GetType.Name = "String" Then
                    str = CType(item.Value, String)
                    StrList.Add(str)
                End If
            Next


            Return StrList
        End Get
    End Property
    Public ReadOnly Property ATTRID As List(Of String)
        Get
            Dim StrList As New List(Of String)
            For Each item As SecsItem In m_La.Value
                Dim str As String = item.Value.GetType.Name & " > This data type is  not string (SeeS14F1)"
                If item.Value.GetType.Name = "String" Then
                    str = CType(item.Value, String)
                    StrList.Add(str)
                End If
            Next


            Return StrList
        End Get
    End Property

End Class
