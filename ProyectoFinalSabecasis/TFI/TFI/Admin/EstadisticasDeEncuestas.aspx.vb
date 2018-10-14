
Imports NegocioYServicios
Imports Seguridad
Imports Modelo
Imports System.Web.UI.DataVisualization.Charting

Public Class EstadisticasDeEncuestas
    Inherits System.Web.UI.Page

    Property preguntas As List(Of Modelo.PreguntaDeEncuesta)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                preguntas = GestorUsuario.instancia().obtenerPreguntasDeEncuestaPorTipo(2, "", "")
                CMBPreguntas.DataSource = preguntas
                CMBPreguntas.DataTextField = "pregunta"
                CMBPreguntas.DataValueField = "pregunta"
                CMBPreguntas.DataBind()
                CMBPreguntas_SelectedIndexChanged(sender, e)
            End If
            CMBPreguntas_SelectedIndexChanged(sender, e)
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

    Protected Sub CMBPreguntas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            preguntas = GestorUsuario.instancia().obtenerPreguntasDeEncuestaPorTipo(2, fechaDesde.Value, fechaHasta.Value)
            Dim respuestas As List(Of OpcionDeEncuesta) = preguntas.Item(CMBPreguntas.SelectedIndex).respuestas
            Grafica.ChartAreas("ChartArea1").AxisX.Interval = 1
            Grafica.DataSource = respuestas
            Grafica.Series("Series1").XValueMember = "opcion"
            Grafica.Series("Series1").YValueMembers = "cantidadDeSelecciones"
            Grafica.DataBind()

            For Each oResp As OpcionDeEncuesta In respuestas
                If oResp.variaciones.Any() Then
                    Dim grafic As New Chart
                    grafic.Titles.Add(oResp.opcion)
                    grafic.ChartAreas.Add(New ChartArea("ChartArea1"))
                    grafic.ChartAreas("ChartArea1").AxisX.Interval = 1
                    grafic.DataSource = oResp.variaciones
                    grafic.Series.Add(New Series())
                    grafic.Series(0).XValueMember = "fecha"
                    grafic.Series(0).YValueMembers = "cantidad"
                    placeHolder.Controls.Add(grafic)
                    grafic.DataBind()
                End If

            Next


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
    End Sub
End Class