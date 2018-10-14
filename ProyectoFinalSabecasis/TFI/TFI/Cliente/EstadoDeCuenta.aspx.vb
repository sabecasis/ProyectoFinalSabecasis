Imports Seguridad
Imports System.Web.Services
Imports NegocioYServicios

Public Class EstadoDeCuenta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("sesion") Is Nothing Then
            nombreUsuario.Value = DirectCast(Session("sesion"), Sesion).usuario.nombre
            aPerfil.InnerText = DirectCast(Session("sesion"), Sesion).usuario.nombre
        Else
            referrer.Value = "/Cliente/IniciarSesion.aspx"
            nombreUsuario.Value = ""
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    <WebMethod> Public Shared Function obtenerEstadoDeCuenta(nombreUsuario As String) As Modelo.EstadoDeCuenta
        Return GestorUsuario.instancia().obtenerEstadoDecuenta(nombreUsuario)
    End Function

End Class