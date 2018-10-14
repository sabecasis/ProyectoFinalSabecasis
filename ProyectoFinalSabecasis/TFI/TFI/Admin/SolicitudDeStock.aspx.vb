Imports Modelo
Imports System.Web.Services

Imports Seguridad
Imports NegocioYServicios
Imports System.IO

Public Class SolicitudDeStock
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre

            End If
            breadcrums.InnerHtml = Session("cadenabreadcrums")
            If Me.IsPostBack Then
                If ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                    Dim oSolicitud = New Modelo.SolicitudDeStock
                    oSolicitud.cantidad = Convert.ToInt32(Request.Form("cantidad"))
                    If Request.Form("completado") Is Nothing Then
                        oSolicitud.estaIngersado = False
                    Else
                        oSolicitud.estaIngersado = True
                    End If
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("fecha"), "fecha")
                    If Not String.IsNullOrEmpty(Request.Form("fecha")) Then
                        oSolicitud.fecha = Request.Form("fecha")
                    End If
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                    ValidacionHelper.instancia().validarNoCero(Request.Form("id"), "id")
                    oSolicitud.nroPedido = Request.Form("id")
                    oSolicitud.producto = New Producto
                    oSolicitud.producto.nroDeProducto = Convert.ToInt32(Request.Form("producto"))
                    oSolicitud.sucursal = New Sucursal
                    oSolicitud.sucursal.nroSucursal = Convert.ToInt32(Request.Form("sucursal"))
                    oSolicitud.usuario = DirectCast(Session("sesion"), Sesion).usuario
                    GestorStock.instancia().guardarSolicitudDeStock(oSolicitud)
                ElseIf ConstantesDeEvento.ELIMINAR.Equals(Request.Form("accion")) Then
                    Dim oSolicitud = New Modelo.SolicitudDeStock
                    oSolicitud.nroPedido = Convert.ToInt32(Request.Form("id"))
                    GestorStock.instancia().eliminarSolicitudDeStock(oSolicitud)
                ElseIf ConstantesDeEvento.COMPROBANTE.Equals(Request.Form("accion")) Then
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                    Dim idSolicitud As Integer = Convert.ToInt32(Request.Form("id"))
                    Dim oSolicitud As Modelo.SolicitudDeStock = GestorStock.instancia().buscarSolicitudDeStock(idSolicitud)
                    If (Not oSolicitud Is Nothing) AndAlso (Not oSolicitud.comprobante Is Nothing) Then
                        Response.Clear()
                        Dim ms As New MemoryStream(oSolicitud.comprobante)
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-disposition", "attachment;filename=pedido.pdf")
                        Response.Buffer = True
                        ms.WriteTo(Response.OutputStream)
                        Response.End()
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
    End Sub

    <WebMethod> Public Shared Function buscar(nroSolicitud As Integer) As Modelo.SolicitudDeStock
        Return GestorStock.instancia().buscarSolicitudDeStock(nroSolicitud)
    End Function

    <WebMethod> Public Shared Function cargarSucursales() As List(Of Sucursal)
        Return GestorABM.instancia().obtenerTodasLasSucursales()
    End Function

    <WebMethod> Public Shared Function cargarProductos() As List(Of Producto)
        Return GestorABM.instancia().obtenerTodosLosProductos()
    End Function

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdSolicitudStock()
    End Function
End Class