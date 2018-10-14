Imports Modelo
Imports DAL
Imports Seguridad

Public Class ComentarioDeProductoDao
    Inherits AbstractDao(Of ComentarioDeProducto)

    Private Sub New()

    End Sub

    Private Shared objeto As New ComentarioDeProductoDao

    Public Shared Function instancia() As ComentarioDeProductoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As ComentarioDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_COMENTARIO_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@comentario"
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.comentario
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_producto"
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

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As ComentarioDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_COMENTARIO_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.id
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As ComentarioDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ComentarioDeProducto)
        Dim query As String = ConstantesDeDatos.OBTENER_ULTIMOS_COMENTARIOS_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As New List(Of ComentarioDeProducto)
        Dim comentario As ComentarioDeProducto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)

            For Each row As DataRow In dtable.AsEnumerable
                comentario = New ComentarioDeProducto
                comentario.comentario = row.Item("comentario")
                comentario.fecha = DirectCast(row.Item("fecha"), DateTime)
                comentario.id = row.Item("comentario_id")
                comentario.producto = New Producto
                comentario.producto.nroDeProducto = row.Item("nro_de_producto")
                comentario.usuario = New Usuario
                comentario.usuario.nombre = row.Item("nombre_usuario")
                comentario.usuario.id = row.Item("usuario_id")
                resultado.Add(comentario)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ComentarioDeProducto
        Return Nothing
    End Function
End Class
