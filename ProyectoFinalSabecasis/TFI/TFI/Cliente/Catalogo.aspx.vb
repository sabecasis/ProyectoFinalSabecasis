Imports Modelo
Imports System.Web.Services
Imports Seguridad
Imports NegocioYServicios
Imports System.IO

Public Class Catalogo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            MaintainScrollPositionOnPostBack = True
            If Session("sesion") Is Nothing Then
                usuarioId.Value = 0
                Try
                    Dim reader As New StreamReader(Server.MapPath("/static/add_xml/add_general.xml"))
                    Dim line As String
                    line = reader.ReadToEnd()
                    If line.Contains("Ad") Then
                        Add1.AdvertisementFile = "/static/add_xml/add_general.xml"
                        Add1.Width = 400
                        Add1.Height = 60
                    End If
                    reader.Close()
                    reader.Dispose()
                Catch ex As Exception
                End Try
            Else
                aPerfil.InnerText = DirectCast(Session("sesion"), Sesion).usuario.nombre
                usuarioId.Value = DirectCast(Session("sesion"), Sesion).usuario.id
                Try
                    Dim reader As New StreamReader(Server.MapPath("/static/add_xml/add1_" & DirectCast(Session("sesion"), Sesion).usuario.id.ToString() & ".xml"))
                    Dim line As String
                    line = reader.ReadToEnd()
                    If line.Contains("Ad") Then
                        Add1.AdvertisementFile = "/static/add_xml/add1_" & DirectCast(Session("sesion"), Sesion).usuario.id.ToString() & ".xml"
                        Add1.Width = 400
                        Add1.Height = 60
                    End If
                    reader.Close()
                    reader.Dispose()
                Catch ex As Exception
                End Try

            End If

            If Not Request.QueryString("check") Is Nothing Then
                crearCheckout()
            End If
            If Me.IsPostBack Then
                If ConstantesDeEvento.CHECKOUT.Equals(Request.Form("accion")) Then
                    crearCheckout()
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

    <WebMethod> Public Shared Function obtenerComentariosDeProducto(nroProducto As Integer) As List(Of ComentarioDeProducto)
        Return GestorOrdenes.instancia().obtenerComentariosDeProducto(nroProducto)
    End Function

    <WebMethod> Public Shared Function eliminarComentario(comentId As Integer) As Boolean
        Return GestorOrdenes.instancia().eliminarComentarioDeProducto(comentId)
    End Function

    <WebMethod> Public Shared Function obtenerProducto(prod As Producto) As Producto
        Return GestorABM.instancia().buscarProducto(prod)
    End Function

    <WebMethod> Public Shared Function crearComentarioDeProducto(oComentario As ComentarioDeProducto) As Boolean
        Return GestorOrdenes.instancia().crearComentarioDeProducto(oComentario)
    End Function

    <WebMethod> Public Shared Sub votarProducto(valoracion As ValoracionDeProducto)
        GestorOrdenes.instancia().valorarProducto(valoracion)
    End Sub

    Private Function crearCheckout() As Boolean
        Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
        Dim checkoutProv As Dictionary(Of String, Modelo.Checkout) = UserCache.instancia().checkoutsIntermedios
        Dim idSesion As String = Request.QueryString("check")
        Dim checkout As New Modelo.Checkout
        Dim parametros As String = Request.Form("productos")
        Dim arrayParam As String()

        If idSesion Is Nothing Then
            checkout.productos = New Dictionary(Of Integer, Integer)
            If Not parametros Is Nothing Then
                arrayParam = parametros.Split(",")
                Dim nroDeProducto As Integer
                Dim oDisp As DisponibilidadEnStock
                Dim cantidad As Integer = 0
                For i As Integer = 0 To arrayParam.Length - 1
                    nroDeProducto = Convert.ToInt32(arrayParam(i))
                    oDisp = GestorStock.instancia().consultarStock(nroDeProducto, 1)
                    If oDisp Is Nothing Then
                        Throw New ExcepcionDeValidacion("No hay stock suficiente para comprar uno de los productos seleccionados")
                    End If
                    cantidad = Convert.ToInt32(Request.Form("cantidad" & arrayParam(i)))
                    If oDisp.cantidad >= cantidad Then
                        checkout.productos.Add(nroDeProducto, cantidad)
                    Else
                        Return False
                    End If
                Next
            End If

            checkout.envio = New Envio
            checkout.estado = New Estado
            checkout.estado.id = 1
            checkout.fecha = Date.Now

        ElseIf Not UserCache.instancia().checkoutsIntermedios Is Nothing Then
            If UserCache.instancia().checkoutsIntermedios.Count > 0 Then
                checkout = UserCache.instancia().checkoutsIntermedios.Item(idSesion)
            End If
        End If

        If Not oSesion Is Nothing Then
            checkout.usuario = DirectCast(Session("sesion"), Sesion).usuario
            Dim oCheckoutAct As Modelo.Checkout = DirectCast(Session("sesion"), Sesion).checkout
            If oCheckoutAct Is Nothing Then
                DirectCast(Session("sesion"), Sesion).checkout = GestorOrdenes.instancia().guardarCheckout(checkout)
            Else
                'Si el checkout ya existia en la sesion, agregar los productos q selecciono o modifico el cliente
                Dim cantProd As Integer
                Dim nuevaCant As Integer = 0
                If Not arrayParam Is Nothing Then
                    If arrayParam.Count > 0 Then
                        For i As Integer = 0 To arrayParam.Length - 1
                            Try
                                cantProd = oCheckoutAct.productos.Item(Convert.ToInt32(arrayParam(i)))
                            Catch ex As Exception
                                cantProd = 0
                            End Try
                            If cantProd <> 0 Then
                                nuevaCant = Convert.ToInt32(Request.Form("cantidad" & arrayParam(i)))
                                oCheckoutAct.productos.Item(Convert.ToInt32(arrayParam(i))) = cantProd + nuevaCant
                            Else
                                oCheckoutAct.productos.Add(Convert.ToInt32(arrayParam(i)), Convert.ToInt32(Request.Form("cantidad" & arrayParam(i))))
                            End If
                            DirectCast(Session("sesion"), Sesion).checkout = GestorOrdenes.instancia().guardarCheckout(oCheckoutAct)
                        Next
                    End If
                End If
            End If

            Response.Redirect("/Cliente/Checkout.aspx")
        Else
            If checkoutProv Is Nothing Then
                checkoutProv = New Dictionary(Of String, Modelo.Checkout)
                UserCache.instancia().checkoutsIntermedios = checkoutProv
            End If
            Dim key As String = Math.Floor(DateTime.Now.Subtract(New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString
            checkoutProv.Add(key, checkout)
            Response.Redirect("/Cliente/IniciarSesion.aspx?check=" & key)
        End If
        Return True
    End Function

    <WebMethod> Public Shared Function obtenerProductosPorCatalogo(idCatalogo As Integer, precio As Integer, estado As Boolean) As List(Of Producto)
        Return GestorOrdenes.instancia().obtenerProductosActivosDeCatalogo(idCatalogo, precio, estado)
    End Function
End Class