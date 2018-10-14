Imports Modelo
Imports DAL
Imports Seguridad

Public Class DevolucionDeProductoDao
    Inherits AbstractDao(Of DevolucionDeProducto)


    Private Sub New()

    End Sub

    Private Shared _instancia As New DevolucionDeProductoDao

    Public Shared Function instancia() As DevolucionDeProductoDao
        Return _instancia
    End Function

    Public Overrides Function crear(oObject As DevolucionDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_DEVOLUCION_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try

            param = New DbDto
            param.valor = oObject.monto.ToString("R").Replace(",", ".")
            param.esParametroDeSalida = False
            param.parametro = "@monto"
            param.tamanio = 21
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.producto.nroDeSerie
            param.esParametroDeSalida = False
            param.parametro = "@nro_prod_especifico_en_stock"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.producto.nroDeSerie
            param.esParametroDeSalida = False
            param.parametro = "@motivo"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.nroDeDevolucion
            param.esParametroDeSalida = True
            param.parametro = "@devolucion_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If result Then
                oObject.nroDeDevolucion = parametros.Item(3).valor
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As DevolucionDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As DevolucionDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_DEVOLUCION_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try

            param = New DbDto
            param.valor = oObject.notaDeCredito.nroNotaDeCredito
            param.esParametroDeSalida = False
            param.parametro = "@nro_nota_de_credito"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.nroDeDevolucion
            param.esParametroDeSalida = False
            param.parametro = "@nro_devolucion"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)


            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

           
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of DevolucionDeProducto)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim resultado As New List(Of DevolucionDeProducto)
        Dim descuento As DevolucionDeProducto
        Dim dset As New DataSet
        Dim dtable As DataTable
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODAS_LAS_DEVOLUCIONES_DE_PRODUCTO_ACTIVAS
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New DevolucionDeProducto
                    descuento.nroDeDevolucion = row.Item("devolucion_id")
                    descuento.monto = row.Item("monto_monetario_en_devolucion")
                    descuento.motivo = row.Item("motivo")
                    descuento.fecha = row.Item("fecha")
                    descuento.producto = New ProductoEspecificoEnStock
                    descuento.producto.nroDeSerie = row.Item("numero_de_serie")
                    resultado.Add(descuento)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As DevolucionDeProducto
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim descuento As DevolucionDeProducto = Nothing
        Dim dset As New DataSet
        Dim dtable As DataTable
        Dim param As DbDto
        Try
            If Not oObject Is Nothing Then
                If oObject.criterioBoolean = True Then
                    query = ConstantesDeDatos.OBTENER_DEVOLUCION_POR_NRO_NOTA_DE_CREDITO
                    param = New DbDto
                    param.valor = oObject.criterioEntero
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_nota"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)
                Else
                    query = ConstantesDeDatos.OBTENER_DEVOLUCION_POR_ID
                    param = New DbDto
                    param.valor = oObject.criterioEntero
                    param.esParametroDeSalida = False
                    param.parametro = "@id"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)
                End If

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New DevolucionDeProducto
                    descuento.nroDeDevolucion = row.Item("devolucion_id")
                    descuento.monto = row.Item("monto_monetario_en_devolucion")
                    descuento.motivo = row.Item("motivo")
                    descuento.fecha = row.Item("fecha")
                    descuento.producto = New ProductoEspecificoEnStock
                    descuento.producto.nroDeSerie = row.Item("numero_de_serie")
                    If Not IsDBNull(row.Item("nro_nota_de_credito")) Then
                        descuento.notaDeCredito = New NotaDeCredito
                        descuento.notaDeCredito.nroNotaDeCredito = row.Item("nro_nota_de_credito")
                    End If

                    Exit For
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return descuento
    End Function
End Class
