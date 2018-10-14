Imports System.Text.RegularExpressions
Imports Seguridad
Imports Modelo

Public Class ValidacionHelper
    Private Sub New()
    End Sub
    Private fecha_caducidad_nota_de_credito As Date = New Date()

    Private Shared _instancia As New ValidacionHelper()
    Public Shared Function instancia() As ValidacionHelper
        Return _instancia
    End Function

    Public Sub validarNoCero(campo As Integer, mensaje As String)
        If campo = 0 Then
            Throw New ExcepcionDeValidacion(ConstantesDeMensaje.CAMPO_VACIO.Replace("?", mensaje))
        End If
    End Sub

    Public Sub validarCampoVacio(campo As String, mensaje As String)
        If String.IsNullOrWhiteSpace(campo) Then
            Throw New ExcepcionDeValidacion(ConstantesDeMensaje.CAMPO_VACIO.Replace("?", mensaje))
        End If
    End Sub
    Public Sub validarTarjeta(numero As String, metodo_de_pago_id As Integer)
        Dim metodo As MetodoDePago = GestorDePagos.instancia().obtenerRegexPorMetodoDePago(metodo_de_pago_id)
        Dim regex As New Regex(metodo.regex)
        If Not regex.IsMatch(numero) Then
            Throw New ExcepcionDeValidacion("La tarjeta de crédito ingresada no es válida para el método de pago ingresado")
        End If
        If metodo.id = 1 Then
            If Not "4906961055233369".Equals(numero) Then
                Throw New ExcepcionDeValidacion("El número no corresponde a una tajeta de crédito válida")
            End If
        ElseIf metodo.id = 2 Then
            If Not "5239168102225252".Equals(numero) Then
                Throw New ExcepcionDeValidacion("El número no corresponde a una tajeta de crédito válida")
            End If
        ElseIf metodo.id = 3 Then
            If Not "344641629186004".Equals(numero) Then
                Throw New ExcepcionDeValidacion("El número no corresponde a una tajeta de crédito válida")
            End If
        End If
    End Sub

    Public Sub validatTitularDeTarjeta(titular As String, numero As String, metodo_de_pago_id As Integer)
        If metodo_de_pago_id = 1 Then
            If Not ("4906961055233369".Equals(numero) And "Sabrina Abecasis".Equals(titular)) Then
                Throw New ExcepcionDeValidacion("Este no es un titular de tarjeta válido")
            End If
        ElseIf metodo_de_pago_id = 2 Then
            If Not ("5239168102225252".Equals(numero) And "Mauricio Prinzo".Equals(titular)) Then
                Throw New ExcepcionDeValidacion("Este no es un titular de tarjeta válido")
            End If
        ElseIf metodo_de_pago_id = 3 Then
            If Not ("344641629186004".Equals(numero) And "Jorge Scali".Equals(titular)) Then
                Throw New ExcepcionDeValidacion("Este no es un titular de tarjeta válido")
            End If
        End If
    End Sub

    Public Sub validarCvv(cvv As String, numero As String, metodo_de_pago_id As Integer)
        If metodo_de_pago_id = 1 Then
            If Not ("4906961055233369".Equals(numero) And "123".Equals(cvv)) Then
                Throw New ExcepcionDeValidacion("Este cvv no es válido")
            End If
        ElseIf metodo_de_pago_id = 2 Then
            If Not ("5239168102225252".Equals(numero) And "122".Equals(cvv)) Then
                Throw New ExcepcionDeValidacion("Este cvv no es válido")
            End If
        ElseIf metodo_de_pago_id = 3 Then
            If Not ("344641629186004".Equals(numero) And "322".Equals(cvv)) Then
                Throw New ExcepcionDeValidacion("Este cvv no es válido")
            End If
        End If
    End Sub

    Public Sub validarNotaDeCredito(oNota As NotaDeCredito)
        If oNota Is Nothing Then
            Throw New ExcepcionDeValidacion(ConstantesDeMensaje.NOTA_DE_CREDITO_INEXISTENTE)
        End If
        If oNota.fechaCaducidad <> fecha_caducidad_nota_de_credito Then
            If oNota.fechaCaducidad < Date.Now Then
                Throw New ExcepcionDeValidacion(ConstantesDeMensaje.NOTA_DE_CREDITO_CADUCADA)
            End If
        End If
        If oNota.estaActiva = False Then
            Throw New ExcepcionDeValidacion(ConstantesDeMensaje.NOTA_DE_CREDITO_INACTIVA)
        End If

    End Sub

    Public Sub validarFechaDeExpiracion(mes As Integer, anio As Integer, numero As String, metodo_de_pago_id As Integer)
        If anio < Date.Now.Year Then
            Throw New ExcepcionDeValidacion(ConstantesDeMensaje.TARJETA_VENCIDA)
        ElseIf anio = Date.Now.Year Then
            If mes <= Date.Now.Month Then
                Throw New ExcepcionDeValidacion(ConstantesDeMensaje.TARJETA_VENCIDA)
            End If
        End If
        If metodo_de_pago_id = 1 Then
            If Not ("4906961055233369".Equals(numero) And 1 = mes And 2020 = anio) Then
                Throw New ExcepcionDeValidacion("La fecha de expiración no corresponde a la tarjeta ingresada")
            End If
        ElseIf metodo_de_pago_id = 2 Then
            If Not ("5239168102225252".Equals(numero) And 12 = mes And 2017 = anio) Then
                Throw New ExcepcionDeValidacion("La fecha de expiración no corresponde a la tarjeta ingresada")
            End If
        ElseIf metodo_de_pago_id = 3 Then
            If Not ("344641629186004".Equals(numero) And 10 = mes And 2019 = anio) Then
                Throw New ExcepcionDeValidacion("La fecha de expiración no corresponde a la tarjeta ingresada")
            End If
        End If
    End Sub
End Class
