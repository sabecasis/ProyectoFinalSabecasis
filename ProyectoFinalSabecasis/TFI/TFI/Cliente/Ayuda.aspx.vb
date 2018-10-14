Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class Ayuda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
    End Sub

    <WebMethod> Public Shared Function obtenerTodosLosArticulos() As List(Of ArticuloSoporte)
        Return GestorABM.instancia().obtenerArticulos()
    End Function
End Class