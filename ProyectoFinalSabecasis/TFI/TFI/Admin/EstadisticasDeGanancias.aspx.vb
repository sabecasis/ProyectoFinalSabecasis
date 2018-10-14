
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class EstadisticasDeGanancias
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre

            End If
            breadcrums.InnerHtml = Session("cadenabreadcrums")
            Dim suc As New Sucursal
            suc.nombre = ""
            suc.nroSucursal = 0
            If Not Me.IsPostBack Then
                Dim sucursales As New List(Of Sucursal)
                sucursales.Add(suc)
                sucursales.AddRange(GestorABM.instancia().obtenerTodasLasSucursales())
                CMBSucursal.DataSource = sucursales
                CMBSucursal.DataValueField = "nroSucursal"
                CMBSucursal.DataTextField = "nombre"
                CMBSucursal.DataBind()
            End If

            GraficaAnual.ChartAreas("ChartArea1").AxisX.Interval = 1
            GraficaAnual.DataSource = GestorOrdenes.instancia().obtenerInformacionEstadisticaAnual(CMBSucursal.SelectedValue)
            GraficaAnual.Series("Series1").XValueMember = "concepto"
            GraficaAnual.Series("Series1").YValueMembers = "total"
            GraficaAnual.DataBind()

            Dim anios As New List(Of String)
            For i As Integer = 2015 To Date.Now.Year
                anios.Add(i.ToString())
            Next
            CMBAnios.DataSource = anios
            CMBAnios.DataBind()

            GraficaMensual.DataSource = GestorOrdenes.instancia().obtenerInformacionEstadisticaMensual(CMBAnios.SelectedValue, CMBSucursal.SelectedValue)
            GraficaMensual.ChartAreas("ChartArea2").AxisX.Interval = 1
            GraficaMensual.Series("Series2").XValueMember = "concepto"
            GraficaMensual.Series("Series2").YValueMembers = "total"
            GraficaMensual.DataBind()

            Dim meses As New List(Of String)
            For i As Integer = 1 To 12
                meses.Add(i.ToString())
            Next
            CMBMeses.DataSource = meses
            CMBMeses.DataBind()

            GraficoDiario.DataSource = GestorOrdenes.instancia().obtenerInformacionEstadisticaDiaria(CMBAnios.SelectedValue, CMBMeses.SelectedValue, CMBSucursal.SelectedValue)
            GraficoDiario.ChartAreas("ChartArea3").AxisX.Interval = 1
            GraficoDiario.Series("Series3").XValueMember = "concepto"
            GraficoDiario.Series("Series3").YValueMembers = "total"
            GraficoDiario.DataBind()
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

    Protected Sub CMBAnios_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

    Protected Sub CMBMeses_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub
    Protected Sub CMBsusursal_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub
End Class