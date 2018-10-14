Imports Modelo
Imports Seguridad
Imports DAL

Public Class DisponibilidadEnStockDao

    Public Sub New()

    End Sub

    Private Shared objeto As New DisponibilidadEnStockDao

    Public Shared Function instancia() As DisponibilidadEnStockDao
        Return objeto
    End Function


    Public Function agregarStock(oDisponibilidad As DisponibilidadEnStock) As Boolean
        Dim query As String = ConstantesDeDatos.AUMENTAR_EXISTENCIAS_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oDisponibilidad.producto.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oDisponibilidad.sucursal.nroSucursal
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cantidad"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oDisponibilidad.cantidad
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Function quitarStock(oDisponibilidad As DisponibilidadEnStock) As Boolean
        Dim query As String = ConstantesDeDatos.DISMINUIR_EXISTENCIAS_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oDisponibilidad.producto.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oDisponibilidad.sucursal.nroSucursal
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cantidad"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oDisponibilidad.cantidad
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Function consultarStock(oDisponibilidad As DisponibilidadEnStock) As DisponibilidadEnStock
        Dim resultado As DisponibilidadEnStock = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_DISPONIBILIDAD_EN_STOCK
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oDisponibilidad.producto.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oDisponibilidad.sucursal.nroSucursal
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New DisponibilidadEnStock
                resultado.producto = New Producto
                resultado.producto.nroDeProducto = row.Item("nro_de_producto")
                resultado.sucursal = New Sucursal
                resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                resultado.cantidad = row.Item("cantidad")
                Exit For
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
