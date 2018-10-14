Imports Seguridad
Imports NegocioYServicios
Imports System.Web.Services
Imports Modelo

Public Class AdministrarDescuentos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LblMensaje.InnerText = ""
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                LblMensaje.InnerText = guardar()
                LblMensaje.DataBind()
            ElseIf ConstantesDeEvento.BUSCAR.Equals(Request.Form("accion")) Then
                buscar(id.Value, nombre.Value)
            End If
            If Not Me.IsPostBack Then
                LstDescuentos.DataSource = GestorABM.instancia().obtenerTodosLosDescuentos()
                LstDescuentos.DataTextField = "nombre"
                LstDescuentos.DataValueField = "id"
                LstDescuentos.DataBind()
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

    Private Sub buscar(idDescuento, nombreDescuento)
        Dim descuento As Descuento = GestorABM.instancia().buscarDescuento(idDescuento, nombreDescuento)
        If Not descuento Is Nothing Then
            id.Value = descuento.id
            nombre.Value = descuento.nombre
            descripcion.Value = descuento.descripcion
            fechainicio.Value = descuento.fechaInicio
            If Not descuento.fechaFin Is Nothing Then
                fechafin.Value = descuento.fechaFin
            End If
            monto.Value = descuento.monto
            porcentaje.Value = descuento.porcentaje
        End If
    End Sub

    Private Function guardar() As String
        Dim descuento As New Modelo.Descuento
        descuento.id = id.Value
        descuento.nombre = nombre.Value
        descuento.descripcion = descripcion.Value
        If String.IsNullOrEmpty(monto.Value) Then
            descuento.monto = 0
        Else
            descuento.monto = Convert.ToDouble(monto.Value.Replace(",", "."))
        End If
        If String.IsNullOrEmpty(porcentaje.Value) Then
            descuento.porcentaje = 0
        Else
            descuento.porcentaje = Convert.ToDouble(porcentaje.Value.Replace(",", "."))
        End If
        descuento.fechaInicio = fechainicio.Value
        If Not String.IsNullOrEmpty(fechafin.Value) Then
            descuento.fechaFin = fechafin.Value
        End If
        Dim result As Boolean = GestorABM.instancia().guardarDescuento(descuento)
        If result Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdDescuento()
    End Function

    Protected Sub LstDescuentos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstDescuentos.SelectedIndexChanged
        buscar(LstDescuentos.SelectedValue, "")
    End Sub
End Class