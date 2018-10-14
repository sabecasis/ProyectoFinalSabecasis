Public Class ExcepcionDeValidacion
    Inherits Exception

    Property mensaje As String
    Public Sub New(_mensaje As String)
        MyBase.New(_mensaje)
    End Sub

End Class
