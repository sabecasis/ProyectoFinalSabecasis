Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeProductoDao
    Inherits AbstractDao(Of TipoDeProducto)

    Private Sub New()

    End Sub

    Private Shared objeto As New TipoDeProductoDao

    Public Shared Function instancia() As TipoDeProductoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As TipoDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As TipoDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As TipoDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeProducto)
        Dim query As String = ConstantesDeDatos.OBTENER_TODOS_LOS_TIPOS_DE_PRODUCTOS
        Dim resultado As New List(Of TipoDeProducto)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim tipo As TipoDeProducto

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each fila As DataRow In dtable.AsEnumerable
                tipo = New TipoDeProducto
                tipo.id = fila.Item("tipo_de_producto_id")
                tipo.tipo = fila.Item("tipo_de_producto")
                resultado.Add(tipo)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeProducto
        Return Nothing
    End Function
End Class
