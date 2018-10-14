Imports System.Web.Services
Imports Modelo
Imports Seguridad
Imports NegocioYServicios

Public Class ChatConCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("sesion") Is Nothing Then
            Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
            idUsuario.Value = oSesion.usuario.id
            aPerfil.InnerText = oSesion.usuario.nombre
        Else
            nroconsulta.Text = 0
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub


    <WebMethod> Public Shared Function obtenerMensajes(idSesionChat As Integer) As ComentarioDeChat
        Return GestorUsuario.instancia().obtenerUltimoMensaje(idSesionChat)
    End Function

    <WebMethod> Public Shared Function buscarRespuestaEnCola(idUsuario As Integer) As SesionDeChatEnCola
        Return GestorUsuario.instancia().buscarSesionDeChatInactiva(idUsuario)
    End Function

    <WebMethod> Public Shared Function enviarComentario(idUsuario As Integer, comentario As String, idSesion As Integer) As Boolean
        Return GestorUsuario.instancia().agregarComentarioDeChat(idUsuario, comentario, idSesion)
    End Function

    <WebMethod> Public Shared Function finalizarSesion(idSesion As Integer, idUsuario As Integer) As Boolean
        Return GestorUsuario.instancia().finalizarSesionDeChat(idSesion, idUsuario)
    End Function
End Class