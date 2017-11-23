Imports System.Xml.Serialization
Imports System.IO

Public Class XmlSerializationHelper

    Public Shared Sub Save(Of T)(fileName As String, data As T)
        Dim xs As XmlSerializer = New XmlSerializer(GetType(T))
        Using writer As StreamWriter = New StreamWriter(fileName)
            xs.Serialize(writer, data)
        End Using
    End Sub

    Public Shared Function Load(Of T)(fileName As String) As T
        Dim ret As T
        Dim xs As XmlSerializer = New XmlSerializer(GetType(T))
        Using reader As StreamReader = New StreamReader(fileName)
            ret = CType(xs.Deserialize(reader), T)
        End Using
        Return ret
    End Function

End Class
