Imports System.Web.Services
Imports Modelo
Imports Negocio
Imports NegocioYServicios

Public Class Activar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    <WebMethod> Public Shared Sub desbloquear(usuario As String)
        Dim oUsuario As Usuario = GestorABM.instancia().buscarUsuario(usuario)
        If Not oUsuario Is Nothing Then
            oUsuario.bloqueado = False
            GestorABM.instancia().guardarUsuario(oUsuario)
        End If
    End Sub


End Class