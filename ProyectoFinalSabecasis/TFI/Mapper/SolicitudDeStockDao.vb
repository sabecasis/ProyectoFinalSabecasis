Imports Modelo
Imports DAL
Imports Seguridad

Public Class SolicitudDeStockDao
    Inherits AbstractDao(Of SolicitudDeStock)

    Private Sub New()

    End Sub

    Private Shared objeto As New SolicitudDeStockDao

    Public Shared Function instancia() As SolicitudDeStockDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As SolicitudDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_SOLICITUD_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_solicitud"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroPedido
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.sucursal.nroSucursal
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.producto.nroDeProducto
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
            param.parametro = "@completado"
            param.tipoDeDato = SqlDbType.Bit
            param.valor = oObject.estaIngersado
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cantidad"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.cantidad
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            If String.IsNullOrEmpty(oObject.fecha) Then
                param.valor = Date.Now.ToString("yyyy-MM-dd")
            Else
                param.valor = oObject.fecha
            End If

            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@comprobante"
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.Binary
            If oObject.comprobante Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.comprobante
            End If
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As SolicitudDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_SOLICITUD_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@id"
            param.tamanio = 18
            param.valor = oObject.nroPedido
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As SolicitudDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_SOLICITUD_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.sucursal.nroSucursal
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@producto_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.producto.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_solicitud"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroPedido
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@completado"
            param.tipoDeDato = SqlDbType.Bit
            param.valor = oObject.estaIngersado
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cantidad"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.cantidad
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            param.valor = oObject.fecha
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

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of SolicitudDeStock)
        Dim oCrit As CriterioDeBusquedaSolicitud = oObject
        Dim resultado As New List(Of SolicitudDeStock)
        Dim parametros As New List(Of DbDto)
        Dim result As SolicitudDeStock
        Dim param As DbDto
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODAS_LAS_SOLICITUDES_POR_ESTADO
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@estado"
                param.tipoDeDato = SqlDbType.Bit
                If oCrit.criterioBoolean Then
                    param.valor = 1
                Else
                    param.valor = 0
                End If
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_producto"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oCrit.nroProducto
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_sucursal"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oCrit.nroSucursal
                parametros.Add(param)

                Dim dset As DataSet
                Dim dtable As DataTable

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    result = New SolicitudDeStock
                    result.nroPedido = row.Item("nro_de_pedido")
                    If row.Item("flag_aceptado") = 0 Then
                        result.estaIngersado = False
                    Else
                        result.estaIngersado = True
                    End If

                    result.cantidad = row.Item("cantidad")
                    If Not IsDBNull(row.Item("comprobante")) Then
                        result.comprobante = row.Item("comprobante")
                    End If
                    result.fecha = row.Item("fecha")
                    result.producto = New Producto()
                    result.producto.nroDeProducto = row.Item("producto_id")
                    result.sucursal = New Sucursal
                    result.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                    result.usuario = New Usuario
                    result.usuario.nombre = row.Item("nombre_usuario")
                    resultado.Add(result)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_PEDIDO_STOCK)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As SolicitudDeStock
        Dim resultado As SolicitudDeStock = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_SOLICITUD_DE_STOCK
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
                resultado = New SolicitudDeStock
                resultado.nroPedido = row.Item("nro_de_pedido")
                resultado.estaIngersado = row.Item("flag_aceptado")
                resultado.cantidad = row.Item("cantidad")
                If Not IsDBNull(row.Item("comprobante")) Then
                    resultado.comprobante = row.Item("comprobante")
                End If
                resultado.fecha = row.Item("fecha")
                resultado.producto = New Producto()
                resultado.producto.nroDeProducto = row.Item("producto_id")
                resultado.sucursal = New Sucursal
                resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                resultado.usuario = New Usuario
                resultado.usuario.nombre = row.Item("nombre_usuario")
                Exit For
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
