Imports System.Web.Services
Imports Negocio
Imports NegocioYServicios
Imports Seguridad

Public Class BusquedaFormulario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
    End Sub

    <WebMethod> Public Shared Function buscar(criterio As String) As List(Of String)
        Return GestorSeguridad.instancia().buscarFormularios(criterio)
    End Function

End Class