Imports Modelo
Imports Datos
Imports System.IO
Imports Seguridad

Public Class GestorDePagos
    Private Sub New()

    End Sub

    Private Shared objeto As New GestorDePagos

    Public Shared Function instancia() As GestorDePagos
        Return objeto
    End Function

    Public Function obtenerRegexPorMetodoDePago(id As Integer)
        Dim query As New CriterioDeBusqueda
        query.criterioEntero = id
        Dim dao As AbstractDao(Of MetodoDePago) = MetodoDePagoDao.instancia()
        Return dao.obtenerUno(query)
    End Function

    Public Function calcularMontoDeCobro(orden As Orden) As Double
        Dim totalAPagar As Double = 0
        For Each detalle As DetalleDeOrden In orden.detalles
            totalAPagar += detalle.monto * detalle.cantidad
        Next
        totalAPagar += orden.envio.monto
        totalAPagar = totalAPagar + totalAPagar * ConstantesDeCargasImpositivas.IVA
        'todo, agregar cargas impositivas de tasjetas
        Return totalAPagar
    End Function

    Public Function cobrarProducto(orden As Orden) As Boolean
        Dim result As Boolean = False
        Dim oEgreso As New EgresoDeStock
        oEgreso.estado = New Estado
        oEgreso.estado.id = 1
        oEgreso.fecha = DateTime.Now
        oEgreso.motivo = ConstantesDeMotivosDeModificacionDeStock.VENTA.ToString
        oEgreso.nroEgreso = EgresoDeStockDao.instancia().obtenerProximoId()
        oEgreso.sucursal = New Sucursal()
        oEgreso.sucursal.nroSucursal = 1
        oEgreso.usuario = orden.usuario
        Dim productosEspecificos As New List(Of ProductoEspecificoEnStock)
        Dim oProdEspPorProducto As List(Of ProductoEspecificoEnStock)
        For Each oDetalle As DetalleDeOrden In orden.detalles
            oProdEspPorProducto = GestorStock.instancia().buscarInstanciasEspecificasParaEgreso(oEgreso.sucursal.nroSucursal, oDetalle.producto.nroDeProducto, oDetalle.cantidad)
            productosEspecificos.AddRange(oProdEspPorProducto)
        Next
        oEgreso.productosEspecificosEnStock = productosEspecificos
        If productosEspecificos.Count > 0 Then
            GestorStock.instancia().guardarEgresoDeStock(oEgreso)
            Dim oFactura As New Factura
            oFactura.fechaDeVenta = orden.fechaInicio
            oFactura.montoDeCobro = calcularMontoDeCobro(orden)
            oFactura.montoDeVenta = orden.totalAPagar
            oFactura.orden = orden
            oFactura.sucursal = New Sucursal()
            oFactura.sucursal.nroSucursal = 1
            oFactura.tipoDeFactura = New TipoDeFactura
            oFactura.tipoDeFactura.id = 1
            oFactura.tipoDeFactura.tipo = "A"
            oFactura.usuario = orden.usuario
            oFactura.egresoDeStock = oEgreso
            oFactura.detallesDeFactura = New List(Of DetalleDeFactura)
            Dim detalleFact As DetalleDeFactura
            For Each detal As DetalleDeOrden In orden.detalles
                detalleFact = New DetalleDeFactura
                detalleFact.tipoDeFactura = New TipoDeFactura
                detalleFact.tipoDeFactura.id = 1
                detalleFact.tipoDeFactura.tipo = "A"
                detalleFact.cantidad = detal.cantidad
                detalleFact.precioUnitario = detal.monto
                detalleFact.producto = detal.producto
                detalleFact.sucursal = oFactura.sucursal
                oFactura.detallesDeFactura.Add(detalleFact)
            Next

            result = FacturaDao.instancia().crear(oFactura)
            If result Then
                Dim criterio As New CriterioDeBusqueda
                For Each oDetalle As DetalleDeFactura In oFactura.detallesDeFactura
                    criterio.criterioEntero = oDetalle.id
                    oDetalle.descuentos = DescuentoDao.instancia().obtenerMuchos(criterio)
                Next
                oFactura.comprobante = GestorComprobantes.instancia().crearComprobanteFactura(oFactura)
                Try
                    File.WriteAllBytes("C:/backups/factura.pdf", oFactura.comprobante)
                Catch ex As Exception
                    'si falla seguimos viviendo igual, que nada nos detenga el hilo ni nos genere un loop
                End Try
                Try
                    GestorSeguridad.instancia().enviarEmail("Usted está recibiendo este email porque ha generado una orden de compra en Doo-Ba con número de orden " & orden.nroDeOrden.ToString() & ". Verifique su compra con la factura que se encuentra adjunta en el presente email.", "Confirmación de compra", orden.usuario.persona.contacto.email, oFactura.comprobante)
                Catch ex As Exception
                    Dim e As Integer = 0
                    'si falla seguimos viviendo igual, que nada nos detenga el hilo ni nos genere un loop
                End Try
                result = FacturaDao.instancia().modificar(oFactura)

                'If MetodosDePago.VISA.Equals(orden.informacionDePago.metodo.id) Or _
                '   MetodosDePago.MASTERCARD.Equals(orden.informacionDePago.metodo.id) Or _
                '   MetodosDePago.AMERICAN_EXPRESS.Equals(orden.informacionDePago.metodo.id) Or _
                '   MetodosDePago.MERCADO_PAGO.Equals(orden.informacionDePago.metodo.id) Then
                '    'llamada al servicio de la tarjeta
                'ElseIf MetodosDePago.PAYPAL.Equals(orden.informacionDePago.metodo.id) Then
                '    'llamada al servicio de paypal
                'End If

                orden.facturas.Add(oFactura)
                Dim oGarantia As New Garantia
                For Each oProd As ProductoEspecificoEnStock In productosEspecificos
                    oGarantia.fechaInicio = DateTime.Now
                    oGarantia.fechaFin = DateTime.Now.AddDays(oProd.producto.garantia.dias)
                    oGarantia.productoEspecifico = oProd
                    oGarantia.nroGarantia = oProd.producto.garantia.id
                    GarantiaDao.instancia().crear(oGarantia)
                    'TODO crear comprobante
                Next
            End If
        End If
        Return result
    End Function


    Public Function calcularMontosDetalle(productos As Dictionary(Of Integer, Integer)) As List(Of DetalleDeOrden)
        Dim criterio As New CriterioDeBusqueda
        Dim prod As Producto
        Dim lista As New List(Of DetalleDeOrden)
        Dim detalle As DetalleDeOrden
        For Each kvp As KeyValuePair(Of Integer, Integer) In productos
            detalle = New DetalleDeOrden
            criterio.criterioEntero = kvp.Key
            criterio.criterioString = ""
            prod = ProductoDao.instancia().obtenerUno(criterio)
            detalle.producto = prod
            detalle.monto = prod.precioVenta
            detalle.cantidad = kvp.Value
            If prod.descuentos.Count > 0 Then
                For Each oDesc As Descuento In prod.descuentos
                    If oDesc.monto > 0 Then
                        detalle.monto = detalle.monto - oDesc.monto
                    End If
                    If oDesc.porcentaje > 0 Then
                        detalle.monto = detalle.monto + ((detalle.monto * oDesc.porcentaje) / 100)
                    End If
                Next
            End If
            lista.Add(detalle)
        Next
        Return lista
    End Function
End Class
