Imports Seguridad
Imports Modelo
Imports Negocio
Imports System.Web.Services
Imports NegocioYServicios

Public Class ChatDeSoporteAlCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Session("sesion") Is Nothing Then
                Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
                idUsuario.Value = oSesion.usuario.id
                Dim sesionChat As New SesionDeChat
                sesionChat.usuario = oSesion.usuario
                sesionChat.estado = New Estado
                sesionChat.estado.id = 1
                Dim sesionChaEnCola As New SesionDeChatEnCola
                sesionChaEnCola.estado = New Estado
                sesionChaEnCola.estado.id = 1
                sesionChaEnCola.sesion = sesionChat
                Dim resultado As Boolean = GestorUsuario.instancia().crearSesionDeChat(sesionChaEnCola)
                If resultado Then
                    nroconsulta.Text = sesionChaEnCola.sesion.id
                End If
                aPerfil.InnerText = oSesion.usuario.nombre
            Else
                nroconsulta.Text = 0
                referrer.Value = "/Cliente/IniciarSesion.aspx"
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

    <WebMethod> Public Shared Function obtenerMensajes(idSesionChat As Integer) As ComentarioDeChat
        Return GestorUsuario.instancia().obtenerUltimoMensaje(idSesionChat)
    End Function

    <WebMethod> Public Shared Function buscarRespuestaEnCola(idSesionChat As Integer) As SesionDeChatEnCola
        Return GestorUsuario.instancia().buscarSesionDeChat(idSesionChat)
    End Function

    <WebMethod> Public Shared Function enviarComentario(idUsuario As Integer, comentario As String, idSesion As Integer) As Boolean
        Return GestorUsuario.instancia().agregarComentarioDeChat(idUsuario, comentario, idSesion)
    End Function


End Class