Imports System.Web.Services
Imports NegocioYServicios
Imports Modelo
Imports Seguridad
Imports System.IO

Public Class AdministrarNotaDeCredito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Not Me.IsPostBack Then
                If Not String.IsNullOrEmpty(Request.Params("nro")) Then
                    nrodevolucion.Value = Request.Params("nro")
                    Dim odevo As DevolucionDeProducto = GestorABM.instancia().buscarDevolucionDeProducto(nrodevolucion.Value)
                    If Not odevo Is Nothing Then
                        monto.Value = odevo.monto
                        nroFactura.Value = GestorOrdenes.instancia().obtenerFacturaPorNroDeSerie(odevo.producto.nroDeSerie).nroFactura
                    End If
                End If
                LstNotas.DataSource = GestorABM.instancia().obtenerTodasLasNotasDeCredito()
                LstNotas.DataValueField = "nroNotaDeCredito"
                LstNotas.DataTextField = "nroNotaDeCredito"
                LstNotas.DataBind()
            Else
                If ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                    Dim nota As New NotaDeCredito
                    ValidacionHelper.instancia().validarCampoVacio(monto.Value, "monto")
                    ValidacionHelper.instancia().validarCampoVacio(nroFactura.Value, "nro. de factura")
                    ValidacionHelper.instancia().validarCampoVacio(fecha.Value, "fecha")
                    nota.devolucionDeProducto = New DevolucionDeProducto
                    If String.IsNullOrEmpty(nrodevolucion.Value) Then
                        nota.devolucionDeProducto.nroDeDevolucion = 0
                    Else
                        nota.devolucionDeProducto.nroDeDevolucion = nrodevolucion.Value
                    End If

                    nota.monto = monto.Value
                    nota.factura = New Factura
                    nota.factura.nroFactura = nroFactura.Value
                    nota.descripcion = descripcion.Value
                    If fechacaducidad.Value.Equals("") Then
                        nota.fechaCaducidad = Date.Now
                    Else
                        nota.fechaCaducidad = fechacaducidad.Value
                    End If
                    nota.fechaEmision = fecha.Value
                    nota.sucursal = New Sucursal
                    nota.sucursal.nroSucursal = 1
                    Dim result = GestorABM.instancia().crearNotaDeCredito(nota)
                    LblMensaje.InnerText = ConstantesDeMensaje.GUARDADO_CON_EXITO & ". Id: " & nota.nroNotaDeCredito

                ElseIf ConstantesDeEvento.BUSCAR.Equals(Request.Form("accion")) Then
                    buscar(id.Value)
                ElseIf ConstantesDeEvento.COMPROBANTE.Equals(Request.Form("accion")) Then
                    ValidacionHelper.instancia().validarCampoVacio(Request.Form("id"), "id")
                    Dim nroNota As Integer = Convert.ToInt32(Request.Form("id"))
                    Dim oEgreso As Modelo.NotaDeCredito = GestorOrdenes.instancia().obtenerNotaDeCreditoPorNro(nroNota)
                    If (Not oEgreso Is Nothing) AndAlso (Not oEgreso.comprobante Is Nothing) Then
                        Response.Clear()
                        Dim ms As New MemoryStream(oEgreso.comprobante)
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("content-disposition", "attachment;filename=NotaCredito.pdf")
                        Response.Buffer = True
                        ms.WriteTo(Response.OutputStream)
                        Response.End()
                    End If
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
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdNotaDeCredito()
    End Function

    Private Sub buscar(nroNota As Integer)
        Dim oNota As NotaDeCredito = GestorOrdenes.instancia().obtenerNotaDeCreditoPorNro(nroNota)
        id.Value = oNota.nroNotaDeCredito
        monto.Value = oNota.monto
        nroFactura.Value = oNota.factura.nroFactura
        fecha.Value = oNota.fechaEmision
        If Not oNota.fechaCaducidad.Equals(New Date()) Then
            fechacaducidad.Value = oNota.fechaCaducidad
        End If

        descripcion.Value = oNota.descripcion
        Dim dev As DevolucionDeProducto = GestorABM.instancia().obtenerDevolucionPorNroNotaDeCredito(oNota.nroNotaDeCredito)
        nrodevolucion.Value = dev.nroDeDevolucion
    End Sub

    Protected Sub LstNotas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstNotas.SelectedIndexChanged
        buscar(LstNotas.SelectedValue)
    End Sub
End Class