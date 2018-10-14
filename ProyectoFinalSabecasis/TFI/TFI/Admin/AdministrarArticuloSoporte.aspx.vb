Imports Modelo
Imports System.Web.Services
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarArticuloSoporte
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


    <WebMethod> Public Shared Function guardar(oArticulo As ArticuloSoporte) As String
        Return GestorABM.instancia().guararArticulo(oArticulo)
    End Function

    <WebMethod> Public Shared Function eliminar(id As Integer) As String
        Return GestorABM.instancia().eliminarArticulo(id)
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosArticulos() As List(Of ArticuloSoporte)
        Return GestorABM.instancia().obtenerArticulos()
    End Function
End Class