Imports Modelo
Imports DAL
Imports Seguridad

Public Class MetodoDeReposicionDao
    Inherits AbstractDao(Of MetodoDeReposicion)

    Private Sub New()

    End Sub

    Private Shared objeto As New MetodoDeReposicionDao

    Public Shared Function instancia() As MetodoDeReposicionDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As MetodoDeReposicion) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As MetodoDeReposicion) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As MetodoDeReposicion) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of MetodoDeReposicion)
        Dim query As String = ConstantesDeDatos.OBTENER_METODOS_REPOSICION
        Dim resultado As New List(Of MetodoDeReposicion)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim metodo As MetodoDeReposicion

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each fila As DataRow In dtable.AsEnumerable
                metodo = New MetodoDeReposicion
                metodo.id = fila.Item("metodo_reposicion_id")
                metodo.metodo = fila.Item("metodo_de_reposicion")
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As MetodoDeReposicion
        Return Nothing
    End Function
End Class
