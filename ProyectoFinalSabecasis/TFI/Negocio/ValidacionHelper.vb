Imports System.Text.RegularExpressions
Imports Seguridad
Imports Modelo

Public Class ValidacionHelper
    Private Sub New()

    End Sub
    Private Shared _instancia As New ValidacionHelper()
    Public Shared Function instancia() As ValidacionHelper
        Return _instancia
    End Function

    Public Sub validarTarjeta(numero As String, metodo_de_pago_id As Integer)
        Dim metodo As MetodoDePago = GestorDePagos.instancia().obtenerRegexPorMetodoDePago(metodo_de_pago_id)
        Dim match As Match = Regex.Match(numero, metodo.regex)
        If Not match.Success Then
            Throw New ExcepcionDeValidacion("La tarjeta de crédito ingresada no es válida para el método de pago ingresado")
        End If
    End Sub

End Class
