Imports Modelo
Imports Datos
Imports System.Threading.Tasks

Public Class GestorEmail

    Private Sub New()

    End Sub

    Private Shared _instancia As New GestorEmail

    Public Shared Function instancia() As GestorEmail
        Return _instancia
    End Function

    Public Async Function enviarEmailDeContacto(oMensaje As MensajeDeConsulta) As System.Threading.Tasks.Task
        Task.Run(Sub()
                     Dim mensaje As String = "<table><tr><td>Email:</td><td>" & oMensaje.email & "</td></tr><tr><td>Persona de contacto: </td><td>" & oMensaje.identificacion & "</td></tr><tr><td>Mensaje:</td><td></td></tr><tr><td>" & oMensaje.mensaje & " </td><td></td></tr></table>"
                     GestorSeguridad.instancia().enviarEmail(mensaje, "Ticket Nro: " & Guid.NewGuid.ToString(), "doo.ba.soporte@gmail.com", Nothing)
                 End Sub
       )

    End Function


    Public Async Function enviarDenegacionDeOrden(oOrden As Orden) As Threading.Tasks.Task
        Task.Run(Sub()
                     Dim emailterminado As String = ""
                     Dim criterio2 As New CriterioDeBusqueda
                     criterio2.criterioEntero = 3
                     Dim template As TipoDeEmail = TipoDeEmailDao.instancia().obtenerUno(criterio2)
                     emailterminado = template.template.Replace("{fecha}", oOrden.fechaInicio.ToString("dd/MM/yyyy"))
                     emailterminado = emailterminado.Replace("{nroDeOrden}", oOrden.nroDeOrden.ToString())
                     GestorSeguridad.instancia().enviarEmail(emailterminado, "Cancelación de orden", oOrden.usuario.persona.contacto.email, Nothing)
                 End Sub
        )
    End Function

    Public Function generarConfirmacionDeOrden(orden As Orden) As String
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = 1
        Dim template As TipoDeEmail = TipoDeEmailDao.instancia().obtenerUno(criterio)
        Dim lineaProducto As String = "<tr>                    <td valign=""top"" colspan=""2"" style=""font-weight:bold"">{producto}</td>                </tr>                <tr>                    <td width=""140"" valign=""top"">                    Cantidad: {cantidad}                    </td>                    <td width=""70"" style=""text-align:right;font-weight:bold"">{precio}</td>                </tr>                                <tr>                    <td valign=""top"" style=""padding-bottom:10px"" colspan=""2""></td><td valign=""top"" style=""padding-bottom:10px"" colspan=""2""></td>                </tr>"
        Dim productos As String = ""
        For Each detalle As DetalleDeOrden In orden.detalles
            productos = productos & lineaProducto.Replace("{producto}", detalle.producto.nombre).Replace("{cantidad}", detalle.cantidad.ToString()).Replace("{precio}", detalle.producto.precioVenta.ToString("C"))
        Next
        Dim emailterminado As String = template.template.Replace("{fecha}", Date.Now.ToString("dd/MM/yyyy"))
        emailterminado = emailterminado.Replace("{productos}", productos)
        emailterminado = emailterminado.Replace("{recargo}", orden.recargoPorTarjeta.ToString("C"))
        'criterio.criterioEntero = orden.egreso.productosEspecificosEnStock(0).nroDeSerie
        'Dim oEnvio As Envio = EnvioDao.instancia().obtenerUno(criterio)
        'Dim metodo As TipoDeEnvio = TipoDeEnvioDao.instancia().obtenerMuchos(Nothing).FirstOrDefault(Function(value As TipoDeEnvio)
        '                                                                                                 Return value.id = orden.envio.tipo.id
        '                                                                                             End Function)
        emailterminado = emailterminado.Replace("{metodo_de_envio}", "Nro. de envío: " & orden.envio.nroEnvio.ToString())
        emailterminado = emailterminado.Replace("{datos_de_envio}", "Costo de envío: " & orden.envio.monto.ToString("C"))
        emailterminado = emailterminado.Replace("{total}", orden.factura.montoDeCobro.ToString("F2"))
        Return emailterminado
    End Function

    Public Async Function enviarEmailDeConfirmacionDeCompra(orden As Orden) As Threading.Tasks.Task
        Task.Run(Sub()

                     Dim emailterminado As String = generarConfirmacionDeOrden(orden)
                     GestorSeguridad.instancia().enviarEmail(emailterminado, "Confirmación de compra", orden.usuario.persona.contacto.email, orden.factura.comprobante)
                 End Sub
        )
    End Function

    Public Async Function enviarEmailNotaDeCredito(oNota As NotaDeCredito) As Threading.Tasks.Task
        Task.Run(Sub()
                     Dim criterio As New CriterioDeBusqueda
                     criterio.criterioEntero = oNota.factura.orden.nroDeOrden
                     Dim orden As Orden = OrdenDao.instancia().obtenerUno(criterio)
                     Dim emailterminado As String = ""
                     Dim criterio2 As New CriterioDeBusqueda
                     criterio2.criterioEntero = 2
                     Dim template As TipoDeEmail = TipoDeEmailDao.instancia().obtenerUno(criterio2)
                     emailterminado = template.template.Replace("{fecha}", Date.Now.ToString("dd/MM/yyyy"))
                     emailterminado = emailterminado.Replace("{total}", oNota.monto.ToString("C"))
                     GestorSeguridad.instancia().enviarEmail(emailterminado, "Nota de crédito", orden.usuario.persona.contacto.email, oNota.comprobante)
                 End Sub
        )
    End Function

End Class
