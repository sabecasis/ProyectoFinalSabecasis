Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class BitacoraDeUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If

        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub


    <WebMethod> Public Shared Function obtenerEventos() As List(Of Evento)
        Return GestorABM.instancia().obtenerTodosLosEventos()
    End Function

    <WebMethod> Public Shared Function buscar(elemento As ElementoDeBitacora) As List(Of ElementoDeBitacora)
        Return GestorLogs.instancia().obtenerBitacora(elemento)
    End Function
End Class