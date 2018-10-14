Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class CargarEncuestasCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.IsPostBack Then
                If ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                    Dim oEncuesta As New Encuesta
                    ValidacionHelper.instancia().validarCampoVacio(descripcion.Value, "descripcion")
                    ValidacionHelper.instancia().validarCampoVacio(fechaDesde.Value, "fecha desde")
                    ValidacionHelper.instancia().validarCampoVacio(fechaHasta.Value, "fecha hasta")
                    ValidacionHelper.instancia().validarCampoVacio(nombre.Value, "nombre")
                    oEncuesta.id = id.Value
                    oEncuesta.descripcion = descripcion.Value
                    oEncuesta.nombre = nombre.Value
                    oEncuesta.tipo = New TipoDeEncuesta
                    oEncuesta.tipo.id = Convert.ToInt32(tipo.Value)
                    oEncuesta.fechaDesde = fechaDesde.Value
                    oEncuesta.fechaHasta = fechaHasta.Value
                    GestorABM.instancia().guardarEncuesta(oEncuesta)
                ElseIf ConstantesDeEvento.BUSCAR.Equals(Request.Form("accion")) Then
                    buscar(id.Value, nombre.Value)
                End If
            Else
                tipo.DataSource = GestorABM.instancia().obtenerTodosLosTiposDeEncuesta()
                tipo.DataValueField = "id"
                tipo.DataTextField = "tipo"
                tipo.DataBind()
                LstEncuestas.DataSource = GestorOrdenes.instancia().obtenerEncuestasPorTipo(0, "", "")
                LstEncuestas.DataTextField = "nombre"
                LstEncuestas.DataValueField = "id"
                LstEncuestas.DataBind()

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

    Private Sub buscar(idEncuesta As Integer, nombreEncuesta As String)
        Dim encuesta As Encuesta = GestorABM.instancia().obtenerEncuesta(idEncuesta, nombreEncuesta)
        If Not encuesta Is Nothing Then
            id.Value = encuesta.id
            nombre.Value = encuesta.nombre
            descripcion.Value = encuesta.descripcion
            fechaDesde.Value = encuesta.fechaDesde
            fechaHasta.Value = encuesta.fechaHasta
            aagregarPreguntas.HRef = "/Admin/PreguntaDeEncuesta.aspx?encuestaid=" & encuesta.id

            LstPreguntas.DataSource = encuesta.preguntas
            LstPreguntas.DataBind()
        End If
    End Sub

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdEncuesta()
    End Function

    Protected Sub LstEncuestas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstEncuestas.SelectedIndexChanged
        buscar(LstEncuestas.SelectedValue, "")


    End Sub
End Class