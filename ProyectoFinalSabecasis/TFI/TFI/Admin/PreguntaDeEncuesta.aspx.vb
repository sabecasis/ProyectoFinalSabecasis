Imports System.Web.Services

Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class PreguntaDeEncuesta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        LblMensaje.InnerText = ""
        Try
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
    <WebMethod> Public Shared Function guardar(oEncuesta As Encuesta) As String
        Return GestorABM.instancia().guardarEncuesta(oEncuesta)
    End Function

    <WebMethod> Public Shared Function buscar(id As Integer) As Modelo.PreguntaDeEncuesta
        ValidacionHelper.instancia().validarCampoVacio(id, "id")
        Return GestorABM.instancia().obtenerPreguntaDeEncuestaPorId(id, "", "")
    End Function

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdPreguntaencuesta()
    End Function
End Class