Imports Modelo
Imports Seguridad
Imports DAL

Public Class SugerenciaDeProductoDao
    Inherits AbstractDao(Of SugerenciaDeProducto)
    Private Sub New()

    End Sub

    Private Shared objeto As New SugerenciaDeProductoDao

    Public Shared Function instancia() As SugerenciaDeProductoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As SugerenciaDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_SUGERENCIA_DE_PRODUCTO
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
           
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.productoComprado.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sugerencia"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.productoSugerido.nroDeProducto
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As SugerenciaDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_SUGERENCIA_DE_PRODUCTO
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.productoComprado.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_sugerencia"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.productoSugerido.nroDeProducto
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function modificar(oObject As SugerenciaDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of SugerenciaDeProducto)
        Dim resultado As New List(Of SugerenciaDeProducto)
        Dim oProd As SugerenciaDeProducto
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto

        Try
            If (oObject Is Nothing) Then
                query = ConstantesDeDatos.OBTENER_PRODUCTOS_CON_MAYOR_PUNTAJE
            Else
                query = ConstantesDeDatos.OBTENER_SUGERENCIAS_DE_PRODUCTOS
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_de_producto"
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
                oProd = New SugerenciaDeProducto
                oProd.productoComprado = New Producto
                If (Not oObject Is Nothing) Then
                    oProd.productoComprado.nroDeProducto = oObject.criterioEntero
                End If

                oProd.productoSugerido = New Producto
                oProd.productoSugerido.nroDeProducto = row.Item("nro_de_producto")
                oProd.productoSugerido.nombre = row.Item("nombre_producto")
                oProd.productoSugerido.descripcion = row.Item("descripcion_producto")
                oProd.productoSugerido.precioVenta = row.Item("precio_venta")
                oProd.productoSugerido.urlImagen = row.Item("url_imagen")
                oProd.catalogo = New Catalogo
                oProd.catalogo.id = row.Item("catalogo_id")
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As SugerenciaDeProducto
        Return Nothing
    End Function
End Class
