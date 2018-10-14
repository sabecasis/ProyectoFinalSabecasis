Imports System.Web.Services
Imports Negocio
Imports Modelo
Imports Seguridad
Imports NegocioYServicios
Imports System.IO

Public Class EgresoDeStock
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.IsPostBack Then
                If ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                    ValidacionHelper.instancia().validarNoCero(Request.Form("id"), "id")
                    Dim oEgreso As New Modelo.EgresoDeStock
                    oEgreso.nroEgreso = Convert.ToInt32(Request.Form("id"))
                    oEgreso.motivo = Request.Form("motivo")
                    oEgreso.estado = New Estado
                    oEgreso.estado.id = Request.Form("estado")
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("fecha"), "fecha")
                    oEgreso.fecha = Request.Form("fecha")
                    oEgreso.productos = New List(Of Producto)
                    Dim oProd As New Producto
                    oProd.nroDeProducto = Convert.ToInt32(Request.Form("producto"))
                    oEgreso.productos.Add(oProd)
                    oEgreso.sucursal = New Sucursal()
                    oEgreso.sucursal.nroSucursal = Convert.ToInt32(Request.Form("sucursal"))
                    oEgreso.usuario = DirectCast(Session("sesion"), Sesion).usuario
                    Dim prodEspecificos As String = Request.Form("productosEspecificos")
                    Dim prodEspList As String() = prodEspecificos.Split(",")
                    Dim listaDeProductosEspecificosDeEgreso As New List(Of ProductoEspecificoEnStock)
                    Dim prodEsp As ProductoEspecificoEnStock
                    oEgreso.cantidad = prodEspList.Count
                    For i As Integer = 0 To prodEspList.Count - 1
                        prodEsp = New ProductoEspecificoEnStock
                        prodEsp.nroDeSerie = prodEspList(i)
                        listaDeProductosEspecificosDeEgreso.Add(prodEsp)
                    Next
                    oEgreso.productosEspecificosEnStock = listaDeProductosEspecificosDeEgreso
                    GestorStock.instancia().guardarEgresoDeStock(oEgreso)
                ElseIf ConstantesDeEvento.ELIMINAR.Equals(Request.Form("accion")) Then
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                    Dim oEgreso As New Modelo.EgresoDeStock
                    oEgreso.nroEgreso = Request.Form("id")
                    GestorStock.instancia().eliminarEgresoDeStock(oEgreso)
                ElseIf ConstantesDeEvento.COMPROBANTE.Equals(Request.Form("accion")) Then
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                    Dim idEgreso As Integer = Convert.ToInt32(Request.Form("id"))
                    Dim oEgreso As Modelo.EgresoDeStock = GestorStock.instancia().buscarEgresoDeStock(idEgreso)
                    If (Not oEgreso Is Nothing) AndAlso (Not oEgreso.comprobante Is Nothing) Then
                        Response.Clear()
                        Dim ms As New MemoryStream(oEgreso.comprobante)
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-disposition", "attachment;filename=egreso.pdf")
                        Response.Buffer = True
                        ms.WriteTo(Response.OutputStream)
                        Response.End()
                    End If
                End If
          
            End If
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre

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


    <WebMethod> Public Shared Function obtenerInstaciasEspecificas(nroSucursal As Integer, nroProducto As Integer, cantidad As Integer) As List(Of ProductoEspecificoEnStock)
        Return GestorStock.instancia().buscarInstanciasEspecificasParaEgreso(nroSucursal, nroProducto, cantidad)
    End Function

    <WebMethod> Public Shared Function buscar(nroEgreso As Integer) As Modelo.EgresoDeStock
        Return GestorStock.instancia().buscarEgresoDeStock(nroEgreso)
    End Function

    <WebMethod> Public Shared Function cargarSucursales() As List(Of Sucursal)
        Return GestorABM.instancia().obtenerTodasLasSucursales()
    End Function

    <WebMethod> Public Shared Function cargarProductos() As List(Of Producto)
        Return GestorABM.instancia().obtenerTodosLosProductos()
    End Function

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdEgresoDeStock()
    End Function

    <WebMethod> Public Shared Function obtenerEstados() As List(Of Estado)
        Return GestorStock.instancia().obtenerTodosLosEstadosDeEgreso()
    End Function
End Class