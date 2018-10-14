Imports System.Web.Services
Imports Negocio
Imports Modelo
Imports Seguridad
Imports NegocioYServicios
Imports System.IO

Public Class Novedades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Session("sesion") Is Nothing Then
                Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
                    aPerfil.InnerText = oSesion.usuario.nombre
                Dim oUsuario As Usuario = DirectCast(Session("sesion"), Sesion).usuario
                usuarioId.Value = oUsuario.id
                If Me.IsPostBack And ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                    File.Delete(Server.MapPath("~/static/add_xml/add1_" & oUsuario.id.ToString() & ".xml"))
                    Dim novedadesDeUsuario = GestorABM.instancia().obtenerTodasLasNovedadesPorUsuario(oUsuario.id)
                    Dim adds As String = ""
                    For Each oNovedad As Novedad In novedadesDeUsuario
                        adds = String.Concat(adds, oNovedad.configuracionAddRotator)
                    Next
                    Dim contextoAdd As String = "<?xml version=""1.0"" encoding=""utf-8"" ?><Advertisements>xxx</Advertisements>"
                    contextoAdd = contextoAdd.Replace("xxx", adds)
                    Dim writer As New StreamWriter(Server.MapPath("~/static/add_xml/add1_" & oUsuario.id.ToString() & ".xml"), True)
                    writer.WriteLine(contextoAdd)
                    writer.Close()
                    writer.Dispose()

                End If
            Else
                referrer.Value = "/Cliente/IniciarSesion.aspx"
            End If
        Catch ex As ExcepcionDeValidacion
            LblMensaje.InnerText = ex.Message
            LblMensaje.DataBind()
        Catch exes As ExcepcionDelSistema
            LblMensaje.InnerText = exes.mensaje
            LblMensaje.DataBind()
        Catch exe As Exception
            LblMensaje.InnerText = exe.Message
            LblMensaje.DataBind()
        End Try
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    <WebMethod> Public Shared Function guardar(id As Integer, newsletters As String()) As String
        Return GestorABM.instancia().guardarNewsletterDeUsuario(id, newsletters)
    End Function

    <WebMethod> Public Shared Function obtenerNewsDeUsuario(id As Integer) As List(Of Newsletter)
        Return GestorABM.instancia().obtenerNewsletterDeUsuario(id)
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosNewsletter() As List(Of Newsletter)
        Return GestorABM.instancia().obtenerTodosLosNewsletter()
    End Function

    <WebMethod> Public Shared Function obtenerTodasLasNovedadesPorUsuario(idUsuario As Integer) As List(Of Novedad)
        Return GestorABM.instancia().obtenerTodasLasNovedadesPorUsuario(idUsuario)
    End Function
End Class