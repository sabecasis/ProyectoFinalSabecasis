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
            tablaDeProductos = tablaDeProductos & "<tr><td><img src='" & oProd.productoSugerido.urlImagen & "' id='imgprod" & oProd.productoSugerido.nroDeProducto & "'/></td><td>" & oProd.productoSugerido.nombre & "</td><td>$" & oProd.productoSugerido.precioVenta & "</td><td><a href='/Cliente/Catalogo.aspx?id=" & oProd.catalogo.id & "'>" & ConstantesDeMensaje.IR_A_CATALOGO & "</a></td></tr>"
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

    Public Function cambiarProducto(nroDeSerie As Integer, nroFactura As Integer) As Integer
        Dim criterio As New CriterioDeBusquedaProductoEspecifico
        criterio.criterioEntero = nroDeSerie
        Dim oProd As ProductoEspecificoEnStock = ProductoEspecificoEnStockDao.instancia().obtenerUno(criterio)
        Dim result As Boolean = True
        Dim oNota As New NotaDeCredito
        If Not oProd Is Nothing Then
            oProd.estado.id = 4
            result = result And ProductoEspecificoEnStockDao.instancia().modificar(oProd)
            oNota.monto = oProd.precioVenta
            oNota.descripcion = "Devolución de producto: " & oProd.nroDeSerie
            oNota.factura = New Factura
            oNota.factura.nroFactura = nroFactura
            result = result And NotaDeCreditoDao.instancia().crear(oNota)
        End If
        Return oNota.nroNotaDeCredito
    End Function

    Public Function obtenerEnvioDeProducto(nroDeSerie As Integer) As Envio
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroDeSerie
        Return EnvioDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerEncuestasPorTipo(idTipo As Integer) As List(Of Encuesta)
        Dim crit As New CriterioDeBusqueda
        crit.criterioEntero = idTipo
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
        If checkout.idSesion = 0 Then
            Return Nothing
        End If
        Dim oCheckout As Checkout = CheckoutDao.instancia().obtenerUno(criterio)
        If oCheckout Is Nothing Then
            esExitoso = CheckoutDao.instancia().crear(checkout)
        Else
            esExitoso = CheckoutDao.instancia().modificar(checkout)
        End If
        Return checkout
    End Function

    Public Function obtenerCheckoutPorId(idCheckout As Integer) As Checkout

    End Function

    Public Function obtenerTablaDeProductosDeCheckout(oCheckout As Checkout) As String
        Dim tablaDeProductos As String = "<table class='checkout-product-table table table-bordered table-hover'>"
        tablaDeProductos = tablaDeProductos & "<tr><th>" & ConstantesDeMensaje.CHECKOUT_PRODUCTO & "</th><th>" & ConstantesDeMensaje.CHECKOUT_PRECIO & "</th><th>" & ConstantesDeMensaje.CHECKOUT_CANTIDAD & "</th><th>" & ConstantesDeMensaje.CHECKOUT_ELIMINAR & "</th></tr>"
        Dim totalCompra As Double = 0.0
        Dim oProd As Producto
        Dim prodCriteria As New CriterioDeBusqueda
        For Each kvp As KeyValuePair(Of Integer, Integer) In oCheckout.productos
            prodCriteria.criterioEntero = kvp.Key
            prodCriteria.criterioString = ""
            oProd = ProductoDao.instancia().obtenerUno(prodCriteria)
            tablaDeProductos = tablaDeProductos & "<tr><td>" & oProd.nombre & "</td><td>$" & oProd.precioVenta & "</td><td>" & kvp.Value.ToString & "</td><td><a href='#' id='" & kvp.Key & "' onClick='setId(this.id)'>X</a></tr>"
            totalCompra += oProd.precioVenta * kvp.Value
        Next
        oCheckout.totalAPagar = totalCompra
        tablaDeProductos = tablaDeProductos & "</table>"
        tablaDeProductos = tablaDeProductos & "<div class='precio'><span id='sptotal'>Total</span><span> $" & totalCompra.ToString & "</span></div>"
        Return tablaDeProductos
    End Function

    Public Function eliminarProductoDeCheckout(oCheck As Checkout, nroProducto As Integer) As Checkout
        oCheck.productos.Item(nroProducto) = 0
        CheckoutDao.instancia().modificar(oCheck)
        oCheck.productos.Remove(nroProducto)
        Return oCheck
    End Function

End Class
