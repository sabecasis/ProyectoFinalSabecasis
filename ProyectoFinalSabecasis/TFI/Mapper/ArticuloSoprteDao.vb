Imports Modelo
Imports DAL
Imports Seguridad

Public Class ArticuloSoprteDao
    Inherits AbstractDao(Of ArticuloSoporte)
    Private Sub New()

    End Sub

    Private Shared objeto As New ArticuloSoprteDao

    Public Shared Function instancia() As ArticuloSoprteDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As ArticuloSoporte) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_ARTICULO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try

            param = New DbDto
            param.valor = oObject.articulo
            param.esParametroDeSalida = False
            param.parametro = "@articulo"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.contenido
            param.esParametroDeSalida = False
            param.parametro = "@contenido"
            param.tamanio = 500
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As ArticuloSoporte) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_ARTICULO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try

            param = New DbDto
            param.valor = oObject.id
            param.esParametroDeSalida = False
            param.parametro = "@articulo_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function modificar(oObject As ArticuloSoporte) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ArticuloSoporte)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim resultado As New List(Of ArticuloSoporte)
        Dim descuento As ArticuloSoporte
        Dim dset As New DataSet
        Dim dtable As DataTable
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_ARTICULOS_SOPORTE
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New ArticuloSoporte
                    descuento.id = row.Item("articulo_id")
                    descuento.articulo = row.Item("articulo")
                    descuento.contenido = row.Item("contenido")
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ArticuloSoporte
        Return Nothing
    End Function
End Class
