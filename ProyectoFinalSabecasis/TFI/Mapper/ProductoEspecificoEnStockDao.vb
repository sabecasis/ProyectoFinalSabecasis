Imports Modelo
Imports DAL
Imports Seguridad

Public Class ProductoEspecificoEnStockDao
    Inherits AbstractDao(Of ProductoEspecificoEnStock)
    Private Sub New()

    End Sub

    Private Shared objeto As New ProductoEspecificoEnStockDao

    Public Shared Function instancia() As ProductoEspecificoEnStockDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As ProductoEspecificoEnStock) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_PRODUCT_ESPECIFICO
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param2 As New DbDto
            param2.esParametroDeSalida = True
            param2.parametro = "@nro_de_serie"
            param2.tipoDeDato = SqlDbType.BigInt
            param2.tamanio = 18
            param2.valor = DBNull.Value
            parametros.Add(param2)

            Dim param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.producto.nroDeProducto
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sucursal"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.sucursal.nroSucursal
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@precio_de_compra"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.precioCompra.ToString("R").Replace(",", ".")
            param.tamanio = 20
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@precio_de_venta"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.precioVenta.ToString("R").Replace(",", ".")
            param.tamanio = 20
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha_ultima_modificacion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.fechaModificacion.ToString("MM-dd-yyyy")
            param.tamanio = 20
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@motivo_modificacion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.motivoModificacion
            param.tamanio = 200
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_ingreso"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            If Not oObject.ingreso Is Nothing Then
                param.valor = oObject.ingreso.nroIngreso
            Else
                param.valor = DBNull.Value
            End If

            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_egreso"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            If Not oObject.egreso Is Nothing Then
                param.valor = oObject.egreso.nroEgreso
            Else
                param.valor = DBNull.Value
            End If

            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            If esExitoso Then
                oObject.nroDeSerie = param2.valor
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As ProductoEspecificoEnStock) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As ProductoEspecificoEnStock) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_PRODUCTO_ESPECIFICO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Try
            Dim param2 As New DbDto
            param2.esParametroDeSalida = False
            param2.parametro = "@nro_de_serie"
            param2.tipoDeDato = SqlDbType.BigInt
            param2.tamanio = 18
            param2.valor = oObject.nroDeSerie
            parametros.Add(param2)

            Dim param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.producto.nroDeProducto
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sucursal"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.sucursal.nroSucursal
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@precio_de_compra"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.precioCompra.ToString("R").Replace(",", ".")
            param.tamanio = 20
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@precio_de_venta"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.precioVenta.ToString("R").Replace(",", ".")
            param.tamanio = 20
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@motivo_modificacion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.motivoModificacion
            param.tamanio = 200
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_ingreso"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            If Not oObject.ingreso Is Nothing Then
                param.valor = oObject.ingreso.nroIngreso
            Else
                param.valor = DBNull.Value
            End If

            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_egreso"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            If Not oObject.egreso Is Nothing Then
                param.valor = oObject.egreso.nroEgreso
            Else
                param.valor = DBNull.Value
            End If

            parametros.Add(param)
            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ProductoEspecificoEnStock)
        Dim criterio As CriterioDeBusquedaProductoEspecifico = oObject
        Dim resultado As New List(Of ProductoEspecificoEnStock)
        Dim oProd As ProductoEspecificoEnStock
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto

        Try

            If criterio.cantidad > 0 Then
                query = ConstantesDeDatos.OBTENER_PRODUCTOS_ESPECIFICOS_DE_STOCK
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_producto"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = criterio.nroProducto
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_sucursal"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = criterio.nroSucursal
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@cantidad"
                param.tipoDeDato = SqlDbType.Int
                param.valor = criterio.cantidad
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@orden"
                param.tamanio = 3
                param.tipoDeDato = SqlDbType.VarChar
                param.valor = criterio.orden
                parametros.Add(param)

            Else
                query = ConstantesDeDatos.OBTENER_PRODUCTOS_ESPECIFICOS_POR_USUARIO

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@usuario_id"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = criterio.idUsuario
                parametros.Add(param)
            End If

            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                oProd = New ProductoEspecificoEnStock
                oProd.nroDeSerie = row.Item("numero_de_serie")
                oProd.sucursal = New Sucursal
                oProd.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                oProd.motivoModificacion = row.Item("motivo_modificacion")
                oProd.estado = New Estado
                oProd.estado.id = row.Item("estado_inst_prod_id")
                If Not IsDBNull(row.Item("nro_de_egreso")) Then
                    oProd.egreso = New EgresoDeStock
                    oProd.egreso.nroEgreso = row.Item("nro_de_egreso")
                End If
                oProd.fechaModificacion = row.Item("fecha_ultima_modificacion")
                oProd.precioCompra = row.Item("precio_compra")
                oProd.precioVenta = row.Item("precio_venta")
                oProd.producto = New Producto
                oProd.producto.nroDeProducto = row.Item("nro_de_producto")
                Dim critProd As New CriterioDeBusqueda
                critProd.criterioEntero = oProd.producto.nroDeProducto
                critProd.criterioString = ""
                oProd.producto = ProductoDao.instancia().obtenerUno(critProd)
                oProd.estado = New Estado
                oProd.estado.id = row.Item("estado_inst_prod_id")
                If criterio.cantidad = 0 Then
                    oProd.garantia = New Garantia
                    oProd.garantia.nroGarantia = row.Item("nro_de_garantia")
                    oProd.garantia.fechaFin = row.Item("fecha_de_fin")
                 
                    oProd.estado.estado = row.Item("estado_prod_especifico")
                End If
               
                resultado.Add(oProd)

            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ProductoEspecificoEnStock
        Dim resultado As ProductoEspecificoEnStock = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_PRODUCTO_ESPECIFICO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New ProductoEspecificoEnStock
                resultado.nroDeSerie = row.Item("numero_de_serie")
                resultado.sucursal = New Sucursal
                resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                resultado.motivoModificacion = row.Item("motivo_modificacion")
                resultado.ingreso = New IngresoDeStock
                resultado.ingreso.nroIngreso = row.Item("nro_de_ingreso")
                resultado.estado = New Estado
                resultado.estado.id = row.Item("estado_inst_prod_id")
                resultado.estado.estado = row.Item("estado_prod_especifico")
                If Not IsDBNull(row.Item("nro_de_egreso")) Then
                    resultado.egreso = New EgresoDeStock
                    resultado.egreso.nroEgreso = row.Item("nro_de_egreso")
                End If
                resultado.fechaModificacion = row.Item("fecha_ultima_modificacion")
                Dim crit As New CriterioDeBusqueda
                crit.criterioEntero = resultado.nroDeSerie
                resultado.garantia = GarantiaDao.instancia().obtenerUno(crit)

                resultado.precioCompra = row.Item("precio_compra")
                resultado.precioVenta = row.Item("precio_venta")
                resultado.producto = New Producto
                resultado.producto.nroDeProducto = row.Item("nro_de_producto")
                Exit For
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
