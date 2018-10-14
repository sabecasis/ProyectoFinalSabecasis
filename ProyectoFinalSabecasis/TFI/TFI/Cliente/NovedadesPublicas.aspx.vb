Imports Modelo
Imports NegocioYServicios

Public Class NovedadesPublicas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim listaNovedades As List(Of Novedad) = GestorABM.instancia().obtenerTodasLasNovedades()
        Dim content As String = ""
        Dim urlRedireccion As String = ""
        For Each oNovedad As Novedad In listaNovedades
            Dim urlImagen As String = ""
            Dim xmlElem = XElement.Parse(oNovedad.configuracionAddRotator)
            For Each oElemento As XElement In xmlElem.Elements.AsEnumerable
                If oElemento.Name.LocalName.Equals("ImageUrl") Then
                    urlImagen = oElemento.Value
                End If
                If oElemento.Name.LocalName.Equals("NavigateUrl") Then
                    urlRedireccion = oElemento.Value
                End If
            Next
            Dim imagen As String = "<div class=""row""><a href=""" & urlRedireccion & """><img src=""" & urlImagen & """ class=""add-image""/></a></div><p/>"
            content = content & imagen
        Next
        contenido.InnerHtml = content
    End Sub

End Class