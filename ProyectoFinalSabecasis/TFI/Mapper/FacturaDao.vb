Imports Modelo
Imports DAL
Imports Seguridad

Public Class FacturaDao
    Inherits AbstractDao(Of Factura)

    Private Sub New()

    End Sub


    Private Shared objeto As New FacturaDao

    Public Shared Function instancia() As FacturaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Factura) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_FACTURA
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            Dim param1 = New DbDto
            param1.esParametroDeSalida = True
            param1.parametro = "@nro_factura"
            param1.tamanio = 18
            param1.tipoDeDato = SqlDbType.BigInt
            param1.valor = DBNull.Value
            parametros.Add(param1)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.sucursal.nroSucursal
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@monto_venta"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.montoDeVenta.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_egreso"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = DBNull.Value
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@usuario_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.usuario.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha_de_venta"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.fechaDeVenta.ToString("yyyy-MM-dd")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@monto_de_cobro"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.montoDeCobro.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@tipo_de_factura_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.tipoDeFactura.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_orden"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.orden.nroDeOrden
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_nota_de_credito"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            If oObject.notaDeCreditoAplicada Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.notaDeCreditoAplicada.nroNotaDeCredito
            End If

            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@comprobante"
            param.tipoDeDato = SqlDbType.Binary
            param.tamanio = -1
            If oObject.comprobante Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.comprobante
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@email_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            If oObject.email Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.email.id
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@recargo_por_tarjeta"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.recargoPorTarjeta.ToString("R").Replace(",", ".")
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If result Then
                oObject.nroFactura = param1.valor

                query = ConstantesDeDatos.GUARDAR_DETALLES_DE_FACTURA

                For Each oDetalle As DetalleDeFactura In oObject.detallesDeFactura
                    parametros = New List(Of DbDto)
                    Dim param2 = New DbDto
                    param2.esParametroDeSalida = True
                    param2.parametro = "@id"
                    param2.tamanio = 18
                    param2.tipoDeDato = SqlDbType.BigInt
                    param2.valor = DBNull.Value
                    parametros.Add(param2)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_factura"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.nroFactura
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_producto"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oDetalle.producto.nroDeProducto
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_sucursal"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oDetalle.sucursal.nroSucursal
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@cantidad"
                    param.tipoDeDato = SqlDbType.Int
                    param.valor = oDetalle.cantidad
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@tipo_de_factura_id"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oDetalle.tipoDeFactura.id
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@precio_unitario"
                    param.tamanio = 20
                    param.tipoDeDato = SqlDbType.VarChar
                    param.valor = oDetalle.precioUnitario.ToString("R").Replace(",", ".")
                    parametros.Add(param)

                  

                    result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                    If result Then
                        oDetalle.id = param2.valor
                    End If
                Next

            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Factura) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Factura) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_FACTURA
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As Boolean = False
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.tamanio = 18
            param.valor = oObject.nroFactura
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_factura"
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tamanio = 18
            If Not oObject.egresoDeStock Is Nothing Then
                param.valor = oObject.egresoDeStock.nroEgreso
            Else
                param.valor = DBNull.Value
            End If
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_egreso"
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.valor = oObject.comprobante
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.Binary
            param.parametro = "@comprobante"
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tamanio = 18
            If Not oObject.email Is Nothing Then
                param.valor = oObject.nroFactura
            Else
                param.valor = DBNull.Value
            End If
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@email_id"
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Factura)
        Dim notas As New List(Of Factura)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then
                If oObject.criterioBoolean = True Then
                    query = ConstantesDeDatos.OBTENER_FACTURAS_PAGADAS_CON_NOTA_DE_CREDITO

                    Dim param As New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_de_nota_de_credito"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.criterioEntero
                    parametros.Add(param)
                Else
                    query = ConstantesDeDatos.OBTENER_FACTURAS_DE_USUARIO

                    Dim param As New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@usuario_id"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.criterioEntero
                    parametros.Add(param)
                End If
              

                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)
                Dim oNota As Factura
                For Each row In dataTable.AsEnumerable
                    oNota = New Factura()
                    oNota.nroFactura = row.Item("nro_de_factura")
                    oNota.montoDeCobro = row.Item("monto_de_cobro")
                    oNota.recargoPorTarjeta = row.Item("recargo_por_tarjeta")
                    oNota.montoDeVenta = row.Item("monto_de_venta")
                    oNota.fechaDeCobro = row.Item("fecha_de_cobro")
                    If Not IsDBNull(row.Item("nro_nota_de_credito_aplicada")) Then
                        oNota.notaDeCreditoAplicada = New NotaDeCredito
                        oNota.notaDeCreditoAplicada.nroNotaDeCredito = row.Item("nro_nota_de_credito_aplicada")
                    End If

                    notas.Add(oNota)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return notas
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Factura
        Dim resultado As Factura = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            If oObject.criterioBoolean = True Then
                If oObject.criterioString Is Nothing Then
                    query = ConstantesDeDatos.OBTENER_FACTURA_POR_NRO_DE_ORDEN

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_de_orden"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.criterioEntero
                    parametros.Add(param)
                Else
                    query = ConstantesDeDatos.OBTENER_FACTURA_POR_NRO_DE_FACTURA

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_de_factura"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.criterioEntero
                    parametros.Add(param)
                End If
            Else
                query = ConstantesDeDatos.OBTENER_FACTURA_POR_NRO_DE_SERIE

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_de_serie"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                parametros.Add(param)
            End If
                Dim dset As DataSet
                Dim dtable As DataTable

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    resultado = New Factura
                    resultado.nroFactura = row.Item("nro_de_factura")
                    resultado.montoDeCobro = row.Item("monto_de_cobro")
                    resultado.fechaDeCobro = row.Item("fecha_de_cobro")
                    If IsDBNull(row.Item("comprobante_de_factura")) Then
                        resultado.comprobante = Nothing
                    Else
                        resultado.comprobante = row.Item("comprobante_de_factura")
                    End If
                    resultado.orden = New Orden
                    resultado.orden.nroDeOrden = row.Item("nro_de_orden")
                    resultado.recargoPorTarjeta = row.Item("recargo_por_tarjeta")
                    Exit For
                Next

            If Not resultado Is Nothing Then
                parametros = New List(Of DbDto)
                query = ConstantesDeDatos.OBTENER_DETALLES_DE_FACTURA

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_factura"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = resultado.nroFactura
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                resultado.detallesDeFactura = New List(Of DetalleDeFactura)
                Dim detalle As DetalleDeFactura
                For Each row As DataRow In dtable.AsEnumerable
                    detalle = New DetalleDeFactura
                    detalle.id = row.Item("detalle_de_factura_id")
                    detalle.cantidad = row.Item("cantidad")
                    detalle.precioUnitario = row.Item("precio_unitario")
                    detalle.producto = New Producto
                    detalle.producto.nroDeProducto = row.Item("nro_de_producto")
                    Dim critProd As New CriterioDeBusqueda
                    critProd.criterioEntero = detalle.producto.nroDeProducto
                    critProd.criterioString = ""
                    detalle.producto = ProductoDao.instancia().obtenerUno(critProd)
                    detalle.sucursal = New Sucursal
                    detalle.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                    detalle.tipoDeFactura = New TipoDeFactura
                    detalle.tipoDeFactura.id = row.Item("tipo_de_factura")
                    resultado.detallesDeFactura.Add(detalle)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
