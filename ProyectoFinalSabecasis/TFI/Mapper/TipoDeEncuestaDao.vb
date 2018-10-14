Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeEncuestaDao
    Inherits AbstractDao(Of TipoDeEncuesta)
    Private Sub New()

    End Sub
    Private Shared objeto As New TipoDeEncuestaDao

    Public Shared Function instancia() As TipoDeEncuestaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As TipoDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As TipoDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As TipoDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeEncuesta)
        Dim query As String = ConstantesDeDatos.OBTENER_TODOS_LOS_TIPOS_DE_ENCUESTA
        Dim resultado As New List(Of TipoDeEncuesta)
        Dim res As TipoDeEncuesta
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                res = New TipoDeEncuesta
                res.id = row.Item("tipo_de_encuesta_id")
                res.tipo = row.Item("tipo_de_encuesta")
                resultado.Add(res)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeEncuesta
        Return Nothing
    End Function
End Class
