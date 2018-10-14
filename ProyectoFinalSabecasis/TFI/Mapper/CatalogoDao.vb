Imports Modelo
Imports DAL
Imports Seguridad

Public Class CatalogoDao
    Inherits AbstractDao(Of Catalogo)
    Private Sub New()

    End Sub

    Private Shared objeto As New CatalogoDao

    Public Shared Function instancia() As CatalogoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Catalogo) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Catalogo) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Catalogo) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Catalogo)
        Dim query As String = ConstantesDeDatos.OBTENER_TODOS_LOS_CATALOGOS
        Dim resultado As New List(Of Catalogo)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim catalogo As Catalogo

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each fila As DataRow In dtable.AsEnumerable
                catalogo = New Catalogo
                catalogo.id = fila.Item("catalogo_id")
                catalogo.catalogo = fila.Item("catalogo")
                catalogo.descripcion = fila.Item("descripcion")
                'TODO agregar obtencion de productos
                resultado.Add(catalogo)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Catalogo
        Return Nothing
    End Function
End Class
