Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class ConfirmacionDeOrden
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
    End Sub

    <WebMethod> Public Shared Function obtenerEncuestasPorTipo(idTipo As Integer) As List(Of Encuesta)
        Return GestorOrdenes.instancia().obtenerEncuestasPorTipo(idTipo, "", "")
    End Function

    <WebMethod> Public Shared Function actualizarOpcion(opt As OpcionDeEncuesta) As Boolean
        Return GestorOrdenes.instancia().actualizarOpcionEncuesta(opt)
    End Function

End Class