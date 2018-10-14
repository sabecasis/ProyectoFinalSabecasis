Imports Modelo
Imports DAL
Imports Seguridad

Public Class MetodoValoracionDao
    Inherits AbstractDao(Of MetodoValoracion)

    Private Sub New()

    End Sub

    Private Shared objeto As New MetodoValoracionDao

    Public Shared Function instancia() As MetodoValoracionDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As MetodoValoracion) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As MetodoValoracion) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As MetodoValoracion) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of MetodoValoracion)
        Dim query As String = ConstantesDeDatos.OBTENER_METODOS_VALORACION
        Dim resultado As New List(Of MetodoValoracion)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim metodo As MetodoValoracion

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each fila As DataRow In dtable.AsEnumerable
                metodo = New MetodoValoracion
                metodo.id = fila.Item("metodo_valoracion_id")
                metodo.metodo = fila.Item("nombre_metodo")
                metodo.descripcion = fila.Item("descripcion")
                resultado.Add(metodo)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As MetodoValoracion
        Return Nothing
    End Function
End Class
