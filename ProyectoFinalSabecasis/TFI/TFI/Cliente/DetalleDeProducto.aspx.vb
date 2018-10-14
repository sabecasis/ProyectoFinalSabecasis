Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad
Imports System.IO

Public Class DetalleDeProducto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Me.IsPostBack AndAlso Not String.IsNullOrEmpty(Request.Form("nroFactura")) Then
                Dim nro As Integer = Convert.ToInt32(Request.Form("nroFactura"))
                Dim oFactura As Factura = GestorOrdenes.instancia().obtenerFacturaPorNroDeSerie(nro)
                If (Not oFactura Is Nothing) AndAlso (Not oFactura.comprobante Is Nothing) Then
                    Response.Clear()
                    Dim ms As New MemoryStream(oFactura.comprobante)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=factura.pdf")
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
        breadcrums.InnerHtml = Session("cadenabreadcrums")

    End Sub

    <WebMethod> Public Shared Function obtenerInformacionDeProducto(nroDeSerie As Integer) As ProductoEspecificoEnStock
        Return GestorStock.instancia().obtenerProductoEspecificoEnStock(nroDeSerie)
    End Function

    <WebMethod> Public Shared Function obtenerEnvioDeProducto(nroDeSerie As Integer) As Envio
        Return GestorOrdenes.instancia().obtenerEnvioDeProducto(nroDeSerie)
    End Function

    <WebMethod> Public Shared Function obtenerInformacionDeFactura(nroDeSerie As Integer) As Factura
        Return GestorOrdenes.instancia().obtenerFacturaPorNroDeSerie(nroDeSerie)
    End Function

    <WebMethod> Public Shared Function cambiarProducto(nroDeSerie As Integer, nroDeFactura As Integer) As String
        Dim nroDevolucion As Integer = GestorOrdenes.instancia().cambiarProducto(nroDeSerie, nroDeFactura)
        Dim resultado = ConstantesDeMensaje.CAMBIO_DE_PRODUCTO.Replace("?", nroDevolucion.ToString())
        Return resultado
    End Function
End Class