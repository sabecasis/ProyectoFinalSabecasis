Imports Seguridad

Public Class MensajeDeRegistro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
    End Sub

End Class