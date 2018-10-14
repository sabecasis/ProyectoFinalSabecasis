Public Class ExcepcionDeNegocios
    Inherits ExcepcionDelSistema

    Public Sub New(e As Exception, clase As String)
        MyBase.New(e)
        tipoDeExcepcion.id = 2
        mensaje = " Clase: " & clase
        clase = clase
        log(mensaje)
        GuardarEnLogHelper.instancia().guardarEnLog(excepcionOriginal.Message, clase, parametrosProcesados, query, tipoDeExcepcion)
    End Sub

  
End Class
