Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarDevolucionDeProductos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Not Me.IsPostBack Then
                Dim lista As List(Of DevolucionDeProducto) = GestorABM.instancia().obtenerTodasLasDevolucionesActivas()
                LStDevoluciones.DataSource = lista
                LStDevoluciones.DataTextField = "nroDeDevolucion"
                LStDevoluciones.DataValueField = "nroDeDevolucion"
                LStDevoluciones.DataBind()
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


    Protected Sub LStDevoluciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LStDevoluciones.SelectedIndexChanged
        Dim devolucion As DevolucionDeProducto = GestorABM.instancia().buscarDevolucionDeProducto(LStDevoluciones.SelectedValue)
        id.Value = devolucion.nroDeDevolucion
        motivo.Value = devolucion.motivo
        fecha.Value = devolucion.fecha
        nroSerie.Value = devolucion.producto.nroDeSerie
        monto.Value = devolucion.monto
    End Sub
End Class