Imports Seguridad
Imports NegocioYServicios
Imports Modelo
Imports System.IO

Public Class AdminitrarOrdenes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Try
            If Not Me.IsPostBack Then
                limpiar()
            Else
                If ConstantesDeEvento.BUSCAR.Equals(Request.Form("accion")) Then
                    If String.IsNullOrEmpty(nrodefactura.Value) Then
                        nrodefactura.Value = 0
                    End If
                    If String.IsNullOrEmpty(nrodeorden.Value) Then
                        nrodeorden.Value = 0
                    End If
                    ordenes.DataSource = GestorOrdenes.instancia().obtenerTodasLasOrdenes(nrodeorden.Value, nrodefactura.Value, fecha_desde.Value, fecha_hasta.Value, nombreusuario.Value)
                    ordenes.DataBind()
                ElseIf ConstantesDeEvento.LIMPIAR.Equals(Request.Form("accion")) Then
                    limpiar()
                End If
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
    End Sub
    Private Sub limpiar()
        ordenes.DataSource = GestorOrdenes.instancia().obtenerTodasLasOrdenes(0, 0, "", "", "")
        ordenes.DataBind()
    End Sub

    Public Sub ordenes_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Try
            ' If multiple ButtonField column fields are used, use the
            ' CommandName property to determine which button was clicked.
            If e.CommandName = "descargarBtn" Then

                ' Convert the row index stored in the CommandArgument
                ' property to an Integer.
                Dim index As Integer = Convert.ToInt32(e.CommandArgument)

                ' Get the last name of the selected author from the appropriate
                ' cell in the GridView control.
                Dim selectedRow As GridViewRow = ordenes.Rows(index)
                Dim btn As LinkButton = selectedRow.Cells(1).Controls.Item(0)
                Dim nro As String = btn.Text

                Dim oFactura As Factura = GestorOrdenes.instancia().obtenerFacturaPorNro(Convert.ToInt32(nro))
                If (Not oFactura Is Nothing) AndAlso (Not oFactura.comprobante Is Nothing) Then
                    Response.Clear()
                    Dim ms As New MemoryStream(oFactura.comprobante)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=factura.pdf")
                    Response.Buffer = True
                    ms.WriteTo(Response.OutputStream)
                    Response.End()
                End If

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
    End Sub



End Class