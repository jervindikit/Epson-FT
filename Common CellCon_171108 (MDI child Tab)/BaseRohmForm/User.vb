Imports System.IO

Public Class User

#Region "Shared Members"

    Private Shared c_CurrentUser As User
    Private Shared c_UserList As List(Of User)

    Public Shared ReadOnly Property CurrentUser As User
        Get
            Return c_CurrentUser
        End Get
    End Property

    Public Shared Function GetAll() As User()
        Return c_UserList.ToArray()
    End Function

    Public Shared Function Login(user As String, password As String) As Boolean
        For Each u As User In c_UserList.ToArray()
            If u.UserName.ToLower() = user.ToLower() AndAlso password = u.c_Password Then
                c_CurrentUser = u
                Return True
            End If
        Next
        Return False
    End Function

    Public Shared Function AddUser(userName As String, password As String, level As UserLevel) As User

        For Each u As User In c_UserList
            If u.UserName.ToLower() = userName.ToLower() Then
                Throw New Exception("User is already exists")
            End If
        Next

        Dim newUser As User = New User()
        newUser.UserName = userName
        newUser.Password = password
        newUser.Level = level

        c_UserList.Add(newUser)

        Dim fileName As String = Path.Combine(My.Application.Info.DirectoryPath, "Users.xml")
        XmlSerializationHelper.Save(fileName, c_UserList)


        Return newUser
    End Function

    Public Shared Sub RemoveUser(userName As String)
        For Each u As User In c_UserList.ToArray()
            If u.UserName = userName Then
                c_UserList.Remove(u)
                Dim fileName As String = Path.Combine(My.Application.Info.DirectoryPath, "Users.xml")
                XmlSerializationHelper.Save(fileName, c_UserList)
            End If
        Next

    End Sub

    Shared Sub New()


        c_CurrentUser = New User
        c_CurrentUser.Level = UserLevel.OP
        c_CurrentUser.UserName = "Guest"

        Dim fileName As String
        fileName = Path.Combine(My.Application.Info.DirectoryPath, "Users.xml")
        If File.Exists(fileName) Then
            c_UserList = XmlSerializationHelper.Load(Of List(Of User))(fileName)
        Else
            c_UserList = New List(Of User)
            c_UserList.Add(New User() With {.UserName = "admin", .Password = "admin", .Level = UserLevel.ADMIN})
        End If
    End Sub

#End Region

    Public Enum UserLevel
        OP
        ADMIN
        ENGINEER
    End Enum

    Private Sub New()
    End Sub

    Private c_UserName As String
    Public Property UserName() As String
        Get
            Return c_UserName
        End Get
        Set(ByVal value As String)
            c_UserName = value
        End Set
    End Property

    Private c_Password As String
    Public Property Password() As String
        Get
            Return c_Password
        End Get
        Set(ByVal value As String)
            c_Password = value
        End Set
    End Property

    Private c_Level As UserLevel
    Public Property Level() As UserLevel
        Get
            Return c_Level
        End Get
        Set(ByVal value As UserLevel)
            c_Level = value
        End Set
    End Property


End Class
