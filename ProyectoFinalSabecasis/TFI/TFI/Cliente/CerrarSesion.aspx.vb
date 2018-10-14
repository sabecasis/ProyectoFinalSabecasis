Imports Negocio
Imports Seguridad

Public Class CerrarSesion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                If Not Request.Cookies("usuario_" & oSesion.usuario.nombre) Is Nothing Then
                    Dim cookie = Request.Cookies("usuario")
                    Try
                        UserCache.instancia().mapa.Remove(cookie.Value)
                    Catch ex As Exception
                    End Try
                    Response.Cookies("usuario_" & oSesion.usuario.nombre).Expires = DateTime.Now.AddDays(-1D)
                    Session.Abandon()
                End If
                Response.Redirect(urlanterior.Value)
            End If

        End If
    End Sub

End Class