Imports Negocio
Imports Modelo
Imports System.Web.Services
Imports Seguridad
Imports NegocioYServicios

Public Class RecuperarPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
    End Sub

    <WebMethod> Public Shared Function recuperarPassword(usuario As String) As String
        Dim oUsuario As Usuario = GestorABM.instancia().buscarUsuario(usuario)
        If Not oUsuario Is Nothing Then
            GestorSeguridad.instancia().enviarEmailRecuperarContrasenia(oUsuario.persona.contacto.email, Criptografia.ObtenerInstancia().CypherTripleDES(usuario, ConstantesDeSeguridad.ENC_KEY, True))
            Return ConstantesDeMensaje.EMAIL_PASSWORD_ENVIADO
        Else
            Return ConstantesDeMensaje.ERROR_NO_ENCONTRADO
        End If
    End Function

End Class