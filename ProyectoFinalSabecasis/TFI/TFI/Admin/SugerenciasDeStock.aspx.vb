
Imports Modelo
Imports System.Web.Services
Imports NegocioYServicios
Imports Seguridad

Public Class SugerenciasDeStock
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
                    Dim sugerencia As New SugerenciaDeProducto
                    sugerencia.productoComprado = New Producto
                    sugerencia.productoComprado.nroDeProducto = Request.Form("producto")
                    sugerencia.productoSugerido = New Producto
                    sugerencia.productoSugerido.nroDeProducto = Request.Form("sugerido")
                    GestorABM.instancia().crearSugerencia(sugerencia)
                ElseIf ConstantesDeEvento.ELIMINAR.Equals(Request.Form("accion")) Then
                    Dim sugerencia As New SugerenciaDeProducto
                    sugerencia.productoComprado = New Producto
                    sugerencia.productoComprado.nroDeProducto = Request.Form("producto")
                    sugerencia.productoSugerido = New Producto
                    sugerencia.productoSugerido.nroDeProducto = Request.Form("sugerido")
                    GestorABM.instancia().eliminarSugerencia(sugerencia)
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

    <WebMethod> Public Shared Function cargarProductos() As List(Of Producto)
        Return GestorABM.instancia().obtenerTodosLosProductos()
    End Function
End Class