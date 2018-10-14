Imports Modelo
Imports DAL
Imports Seguridad

Public Class IngresoDeStockDao
    Inherits AbstractDao(Of IngresoDeStock)

    Private Sub New()

    End Sub

    Private Shared objeto As New IngresoDeStockDao

    Public Shared Function instancia() As IngresoDeStockDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As IngresoDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_INGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As Boolean = False
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_de_ingreso"
            param.tamanio = 18
            param.valor = oObject.nroIngreso
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.valor = oObject.producto.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_sucursal"
            param.tamanio = 18
            param.valor = oObject.sucursal.nroSucursal
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.Int
            param.parametro = "@cantidad"
            param.valor = oObject.cantidad
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.Binary
            param.tamanio = -1
            param.parametro = "@comprobante"
            If oObject.comprobante Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.comprobante
            End If

            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.VarChar
            param.parametro = "@fecha_de_ingreso"
            param.tamanio = 50
            param.valor = oObject.fecha
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_de_pedido"
            param.tamanio = 18
            param.valor = oObject.solicitud.nroPedido
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@usuario_id"
            param.tamanio = 18
            param.valor = oObject.usuario.id
            parametros.Add(param)


            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            Dim disp As New DisponibilidadEnStock
            disp.cantidad = oObject.cantidad
            disp.producto = oObject.producto
            disp.sucursal = oObject.sucursal
            DisponibilidadEnStockDao.instancia().agregarStock(disp)


        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As IngresoDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_INGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As Boolean = False
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_de_ingreso"
            param.tamanio = 18
            param.valor = oObject.nroIngreso
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Dim disp As New DisponibilidadEnStock
            disp.producto = oObject.producto
            disp.sucursal = oObject.sucursal
            disp.cantidad = oObject.cantidad
            DisponibilidadEnStockDao.instancia().quitarStock(disp)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As IngresoDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_INGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As Boolean = False
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_de_ingreso"
            param.tamanio = 18
            param.valor = oObject.nroIngreso
            parametros.Add(param)


            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.Binary
            param.tamanio = -1
            param.parametro = "@comprobante"
            If oObject.comprobante Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.comprobante
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.VarChar
            param.parametro = "@fecha_de_ingreso"
            param.tamanio = 50
            param.valor = oObject.fecha
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_de_pedido"
            param.tamanio = 18
            param.valor = oObject.solicitud.nroPedido
            parametros.Add(param)


            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of IngresoDeStock)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_INGRESO_DE_STOCK)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As IngresoDeStock
        Dim query As String = ConstantesDeDatos.OBTENER_INGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As IngresoDeStock = Nothing
        Try
            param = New DbDto
            param.valor = oObject.criterioEntero
            param.tipoDeDato = SqlDbType.BigInt
            param.esParametroDeSalida = False
            param.tamanio = 18
            param.parametro = "@id_ingreso"
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)

            For Each row As DataRow In dtable.AsEnumerable
                resultado = New IngresoDeStock
                resultado.nroIngreso = row.Item("nro_de_ingreso")
                resultado.cantidad = row.Item("cantidad")
                resultado.fecha = row.Item("fecha_ingreso")
                resultado.producto = New Producto
                resultado.producto.nroDeProducto = row.Item("nro_de_producto")
                resultado.sucursal = New Sucursal
                resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                resultado.usuario = New Usuario
                resultado.usuario.nombre = row.Item("nombre_usuario")
                resultado.solicitud = New SolicitudDeStock
                resultado.solicitud.nroPedido = row.Item("nro_de_pedido")
                If Not IsDBNull(row.Item("comprobante_de_ingreso")) Then
                    resultado.comprobante = row.Item("comprobante_de_ingreso")
                End If

                Exit For
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
