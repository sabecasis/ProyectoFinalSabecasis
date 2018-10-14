Imports System.Web.Services
Imports Modelo

Imports System.Globalization
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarIdiomas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    <WebMethod> Public Shared Function guardar(idioma As Idioma) As String
        Return GestorABM.instancia().guardarIdioma(idioma)
    End Function

    <WebMethod> Public Shared Function eliminar(id As Integer) As String
        Return GestorABM.instancia().eliminarIdioma(id)
    End Function

    <WebMethod> Public Shared Function obtenerCantidadDeElementos() As Integer
        Return GestorABM.instancia().obtenerCantidadDeElementosTraducibles()
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosElementosPaginados(desde As Integer, hasta As Integer) As List(Of Elemento)
        Return GestorABM.instancia().obtenerElementosPaginados(desde, hasta, 1)
    End Function

    <WebMethod> Public Shared Function obtenerElementosDeIdioma(id As Integer) As List(Of ElementoDeIdioma)
        Return GestorABM.instancia().obtenerElementosDeIdioma(id)
    End Function

    <WebMethod> Public Shared Function obtenerListaIdiomas() As List(Of Idioma)
        Return GestorABM.instancia().obtenerTodosLosIdiomas()
    End Function

    <WebMethod> Public Shared Function obtenerIdioma(id As Integer) As Idioma
        Return GestorABM.instancia().buscarIdioma(id)
    End Function

End Class