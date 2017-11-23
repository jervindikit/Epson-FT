Public Class CheckUserPermissionResult

    Private c_IsPermit As Boolean
    Public Property IsPermit() As Boolean
        Get
            Return c_IsPermit
        End Get
        Set(ByVal value As Boolean)
            c_IsPermit = value
        End Set
    End Property

    Private c_ErrorMessage As String
    Public Property ErrorMessage() As String
        Get
            Return c_ErrorMessage
        End Get
        Set(ByVal value As String)
            c_ErrorMessage = value
        End Set
    End Property

End Class
