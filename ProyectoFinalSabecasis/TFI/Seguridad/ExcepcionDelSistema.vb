Imports Modelo

Public Class ExcepcionDelSistema
    Inherits Exception

    Property excepcionOriginal As Exception

    Property mensaje As String = ""
    Property clase As String = ""
    Property tipoDeExcepcion As New TipoDeExcepcion
    Property query As String = ""
    Property parametrosProcesados As String = ""

    Public Sub New(e As Exception)
        excepcionOriginal = e
        tipoDeExcepcion.id = 2
        log(e.Message)
        GuardarEnLogHelper.instancia().guardarEnLog(excepcionOriginal.Message, clase, parametrosProcesados, query, tipoDeExcepcion)
    End Sub


    Protected Sub log(message As String)
        Dim file As System.IO.StreamWriter
        Try
            file = My.Computer.FileSystem.OpenTextFileWriter("/log.txt", True)
            file.WriteLine(message)
            file.Close()
        Catch ex As Exception
        End Try

    End Sub
End Class
