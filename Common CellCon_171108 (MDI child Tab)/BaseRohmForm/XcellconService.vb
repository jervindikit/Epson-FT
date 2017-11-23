Imports Rohm.Common.Model
Imports CellcontrollerDataAdapter
Public Class XcellconService
    Implements IxCellcontroller

    Private c_Callback As IxCellcontroller
    Public ReadOnly Property CallbackInstance As IxCellcontroller
        Get
            Return c_Callback
        End Get
    End Property
    Public Sub New(machineName As String, callback As IxCellcontroller)
        c_Callback = callback
    End Sub

    Public Sub Download(data As DBData) Implements IxCellcontroller.Download
        c_Callback.Download(data)
    End Sub
End Class
