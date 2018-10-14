Imports Modelo

Imports System.Web.Services
Imports Seguridad
Imports NegocioYServicios

Public Class PagoDeOrden
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = DirectCast(Session("Sesion"), Sesion)
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
                Dim totalapagar As Double = oSesion.checkout.totalAPagar + oSesion.checkout.envio.monto
                total.Value = totalapagar.ToString("F2")
            End If

            If Me.IsPostBack Then
                If Request.Form("accion").Equals(ConstantesDeEvento.CHECKOUT) Then

                    Dim info As New InformacionDePago
                    info.persona = oSesion.usuario.persona
                    If Not String.IsNullOrEmpty(Request.Form("notacredito")) Then
                        info.nroNotaDeCredito = Convert.ToInt32(Request.Form("notacredito"))
                    End If

                    info.anioVencimiento = Request.Form("anio")
                    info.mesVencimiento = Request.Form("mes")
                    info.titular = Request.Form("titular")
                    info.nroDeTarjeta = Request.Form("nro")
                    info.cvv = Request.Form("cvv")
                    info.email = Request.Form("email")
                    info.passwordPaypal = Request.Form("password")
                    info.metodo = GestorABM.instancia().obtenerMetodoDePagoPorId(Convert.ToInt32(Request.Form("metodo")))
                    ValidacionHelper.instancia().validarTarjeta(info.nroDeTarjeta, info.metodo.id)
                    ValidacionHelper.instancia().validatTitularDeTarjeta(info.titular, info.nroDeTarjeta, info.metodo.id)
                    ValidacionHelper.instancia().validarCvv(info.cvv, info.nroDeTarjeta, info.metodo.id)
                    ValidacionHelper.instancia().validarFechaDeExpiracion(info.mesVencimiento, info.anioVencimiento, info.nroDeTarjeta, info.metodo.id)


                    Dim orden As New Orden
                    orden.informacionDePago = info
                    orden.estado = New Estado
                    orden.estado.id = 1
                    orden.detalles = GestorDePagos.instancia().calcularMontosDetalle(oSesion.checkout.productos)
                    orden.usuario = oSesion.usuario
                    orden.cuotas = info.metodo.cuotas.ElementAt(Convert.ToInt32(Request.Form("cantCuotas"))).cantidadDeCuotas
                    orden.envio = oSesion.checkout.envio
                    orden.totalAPagar = Convert.ToDouble(totalOriginalAPagar.Value)
                    orden.recargoPorTarjeta = (info.metodo.cuotas.ElementAt(Convert.ToInt32(Request.Form("cantCuotas"))).porcentajeDeRecargo * orden.totalAPagar) / 100
                    oSesion.checkout.envio.nroEnvio = GestorOrdenes.instancia().crearEnvio(oSesion.checkout.envio)
                    Dim orderId As Long = GestorOrdenes.instancia().crearOrden(orden)
                    orden.nroDeOrden = orderId
                    GestorLogs.instancia().guardarEnBitacora(2, oSesion.usuario.id)
                    If orderId <> 0 Then
                        oSesion.checkout = Nothing
                        Response.Redirect("/Cliente/ConfirmacionDeOrden.aspx?orderid=" & orderId.ToString)
                    End If
                End If
            End If
        Catch ex As ExcepcionDeValidacion
            LblMensaje.InnerText = ex.Message
            LblMensaje.DataBind()
        Catch exes As ExcepcionDelSistema
            LblMensaje.InnerText = exes.mensaje
            LblMensaje.DataBind()
        Catch exe As Exception
            LblMensaje.InnerText = exe.Message
            LblMensaje.DataBind()
        End Try
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    <WebMethod> Public Shared Function obtenerTodosLosTiposDePago() As List(Of MetodoDePago)
        Return GestorOrdenes.instancia().obtenerTodosLosMetodosDePago()
    End Function

    <WebMethod> Public Shared Function obtenerMetodoDePagoPorId(id As Integer) As MetodoDePago
        Return GestorABM.instancia().obtenerMetodoDePagoPorId(id)
    End Function

    <WebMethod> Public Shared Function obtenerRegexDeTarjetaPorIdMetodo(id As Integer)
        Return GestorDePagos.instancia().obtenerRegexPorMetodoDePago(id)
    End Function

    <WebMethod> Public Shared Function calcularMontoConNotaDeCredito(total As Double, nroNotaDeCredito As Integer) As Double
        Return GestorDePagos.instancia().calcularMontoConNotaDeCredito(nroNotaDeCredito, total)
    End Function

    <WebMethod> Public Shared Function validarDatosDeTarjeta(info As InformacionDePago) As Boolean
        If String.IsNullOrEmpty(info.nroDeTarjeta) Then
            Return True
        Else
            ValidacionHelper.instancia().validarTarjeta(info.nroDeTarjeta, info.metodo.id)
            If Not String.IsNullOrEmpty(info.titular) Then
                ValidacionHelper.instancia().validatTitularDeTarjeta(info.titular, info.nroDeTarjeta, info.metodo.id)
            End If
            If Not String.IsNullOrEmpty(info.cvv) Then
                ValidacionHelper.instancia().validarCvv(info.cvv, info.nroDeTarjeta, info.metodo.id)
            End If
                ValidacionHelper.instancia().validarFechaDeExpiracion(info.mesVencimiento, info.anioVencimiento, info.nroDeTarjeta, info.metodo.id)
        End If
        Return True
    End Function

End Class