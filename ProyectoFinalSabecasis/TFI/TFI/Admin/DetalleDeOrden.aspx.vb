Imports Seguridad
Imports Modelo
Imports NegocioYServicios

Public Class DetalleDeOrden
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Try
            Dim nroOrden As Integer = Request.Params("nroDeOrden")
            Dim oOrden As Orden = GestorOrdenes.instancia().obtenerOrdenPorNro(nroOrden)
            nombreusuario.Value = oOrden.usuario.nombre

            Dim sourceOrden As New List(Of Orden)
            sourceOrden.Add(oOrden)
            orden.DataSource = sourceOrden
            orden.DataBind()

            Dim vistaE As New VistaDeEnvio
            vistaE.estado = oOrden.envio.estado.estado
            vistaE.fecha = oOrden.envio.fecha.ToString("dd/MM/yyyy")
            vistaE.monto = oOrden.envio.monto.ToString("C")
            vistaE.nroEnvio = oOrden.envio.nroEnvio.ToString()
            vistaE.tipo = oOrden.envio.tipo.tipo
            Dim sourceEvio As New List(Of VistaDeEnvio)
            sourceEvio.Add(vistaE)
            envio.DataSource = sourceEvio
            envio.DataBind()

            Dim listaVistas As New List(Of VistaDetalleDeOrden)
            For Each oDetalle As Modelo.DetalleDeOrden In oOrden.detalles
                Dim vista As New VistaDetalleDeOrden
                vista.nombreProducto = oDetalle.producto.nombre
                vista.monto = oDetalle.monto
                vista.cantidad = oDetalle.cantidad
                listaVistas.Add(vista)
            Next
            detall.DataSource = listaVistas
            detall.DataBind()

            Dim vistaInformacionDePago As New VistaInformacionDePago
            vistaInformacionDePago.anioVencimiento = oOrden.informacionDePago.anioVencimiento
            vistaInformacionDePago.mesVencimiento = oOrden.informacionDePago.mesVencimiento
            vistaInformacionDePago.metodo = oOrden.informacionDePago.metodo.metodo
            vistaInformacionDePago.nroDeTarjeta = "************" & oOrden.informacionDePago.nroDeTarjeta.Substring(12)
            vistaInformacionDePago.nroNotaDeCredito = oOrden.informacionDePago.nroNotaDeCredito
            vistaInformacionDePago.titular = oOrden.informacionDePago.titular
            vistaInformacionDePago.cvv = oOrden.informacionDePago.cvv
            Dim sourceInfo As New List(Of VistaInformacionDePago)
            sourceInfo.Add(vistaInformacionDePago)
            informacionPago.DataSource = sourceInfo
            informacionPago.DataBind()


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
    End Sub

End Class