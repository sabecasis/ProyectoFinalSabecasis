Imports System.IO
Imports Modelo
Imports NegocioYServicios
Imports System.Threading.Tasks

Public Class RecargadorDeNovedades

    Public Async Function recargar(newsletterid As Integer, contexto As System.Web.HttpContext) As Task
        Await Task.Run(Sub()
                           'recargo por cada usuario
                           Dim listaNewsletters As List(Of Newsletter) = GestorABM.instancia().obtenerUsuariosDeNewsletter(newsletterid)
                           For Each news As Newsletter In listaNewsletters
                               File.Delete(contexto.Server.MapPath("~/static/add_xml/add1_" & news.usuario.id.ToString() & ".xml"))
                               Dim novedadesDeUsuario = GestorABM.instancia().obtenerTodasLasNovedadesPorUsuario(news.usuario.id)
                               Dim adds As String = ""
                               For Each oNovedad As Novedad In novedadesDeUsuario
                                   adds = String.Concat(adds, oNovedad.configuracionAddRotator)
                               Next
                               Dim contextoAdd As String = "<?xml version=""1.0"" encoding=""utf-8"" ?><Advertisements>xxx</Advertisements>"
                               contextoAdd = contextoAdd.Replace("xxx", adds)
                               Dim writer As New StreamWriter(contexto.Server.MapPath("~/static/add_xml/add1_" & news.usuario.id.ToString() & ".xml"), True)
                               writer.WriteLine(contextoAdd)
                               writer.Close()
                               writer.Dispose()
                           Next
                           'recargo el general para los usuarios anonimos
                           Dim listaNovedades As List(Of Novedad) = GestorABM.instancia().obtenerTodasLasNovedades()
                           Dim adds2 As String = ""
                           For Each oNovedad As Novedad In listaNovedades
                               adds2 = String.Concat(adds2, oNovedad.configuracionAddRotator)
                           Next
                           Dim contextoAdd2 As String = "<?xml version=""1.0"" encoding=""utf-8"" ?><Advertisements>xxx</Advertisements>"
                           contextoAdd2 = contextoAdd2.Replace("xxx", adds2)
                           Dim writer2 As New StreamWriter(contexto.Server.MapPath("~/static/add_xml/add_general.xml"), True)
                           writer2.WriteLine(contextoAdd2)
                           writer2.Close()
                           writer2.Dispose()
                       End Sub
        )
    End Function
End Class
