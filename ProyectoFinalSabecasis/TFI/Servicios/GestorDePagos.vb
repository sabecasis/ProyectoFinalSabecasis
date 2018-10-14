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
            Dim subtotal As Integer = 0
            subtotal += detalle.monto * detalle.cantidad
            If Not detalle.producto.descuentos Is Nothing AndAlso detalle.producto.descuentos.Any() Then
                For Each oDEsc As Descuento In detalle.producto.descuentos
                    If oDEsc.monto <> 0 Then
                        subtotal = subtotal - (oDEsc.monto * detalle.cantidad)
                    End If
                    If oDEsc.porcentaje <> 0 Then
                        subtotal = subtotal - (((subtotal * oDEsc.porcentaje) / 100))
                    End If
                Next
            End If
            totalAPagar += subtotal
        Next
        totalAPagar += orden.envio.monto
        If orden.informacionDePago.nroNotaDeCredito <> 0 Then
            Try
                totalAPagar = calcularMontoConNotaDeCredito(orden.informacionDePago.nroNotaDeCredito, totalAPagar)
            Catch ex As ExcepcionDeValidacion
            End Try
        End If
        Dim cuotaRecargo = orden.informacionDePago.metodo.cuotas.First(Function(c) c.cantidadDeCuotas = orden.cuotas)
        totalAPagar = totalAPagar + orden.recargoPorTarjeta
        Return totalAPagar
    End Function

    Public Function calcularMontoConNotaDeCredito(nroNotaDeCredito As Integer, total As Integer) As Double
        Dim oNota As NotaDeCredito = GestorOrdenes.instancia().obtenerNotaDeCreditoPorNro(nroNotaDeCredito)
        ValidacionHelper.instancia().validarNotaDeCredito(oNota)
        Dim criterioFacturas As New CriterioDeBusqueda
        criterioFacturas.criterioBoolean = True
        criterioFacturas.criterioEntero = oNota.nroNotaDeCredito
        Dim facturasAplicadas As List(Of Factura) = FacturaDao.instancia().obtenerMuchos(criterioFacturas)
        Dim gastosDeNota As Double = 0
        For Each facturita As Factura In facturasAplicadas
            gastosDeNota += (facturita.montoDeVenta + facturita.recargoPorTarjeta)
        Next
        Dim montoAplicable = oNota.monto - gastosDeNota
        If montoAplicable > total Then
            total = 0
        Else
            total = total - oNota.monto
        End If
        Return total
    End Function
    Public Function cobrarProducto(orden As Orden) As Boolean
        Dim result As Boolean = False
        If orden.egreso Is Nothing Then
            Dim oEgreso As New EgresoDeStock
            oEgreso.estado = New Estado
            oEgreso.estado.id = 1
            oEgreso.fecha = DateTime.Now.ToString("yyyy-MM-dd")
            oEgreso.motivo = ConstantesDeMotivosDeModificacionDeStock.VENTA.ToString
            oEgreso.nroEgreso = EgresoDeStockDao.instancia().obtenerProximoId()
            oEgreso.sucursal = New Sucursal()
            oEgreso.sucursal.nroSucursal = 1
            oEgreso.usuario = orden.usuario
            Dim productosEspecificos As New List(Of ProductoEspecificoEnStock)
            Dim oProdEspPorProducto As List(Of ProductoEspecificoEnStock)
            For Each oDetalle As DetalleDeOrden In orden.detalles
                oProdEspPorProducto = GestorStock.instancia().buscarInstanciasEspecificasParaEgreso(oEgreso.sucursal.nroSucursal, oDetalle.producto.nroDeProducto, oDetalle.cantidad)
                If oProdEspPorProducto Is Nothing Then
                    result = True
                    Try
                        GestorEmail.instancia().enviarDenegacionDeOrden(orden)
                    Catch ex As Exception
                        'si falla seguimos viviendo igual, que nada nos detenga el hilo ni nos genere un loop
                    End Try
                    Exit For
                Else
                    productosEspecificos.AddRange(oProdEspPorProducto)
                End If

            Next
            oEgreso.productosEspecificosEnStock = productosEspecificos
            If productosEspecificos.Count > 0 Then
                GestorStock.instancia().guardarEgresoDeStock(oEgreso)
                orden.sucursal = New Sucursal()
                orden.sucursal.nroSucursal = 1
                orden.egreso = oEgreso
                GestorOrdenes.instancia().guardarOrden(orden)

                Dim oGarantia As New Garantia
                For Each oProd As ProductoEspecificoEnStock In productosEspecificos
                    oGarantia.fechaInicio = DateTime.Now
                    oGarantia.fechaFin = DateTime.Now.AddDays(oProd.producto.garantia.dias)
                    oGarantia.productoEspecifico = oProd
                    oGarantia.nroGarantia = oProd.producto.garantia.id
                    GarantiaDao.instancia().crear(oGarantia)
                Next
            End If
        End If

        Dim oFactura = GestorOrdenes.instancia().obtenerFacturaPorNumeroDeOrden(orden.nroDeOrden)
        If oFactura Is Nothing AndAlso Not orden.egreso Is Nothing AndAlso Not orden.egreso.productosEspecificosEnStock.Count = 0 Then
            oFactura = New Factura
            oFactura.fechaDeVenta = orden.fechaInicio
            oFactura.montoDeCobro = calcularMontoDeCobro(orden)
            oFactura.montoDeVenta = orden.totalAPagar
            oFactura.orden = orden
            oFactura.sucursal = New Sucursal()
            oFactura.sucursal.nroSucursal = 1
            oFactura.tipoDeFactura = New TipoDeFactura
            oFactura.tipoDeFactura.id = 2
            oFactura.tipoDeFactura.tipo = "B"
            oFactura.usuario = orden.usuario
            oFactura.egresoDeStock = orden.egreso
            oFactura.detallesDeFactura = New List(Of DetalleDeFactura)
            oFactura.notaDeCreditoAplicada = New NotaDeCredito
            oFactura.notaDeCreditoAplicada.nroNotaDeCredito = orden.informacionDePago.nroNotaDeCredito
            oFactura.recargoPorTarjeta = orden.recargoPorTarjeta
            Dim detalleFact As DetalleDeFactura
            For Each detal As DetalleDeOrden In orden.detalles
                detalleFact = New DetalleDeFactura
                detalleFact.tipoDeFactura = New TipoDeFactura
                detalleFact.tipoDeFactura.id = 2
                detalleFact.tipoDeFactura.tipo = "B"
                detalleFact.cantidad = detal.cantidad
                detalleFact.precioUnitario = detal.monto
                detalleFact.producto = detal.producto
                detalleFact.sucursal = oFactura.sucursal
                detalleFact.descuentos = detal.producto.descuentos
                oFactura.detallesDeFactura.Add(detalleFact)
            Next

            result = FacturaDao.instancia().crear(oFactura)
            'si hubiese integración con el servicio de tarjeta de crédito, la llamada estaría acá

            orden.factura = oFactura

            If result Then
                Dim criterio As New CriterioDeBusqueda
                For Each oDetalle As DetalleDeFactura In oFactura.detallesDeFactura
                    criterio.criterioEntero = oDetalle.id
                    oDetalle.descuentos = DescuentoDao.instancia().obtenerMuchos(criterio)
                Next
                oFactura.comprobante = GestorComprobantes.instancia().crearComprobanteFactura(oFactura)
                Try
                    ' File.WriteAllBytes("C:/backups/factura.pdf", oFactura.comprobante)
                Catch ex As Exception
                    'si falla seguimos viviendo igual, que nada nos detenga el hilo ni nos genere un loop
                End Try
                Try
                    GestorEmail.instancia().enviarEmailDeConfirmacionDeCompra(orden)
                Catch ex As Exception
                    Dim e As Integer = 0
                    'si falla seguimos viviendo igual, que nada nos detenga el hilo ni nos genere un loop
                End Try
                result = FacturaDao.instancia().modificar(oFactura)



                If oFactura.notaDeCreditoAplicada.nroNotaDeCredito <> 0 Then
                    Dim oNota As NotaDeCredito = GestorOrdenes.instancia().obtenerNotaDeCreditoPorNro(oFactura.notaDeCreditoAplicada.nroNotaDeCredito)
                    Dim criterioFacturas As New CriterioDeBusqueda
                    criterioFacturas.criterioBoolean = True
                    criterioFacturas.criterioEntero = oNota.nroNotaDeCredito
                    Dim facturasAplicadas As List(Of Factura) = FacturaDao.instancia().obtenerMuchos(criterioFacturas)
                    Dim gastosDeNota As Double = 0
                    For Each facturita As Factura In facturasAplicadas
                        gastosDeNota += (facturita.montoDeVenta + facturita.recargoPorTarjeta)
                    Next
                    If gastosDeNota <> 0 Then
                        gastosDeNota -= (oFactura.montoDeVenta + oFactura.recargoPorTarjeta)
                    End If
                    Dim criterioEstado As New CriterioDeBusqueda
                    criterioEstado.criterioEntero = orden.informacionDePago.persona.id
                    Dim estadoDeCuenta As EstadoDeCuenta = EstadoDeCuentaDao.instancia().obtenerUno(criterioEstado)
                    If oNota.monto > gastosDeNota + orden.totalAPagar + orden.recargoPorTarjeta Then
                        estadoDeCuenta.totalEnCredito -= (orden.totalAPagar + orden.recargoPorTarjeta)
                    Else
                        estadoDeCuenta.totalEnCredito -= (oNota.monto - gastosDeNota)
                        oNota.estaActiva = False
                        NotaDeCreditoDao.instancia().modificar(oNota)
                    End If
                    EstadoDeCuentaDao.instancia().modificar(estadoDeCuenta)
                End If
            End If
        Else
            result = True
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
            lista.Add(detalle)
        Next
        Return lista
    End Function
End Class
