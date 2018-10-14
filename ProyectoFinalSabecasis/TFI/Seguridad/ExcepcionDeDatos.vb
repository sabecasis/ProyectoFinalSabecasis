Imports Modelo

Public Class ExcepcionDeDatos
    Inherits ExcepcionDeNegocios

    Property parametros As String = ""


    Public Sub New(e As Exception, sqlQuery As String, parameters As List(Of DbDto), clase As String)
        MyBase.New(e, clase)

        tipoDeExcepcion.id = 1
        excepcionOriginal = e
        query = sqlQuery
        parameters = parameters
        mensaje = " \n SQL Stored Procedure: " & sqlQuery
        If Not parameters Is Nothing Then
            If parameters.Count > 0 Then
                mensaje = mensaje & " Parámetros: "
                For Each param As DbDto In parameters.AsEnumerable
                    parametros = parametros & "( " & param.parametro & ": " & param.valor & " ) "
                Next
                mensaje &= parametros
            End If
        End If
        log(mensaje)
        GuardarEnLogHelper.instancia().guardarEnLog(excepcionOriginal.Message, clase, parametrosProcesados, query, tipoDeExcepcion)
    End Sub



End Class
