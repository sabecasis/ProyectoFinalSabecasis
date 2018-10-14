Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class DetallesDeNovedad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            Dim nombre = Request.Params("id")
            If Not nombre Is Nothing Then
                Dim novedad As Novedad = GestorABM.instancia().obtenerNovedadPorNombre(nombre)
                If Not novedad Is Nothing Then
                    contenido.InnerHtml = novedad.novedad
                    titulo.InnerHtml = "<h1>" & novedad.titulo & "</h1>"
                    cabecera.Style.Add("background-image", "url('/static/add_img/novedad_" & novedad.nombre.Trim() & "_grande.png')")
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

End Class