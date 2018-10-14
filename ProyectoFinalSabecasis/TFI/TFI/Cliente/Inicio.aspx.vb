Imports Negocio
Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class Inicio
    Inherits System.Web.UI.Page

    Property preguntas As List(Of Modelo.PreguntaDeEncuesta)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            MaintainScrollPositionOnPostBack = True
            If Me.IsPostBack Then
                Dim oMensaje As New Modelo.MensajeDeConsulta
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("nombre"), "nombre")
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("email"), "email")
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("mensaje"), "mensaje")
                oMensaje.identificacion = Request.Form("nombre")
                oMensaje.email = Request.Form("email")
                oMensaje.mensaje = Request.Form("mensaje")
                mensajeRespuesta.Text = GestorABM.instancia().guardarMensajeConsulta(oMensaje)
            End If
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
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

    <WebMethod> Public Shared Function obtenerEncuestasPorTipo(idTipo As Integer) As List(Of Encuesta)
        Return GestorOrdenes.instancia().obtenerEncuestasPorTipo(idTipo, "", "")
    End Function

    <WebMethod> Public Shared Function actualizarOpcion(opt As OpcionDeEncuesta) As Boolean
        Return GestorOrdenes.instancia().actualizarOpcionEncuesta(opt)
    End Function


    <WebMethod> Public Shared Function obtenerPreguntasDeEncuesta() As List(Of Modelo.PreguntaDeEncuesta)
        Return GestorUsuario.instancia().obtenerPreguntasDeEncuestaPorTipo(1, "", "")
    End Function


End Class
