Imports System.Web.Services

Imports Modelo
Imports Seguridad
Imports NegocioYServicios
Imports System.IO

Public Class IngresoDeStock
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre

            End If
            breadcrums.InnerHtml = Session("cadenabreadcrums")
            If ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                Dim oIngreso As New Modelo.IngresoDeStock
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("cantidad"), "cantidad")
                ValidacionHelper.instancia().validarNoCero(Request.Form("cantidad"), "cantidad")
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("fecha"), "fecha")
                oIngreso.nroIngreso = Convert.ToInt32(Request.Form("id"))
                oIngreso.cantidad = Convert.ToInt32(Request.Form("cantidad"))
                oIngreso.fecha = Request.Form("fecha")
                oIngreso.producto = New Producto
                oIngreso.producto.nroDeProducto = Convert.ToInt32(Request.Form("producto"))
                oIngreso.solicitud = New Modelo.SolicitudDeStock
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("solicitud"), "solicitud")
                oIngreso.solicitud.nroPedido = Convert.ToInt32(Request.Form("solicitud"))
                oIngreso.sucursal = New Sucursal
                oIngreso.sucursal.nroSucursal = Convert.ToInt32(Request.Form("sucursal"))
                oIngreso.usuario = DirectCast(Session("sesion"), Sesion).usuario
                If Not String.IsNullOrEmpty(Request.Form("preciocompra")) Then
                    oIngreso.precioDeCompra = Convert.ToDouble(Request.Form("preciocompra"))
                End If
                GestorStock.instancia().guardarIngresoDeStock(oIngreso)
            ElseIf ConstantesDeEvento.ELIMINAR.Equals(Request.Form("accion")) Then
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                Dim oIngreso As New Modelo.IngresoDeStock
                oIngreso.nroIngreso = Convert.ToInt32(Request.Form("id"))
                GestorStock.instancia().eliminarIngresoDeStock(oIngreso)
            ElseIf ConstantesDeEvento.COMPROBANTE.Equals(Request.Form("accion")) Then
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                Dim idIngreso As Integer = Convert.ToInt32(Request.Form("id"))
                Dim oIgreso As Modelo.IngresoDeStock = GestorStock.instancia().buscarIngresoDeStock(idIngreso)
                If (Not oIgreso Is Nothing) AndAlso (Not oIgreso.comprobante Is Nothing) Then
                    Response.Clear()
                    Dim ms As New MemoryStream(oIgreso.comprobante)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=ingreso.pdf")
                    Response.Buffer = True
                    ms.WriteTo(Response.OutputStream)
                    Response.End()
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
    End Sub


    <WebMethod> Public Shared Function cargarSucursales() As List(Of Sucursal)
        Return GestorABM.instancia().obtenerTodasLasSucursales()
    End Function

    <WebMethod> Public Shared Function cargarProductos() As List(Of Producto)
        Return GestorABM.instancia().obtenerTodosLosProductos()
    End Function


    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdIngresoStock()
    End Function

    <WebMethod> Public Shared Function obtenerSolicitudesActivas(nroSucursal As Integer, nroProducto As Integer) As List(Of Modelo.SolicitudDeStock)
        Return GestorStock.instancia().obtenerSolicitudesDeStockActivas(nroSucursal, nroProducto)
    End Function

    <WebMethod> Public Shared Function buscar(id As Integer) As Modelo.IngresoDeStock
        Return GestorStock.instancia().buscarIngresoDeStock(id)
    End Function
End Class