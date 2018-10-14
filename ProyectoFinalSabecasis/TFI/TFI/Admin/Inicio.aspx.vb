Imports Seguridad

Public Class AdminLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        Else
            referrer.Value = "/Cliente/IniciarSesion.aspx"
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

End Class