Imports Modelo
Imports Datos
Imports Seguridad

Public Class GestorOrdenes
    Private Sub New()

    End Sub

    Private Shared objeto As New GestorOrdenes

    Public Shared Function instancia() As GestorOrdenes
        Return objeto
    End Function

    Public Function generarEnvio(oEnvio As Envio) As Boolean
        Return True
    End Function

    Public Function obtenerTodasLasOrdenes(nroDeOrden As Integer, nroDeFactura As Integer, fechaDesde As String, fechaHasta As String, usuario As String) As List(Of VistaDeOrden)
        Dim criterio As New CriterioDeBusquedaDeOrden
        criterio.fechaDesde = fechaDesde
        criterio.fechaHasta = fechaHasta
        criterio.nroDeFactura = nroDeFactura
        criterio.nroDeOrden = nroDeOrden
        criterio.usuario = usuario
        Dim listaOrdenes As List(Of Orden) = OrdenDao.instancia().obtenerMuchos(criterio)
        Dim listaResultado As New List(Of VistaDeOrden)
        For Each oOrden As Orden In listaOrdenes
            Dim vista As New VistaDeOrden
            If String.IsNullOrEmpty(oOrden.fechaFinalizacion) Then
                vista.fechaCompletado = ""
            Else
                vista.fechaCompletado = oOrden.fechaFinalizacion
            End If
            vista.fechaInicio = oOrden.fechaInicio
            If Not oOrden.egreso Is Nothing Then
                vista.nroEgreso = oOrden.egreso.nroEgreso
            End If
            If Not oOrden.factura Is Nothing Then
                vista.nroFactura = oOrden.factura.nroFactura
            End If
            vista.nroDeOrden = oOrden.nroDeOrden
            vista.usuario = oOrden.usuario.nombre
            listaResultado.Add(vista)
        Next
        Return listaResultado
    End Function


    Public Function obtenerOrdenPorNro(nroOrden As Integer) As Orden
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroOrden
        Return OrdenDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerNotaDeCreditoPorNro(nroNotaDeCredito As Integer) As NotaDeCredito
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroNotaDeCredito
        Return NotaDeCreditoDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerSucursalesDeOcaPorLocalidad(codigoPostal As String) As List(Of KeyValuePair(Of String, String))
        Dim cliente As New Oca.Oep_TrackingSoapClient
        Dim dataSet = cliente.GetCentrosImposicionPorCP(codigoPostal)
        Dim tabla As DataTable = dataSet.Tables(0)
        Dim listaresultado As New List(Of KeyValuePair(Of String, String))
        For Each row As DataRow In tabla.AsEnumerable
            listaresultado.Add(New KeyValuePair(Of String, String)(row.Item("IdCentroImposicion"), row.Item("Calle") & " " & row.Item("Numero")))
        Next
        Return listaresultado
    End Function

    Public Function obtenerCostoDeEnvioDeCheckout(idCheckout As Integer, codigoPostal As String) As Double
        Dim checkout = GestorOrdenes.instancia().obtenerCheckoutPorId(idCheckout)
        'por cada producto del checkout calculamos el volumen, obtenemos el costo de envío y lo sumamos al total
        Dim total As Double = 0
        If Not checkout.productos Is Nothing Then
            For Each id As Integer In checkout.productos.Keys
                total = total + obtenerCostoDeEnvioPorProductoEnCheckout(id, codigoPostal, checkout.productos(id))
            Next
        End If
        Return total
    End Function

    Public Function obtenerFacturaPorNumeroDeOrden(nroDeOrden As Integer) As Factura
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioBoolean = True
        criterio.criterioEntero = nroDeOrden
        Return FacturaDao.instancia().obtenerUno(criterio)
    End Function

    Public Function guardarOrden(orden As Orden) As Boolean
        Return OrdenDao.instancia().modificar(orden)
    End Function

    Public Function obtenerCostoDeEnvioPorProductoEnCheckout(nroDeProducto As Integer, cp As String, cantidad As Integer) As Double
        Dim cliente As New Oca.Oep_TrackingSoapClient
        Dim producto As Producto = New Producto
        producto.nroDeProducto = nroDeProducto
        producto = GestorABM.instancia().buscarProducto(producto)
        Dim volumen = producto.anchoPaquete * producto.altoPaquete * producto.largoPaquete
        'cp de flores donde se supone que esta la fabrica. TODO: sacar esto delobjeto sucursal
        Dim dataSet = cliente.Tarifar_Envio_Corporativo(producto.peso.ToString("R").Replace(",", "."), volumen.ToString(), "1406", cp, cantidad, "27-16321016-4", "63082")
        Dim tabla As DataTable = dataSet.Tables(0)
        Dim precioEnvio As Double = 0
        For Each row As DataRow In tabla.AsEnumerable
            precioEnvio = row.Item("Total")
        Next
        Return precioEnvio
    End Function

    Public Function obtenerValoracionDeProductoProNro(nroDeProducto As Integer) As ValoracionDeProducto
        Dim dao As AbstractDao(Of ValoracionDeProducto) = ValoracionDeProductoDao.instancia()
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroDeProducto
        Return dao.obtenerUno(criterio)
    End Function

    Public Sub valorarProducto(oValoracion As ValoracionDeProducto)
        Dim dao As AbstractDao(Of ValoracionDeProducto) = ValoracionDeProductoDao.instancia()
        dao.modificar(oValoracion)
    End Sub

    Public Function actualizarOpcionEncuesta(opt As OpcionDeEncuesta) As Boolean
        Return OpcionDeEncuestaDao.instancia().modificar(opt)
    End Function

    Public Function obtenerFacturaPorNroDeSerie(nroDeSerie As Integer) As Factura
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroDeSerie
        Return FacturaDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerInformacionEstadisticaDiaria(anio As String, mes As Integer, nroSucursal As Integer) As List(Of EstadisticaDeVenta)
        Return EstadisticaDeVentaDao.instancia().obtenerEstadisticasDiarias(anio, mes, nroSucursal)
    End Function

    Public Function obtenerInformacionEstadisticaAnual(nroSucursal As Integer) As List(Of EstadisticaDeVenta)
        Return EstadisticaDeVentaDao.instancia().obtenerEstadisticasDeVentaAnual(nroSucursal)
    End Function

    Public Function obtenerInformacionEstadisticaMensual(anio As String, nroSucursal As Integer) As List(Of EstadisticaDeVenta)
        Return EstadisticaDeVentaDao.instancia().obtenerEstadisticasDeVentaMensual(anio, nroSucursal)
    End Function

    Public Function armarTablaSugerencias(sugerencias As List(Of SugerenciaDeProducto)) As String
        Dim tablaDeProductos As String = "<table>"
        For Each oProd As SugerenciaDeProducto In sugerencias
            tablaDeProductos = tablaDeProductos & "<tr><td></td><td>" & ConstantesDeMensaje.CHECKOUT_PRODUCTO & "</td><td>" & ConstantesDeMensaje.CHECKOUT_PRECIO & "</td><td></td></tr>"
            tablaDeProductos = tablaDeProductos & "<tr><td><img src='" & oProd.productoSugerido.urlImagen & "' onerror=""this.src='../static/product.jpg';"" id='imgprod" & oProd.productoSugerido.nroDeProducto & "'/></td><td>" & oProd.productoSugerido.nombre & "</td><td>$" & oProd.productoSugerido.precioVenta & "</td><td><a href='/Cliente/Catalogo.aspx?id=" & oProd.catalogo.id & "'>" & ConstantesDeMensaje.IR_A_CATALOGO & "</a></td></tr>"
        Next
        tablaDeProductos = tablaDeProductos & "</table>"
        Return tablaDeProductos
    End Function

    Public Function obtenerSugerenciasDeProductos(oCheckout As Modelo.Checkout) As List(Of SugerenciaDeProducto)
        Dim criterio As New CriterioDeBusqueda
        Dim listaSugerencias As New List(Of SugerenciaDeProducto)
        listaSugerencias.AddRange(obtenerSugerenciasDeProductosConMayorPuntaje())
        For Each oProd As KeyValuePair(Of Integer, Integer) In oCheckout.productos
            criterio.criterioEntero = oProd.Key
            listaSugerencias.AddRange(SugerenciaDeProductoDao.instancia().obtenerMuchos(criterio))
        Next
        Return listaSugerencias
    End Function

    Private Function obtenerSugerenciasDeProductosConMayorPuntaje() As List(Of SugerenciaDeProducto)
        Dim listaSugerencias As New List(Of SugerenciaDeProducto)
        listaSugerencias.AddRange(SugerenciaDeProductoDao.instancia().obtenerMuchos(Nothing))
        Return listaSugerencias
    End Function

    Public Function obtenerFacturaPorNro(nroDeFactura As Integer) As Factura
        Dim criterioFac As New CriterioDeBusqueda
        criterioFac.criterioEntero = nroDeFactura
        criterioFac.criterioString = "porid"
        Dim oFactura As Factura = FacturaDao.instancia().obtenerUno(criterioFac)
        Return oFactura
    End Function

    Public Function cambiarProducto(nroDeSerie As Integer, nroFactura As Integer) As Integer

        Dim criterio As New CriterioDeBusquedaProductoEspecifico
        criterio.criterioEntero = nroDeSerie
        Dim oProd As ProductoEspecificoEnStock = ProductoEspecificoEnStockDao.instancia().obtenerUno(criterio)
        Dim result As Boolean = True
        Dim devolucion As New DevolucionDeProducto
        Dim criterioFactura As New CriterioDeBusqueda
        criterioFactura.criterioEntero = nroDeSerie
        Dim facturaasociada As Factura = FacturaDao.instancia().obtenerUno(criterioFactura)
        If Not facturaasociada Is Nothing Then
            If Not oProd Is Nothing Then
                devolucion.monto = facturaasociada.montoDeCobro
                devolucion.motivo = ""
                devolucion.producto = oProd
                Dim resultado As Integer = DevolucionDeProductoDao.instancia().crear(devolucion)
                If resultado Then
                    oProd.estado.id = 4
                    result = result And ProductoEspecificoEnStockDao.instancia().modificar(oProd)
                End If
            End If

        End If
        Return devolucion.nroDeDevolucion
    End Function

    Public Function obtenerEnvioDeProducto(nroDeSerie As Integer) As Envio
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroDeSerie
        Return EnvioDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerEncuestasPorTipo(idTipo As Integer, fechaDesde As String, fechaHasta As String) As List(Of Encuesta)
        Dim crit As New CriterioDeBusquedaEncuesta
        crit.criterioEntero = idTipo
        crit.fechaDesde = fechaDesde
        crit.fechaHasta = fechaHasta
        Return EncuestaDao.instancia().obtenerMuchos(crit)
    End Function

    Public Function obtenerProximaOrdenEnCola() As OrdenEnCola
        Return OrdenEnColaDao.instancia().obtenerUno(Nothing)
    End Function

    Public Function actualizarOrdenEnCola(orden As OrdenEnCola) As Boolean
        Return OrdenEnColaDao.instancia().modificar(orden)
    End Function
    Public Function eliminarComentarioDeProducto(comentId) As Boolean
        Dim comentario As New ComentarioDeProducto
        comentario.id = comentId
        Return ComentarioDeProductoDao.instancia().eliminar(comentario)
    End Function

    Public Function obtenerComentariosDeProducto(nroProducto As Integer) As List(Of ComentarioDeProducto)
        Dim crti As New CriterioDeBusqueda
        crti.criterioEntero = nroProducto
        Return ComentarioDeProductoDao.instancia().obtenerMuchos(crti)
    End Function

    Public Function crearComentarioDeProducto(oComentario As ComentarioDeProducto) As Boolean
        Return ComentarioDeProductoDao.instancia().crear(oComentario)

    End Function

    Public Function crearEnvio(oEnvio As Envio) As Long
        Dim resultado As Boolean = True
        If oEnvio.contacto.id = 0 Then
            resultado = ContactoDao.instancia().crear(oEnvio.contacto)
        End If
        If resultado Then
            EnvioDao.instancia().crear(oEnvio)
        End If
        Return oEnvio.nroEnvio
    End Function



    Public Function crearOrden(oOrden As Orden) As Long
        OrdenDao.instancia().crear(oOrden)
        Return oOrden.nroDeOrden
    End Function

    Public Function obtenerProductosActivosDeCatalogo(idCatalogo As Integer, precio As Integer, conDescuento As Boolean) As List(Of Producto)
        Dim criterio As New CriterioDeBusquedaDeProducto
        criterio.idCatalogo = idCatalogo
        criterio.idEstadoProducto = 1
        criterio.precio = precio
        criterio.conDescuento = conDescuento
        Return ProductoDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function obtenerTodosLosMetodosDePago() As List(Of MetodoDePago)
        Return MetodoDePagoDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function guardarCheckout(checkout As Checkout) As Checkout
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = checkout.idSesion
        Dim esExitoso As Boolean
        Dim oCheckout As Checkout = CheckoutDao.instancia().obtenerUno(criterio)
        If oCheckout Is Nothing Then
            esExitoso = CheckoutDao.instancia().crear(checkout)
        Else
            esExitoso = CheckoutDao.instancia().modificar(checkout)
        End If
        Return checkout
    End Function

    Public Function obtenerCheckoutPorId(idCheckout As Integer) As Checkout
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = idCheckout
        Return CheckoutDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerTablaDeProductosDeCheckout(oCheckout As Checkout) As String
        Dim tablaDeProductos As String = "<table class='checkout-product-table table table-bordered table-hover'>"
        tablaDeProductos = tablaDeProductos & "<tr><th>" & ConstantesDeMensaje.CHECKOUT_PRODUCTO & "</th><th>" & ConstantesDeMensaje.CHECKOUT_PRECIO & "</th><th>" & ConstantesDeMensaje.CHECKOUT_CANTIDAD & "</th><th>" & ConstantesDeMensaje.CHECKOUT_ELIMINAR & "</th></tr>"
        Dim totalCompra As Double = 0.0
        Dim oProd As Producto
        Dim prodCriteria As New CriterioDeBusqueda
        Dim subtotal As Double = 0
        For Each kvp As KeyValuePair(Of Integer, Integer) In oCheckout.productos
            prodCriteria.criterioEntero = kvp.Key
            prodCriteria.criterioString = ""
            oProd = ProductoDao.instancia().obtenerUno(prodCriteria)
            tablaDeProductos = tablaDeProductos & "<tr><td>" & oProd.nombre & "</td><td>$" & oProd.precioVenta & "</td><td>" & kvp.Value.ToString & "</td><td><a href='#' id='" & kvp.Key & "' onClick='setId(this.id)'>X</a></tr>"
            subtotal = oProd.precioVenta * kvp.Value
            If Not oProd.descuentos Is Nothing AndAlso oProd.descuentos.Any() Then
                For Each oDesc As Descuento In oProd.descuentos
                    If oDesc.monto <> 0 Then
                        subtotal = subtotal - (oDesc.monto * kvp.Value)
                    End If
                    If oDesc.porcentaje <> 0 Then
                        subtotal = subtotal - ((oDesc.porcentaje * subtotal) / 100)
                    End If
                Next
            End If
            totalCompra += subtotal
        Next
        oCheckout.totalAPagar = totalCompra
        tablaDeProductos = tablaDeProductos & "</table>"
        tablaDeProductos = tablaDeProductos & "<div class='precio'><span id='sptotal'>Total</span><span> $" & totalCompra.ToString("F2") & "</span></div>"
        Return tablaDeProductos
    End Function

    Public Function eliminarProductoDeCheckout(oCheck As Checkout, nroProducto As Integer) As Checkout
        oCheck.productos.Item(nroProducto) = 0
        CheckoutDao.instancia().modificar(oCheck)
        oCheck.productos.Remove(nroProducto)
        Return oCheck
    End Function

End Class
