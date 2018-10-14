Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios

Public Class admin
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("idioma") Is Nothing Then
            Dim cookie As New HttpCookie("idioma")
            cookie.Value = 11274
            cookie.Expires = DateTime.MaxValue
            Response.Cookies.Add(cookie)
        End If
        Dim breadcrumlist As List(Of KeyValuePair(Of String, KeyValuePair(Of String, String)))
        Dim pagina As String = Request.Url.AbsolutePath.ToString().Substring(Request.Url.AbsolutePath.LastIndexOf("/") + 1)
        If Request.Url.AbsolutePath.ToString().Contains("/Cliente/") Then
            pagina = "/Cliente/" & pagina
        Else
            pagina = "/Admin/" & pagina
        End If
        Dim resultados As List(Of Permiso) = GestorSeguridad.instancia().buscarPermisosPorUrl(pagina)
        Dim idYLeyenda As New KeyValuePair(Of String, String)(resultados.ElementAt(0).elemento.nombre, resultados.ElementAt(0).elemento.leyendaPorDefecto)
        Dim values As New KeyValuePair(Of String, KeyValuePair(Of String, String))(Request.Url.AbsolutePath.ToString(), idYLeyenda)

        If (Session("breadcrums") Is Nothing) Then
            breadcrumlist = New List(Of KeyValuePair(Of String, KeyValuePair(Of String, String)))
            breadcrumlist.Add(values)
            Session("breadcrums") = breadcrumlist
        Else
            breadcrumlist = DirectCast(Session("breadcrums"), List(Of KeyValuePair(Of String, KeyValuePair(Of String, String))))
            If Not (breadcrumlist.ElementAt(breadcrumlist.Count - 1).Value.Value.Equals(resultados.ElementAt(0).elemento.leyendaPorDefecto)) Then
                breadcrumlist.Add(values)
            End If
        End If

        If breadcrumlist.Count > 5 Then
            breadcrumlist.RemoveAt(0)
        End If
        Dim cadenadebreadcrums As String = ""
        For Each obj As KeyValuePair(Of String, KeyValuePair(Of String, String)) In breadcrumlist
            cadenadebreadcrums = cadenadebreadcrums & "<a href='" & obj.Key & "' id='br-" & obj.Value.Key & "'>" & obj.Value.Value & "</a> >"
        Next
        cadenadebreadcrums = cadenadebreadcrums
        Session("cadenabreadcrums") = cadenadebreadcrums
    End Sub

End Class