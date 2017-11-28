<Serializable()> _
Public Class SecsGemID
    Private _LotStartCEID As String
    Public Property LotStartCEID() As String
        Get
            Return _LotStartCEID
        End Get
        Set(ByVal value As String)
            _LotStartCEID = value
        End Set
    End Property

    Private _LotEndCEID As String
    Public Property LotEndCEID() As String
        Get
            Return _LotEndCEID
        End Get
        Set(ByVal value As String)
            _LotEndCEID = value
        End Set
    End Property

    Private _GoodCat1SVID As String
    Public Property GoodCat1SVID() As String
        Get
            Return _GoodCat1SVID
        End Get
        Set(ByVal value As String)
            _GoodCat1SVID = value
        End Set
    End Property

    Private _GoodCat2SVID As String
    Public Property GoodCat2SVID() As String
        Get
            Return _GoodCat2SVID
        End Get
        Set(ByVal value As String)
            _GoodCat2SVID = value
        End Set
    End Property


    Private _NgBin1SVID As String
    Public Property NgBin1SVID() As String
        Get
            Return _NgBin1SVID
        End Get
        Set(ByVal value As String)
            _NgBin1SVID = value
        End Set
    End Property

    Private _NgBin2SVID As String
    Public Property NgBin2SVID() As String
        Get
            Return _NgBin2SVID
        End Get
        Set(ByVal value As String)
            _NgBin2SVID = value
        End Set
    End Property

    Private _NgBin3SVID As String
    Public Property NgBin3SVID() As String
        Get
            Return _NgBin3SVID
        End Get
        Set(ByVal value As String)
            _NgBin3SVID = value
        End Set
    End Property

    Private _NgBin4SVID As String
    Public Property NgBin4SVID() As String
        Get
            Return _NgBin4SVID
        End Get
        Set(ByVal value As String)
            _NgBin4SVID = value
        End Set
    End Property

    Private _NgBin5SVID As String
    Public Property NgBin5SVID() As String
        Get
            Return _NgBin5SVID
        End Get
        Set(ByVal value As String)
            _NgBin5SVID = value
        End Set
    End Property

    Private _NgBin6SVID As String
    Public Property NgBin6SVID() As String
        Get
            Return _NgBin6SVID
        End Get
        Set(ByVal value As String)
            _NgBin6SVID = value
        End Set
    End Property



    Private _LotIDSVID As String
    Public Property LotIDSVID() As String
        Get
            Return _LotIDSVID
        End Get
        Set(ByVal value As String)
            _LotIDSVID = value
        End Set
    End Property

    Private _EditIndex As Integer = -1    'Use when edit only
    Public Property EditIndex() As Integer
        Get
            Return _EditIndex
        End Get
        Set(ByVal value As Integer)
            _EditIndex = value
        End Set
    End Property


End Class
