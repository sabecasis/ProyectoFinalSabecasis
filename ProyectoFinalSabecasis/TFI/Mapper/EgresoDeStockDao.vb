Imports Modelo
Imports DAL
Imports Seguridad

Public Class EgresoDeStockDao
    Inherits AbstractDao(Of EgresoDeStock)

    Private Sub New()

    End Sub

    Private Shared objeto As New EgresoDeStockDao

    Public Shared Function instancia() As EgresoDeStockDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As EgresoDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_EGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_egreso"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroEgreso
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
            param.parametro = "@estado"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
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
            param.parametro = "@motivo"
            param.tamanio = 300
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.motivo
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

            If resultado Then
                Dim crit As New CriterioDeBusqueda
                Dim disp As New DisponibilidadEnStock
                For Each oProd As ProductoEspecificoEnStock In oObject.productosEspecificosEnStock
                    crit.criterioEntero = oProd.nroDeSerie
                    oProd = ProductoEspecificoEnStockDao.instancia().obtenerUno(crit)
                    oProd.estado.id = 3 'egresado
                    oProd.egreso = New EgresoDeStock
                    oProd.egreso.nroEgreso = oObject.nroEgreso
                    oProd.motivoModificacion = ConstantesDeMotivosDeModificacionDeStock.EGRESO.ToString
                    ProductoEspecificoEnStockDao.instancia().modificar(oProd)
                    disp.producto = oProd.producto
                    disp.sucursal = oProd.sucursal
                    disp.cantidad = 1
                    DisponibilidadEnStockDao.instancia().quitarStock(disp)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As EgresoDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_EGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            If (Not oObject.productosEspecificosEnStock Is Nothing) Then
                For Each oprod As ProductoEspecificoEnStock In oObject.productosEspecificosEnStock
                    Dim disp As New DisponibilidadEnStock
                    disp.cantidad = 1
                    disp.producto = oprod.producto
                    Dim suc As New Sucursal
                    suc.nroSucursal = 1
                    disp.sucursal = suc
                    DisponibilidadEnStockDao.instancia().agregarStock(disp)
                Next
            End If


            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_egreso"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroEgreso
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)


        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As EgresoDeStock) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_EGRESO_DE_STOCK
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_egreso"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroEgreso
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
            param.parametro = "@motivo"
            param.tamanio = 300
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.motivo
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
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

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of EgresoDeStock)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_EGRESO_DE_STOCK)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As EgresoDeStock
        Dim resultado As EgresoDeStock = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_EGRESO_DE_STOCK
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
                resultado = New EgresoDeStock
                resultado.nroEgreso = row.Item("nro_de_egreso")
                If Not IsDBNull(row.Item("comprobante")) Then
                    resultado.comprobante = row.Item("comprobante")
                End If
                resultado.fecha = row.Item("fecha")
                resultado.sucursal = New Sucursal
                resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                resultado.usuario = New Usuario
                resultado.usuario.nombre = row.Item("nombre_usuario")
                resultado.motivo = row.Item("motivo")
                resultado.estado = New Estado
                resultado.estado.id = row.Item("estado_id")
                Exit For
            Next
            If Not resultado Is Nothing Then
                query = ConstantesDeDatos.OBTENER_PRODUCTOS_ESPECIFICOS_DE_EGRESO

                parametros = New List(Of DbDto)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_egreso"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                resultado.productosEspecificosEnStock = New List(Of ProductoEspecificoEnStock)
                For Each row As DataRow In dtable.AsEnumerable
                    Dim prod As New ProductoEspecificoEnStock
                    prod.producto = New Producto
                    prod.producto.nroDeProducto = row.Item("nro_de_producto")
                    prod.nroDeSerie = row.Item("numero_de_serie")
                    resultado.productosEspecificosEnStock.Add(prod)

                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
