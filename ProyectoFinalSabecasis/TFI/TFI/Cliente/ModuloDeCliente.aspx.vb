Imports Seguridad
Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios

Public Class ModuloDeCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("sesion") Is Nothing Then
            idUsuario.Value = DirectCast(Session("sesion"), Sesion).usuario.id
            Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
            aPerfil.InnerText = oSesion.usuario.nombre
        Else
            idUsuario.Value = 0
            referrer.Value = "/Cliente/IniciarSesion.aspx"
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub


    <WebMethod> Public Shared Function obtenerProductosAdquiridos(idUsuario) As List(Of ProductoEspecificoEnStock)
        Return GestorStock.instancia().obtenerProductosEspecificosPorUsuario(idUsuario)
    End Function


End Class