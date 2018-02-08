Imports XtraLibrary.SecsGem
Public Class S14F2
    Inherits SecsMessageBase
    Private m_Ln As SecsItemList
    Private m_L2p As New SecsItemList("L2p")



    Public Sub New()
        MyBase.New(14, 2, False)
        Dim m_L2 As New SecsItemList("L2")
        AddItem(m_L2)
        m_Ln = New SecsItemList("Ln")
        m_L2.AddItem(m_Ln)
        m_L2p = New SecsItemList("L2p")
        m_L2.AddItem(m_L2p)
      

    End Sub

    Public Sub AOI_AttrAdd(ByVal OBJID As String, ByVal OrgLoc As Byte, ByVal CellStaus As Byte(), ByVal OBJACK As Byte) 'acknowledge code, 0 ok, 1 error 
        Dim m_L2 As New SecsItemList("L2")
        Dim m_OBJID As New SecsItemAscii("OBJID", OBJID)

        Dim m_La As New SecsItemList("La")

        Dim mLa_L2 As New SecsItemList("La_L2")
        mLa_L2.AddItem(New SecsItemAscii("OrgLoc", "OriginLocation"))
        mLa_L2.AddItem(New SecsItemU1("OrgLoc", OrgLoc))
        m_La.AddItem(mLa_L2)
        mLa_L2 = New SecsItemList("La_L2")
        mLa_L2.AddItem(New SecsItemAscii("Rows", "Rows"))
        mLa_L2.AddItem(New SecsItemU4("Rows", 10))
        m_La.AddItem(mLa_L2)
        mLa_L2 = New SecsItemList("La_L2")
        mLa_L2.AddItem(New SecsItemAscii("Columns", "Columns"))
        mLa_L2.AddItem(New SecsItemU4("Columns", 10))
        m_La.AddItem(mLa_L2)
        mLa_L2 = New SecsItemList("La_L2")
        mLa_L2.AddItem(New SecsItemAscii("CellStatus", "CellStatus"))
        mLa_L2.AddItem(New SecsItemU1("CellStatus", CellStaus))
        m_La.AddItem(mLa_L2)

        m_L2.AddItem(m_OBJID)
        m_L2.AddItem(m_La)
        m_Ln.AddItem(m_L2)




        Dim m_OBJACK As New SecsItemU1("OBJACK", OBJACK)
        Dim m_Lp As New SecsItemList("Lp")
       
        m_L2p.AddItem(m_OBJACK)
        m_L2p.AddItem(m_Lp)

    End Sub
    
End Class
