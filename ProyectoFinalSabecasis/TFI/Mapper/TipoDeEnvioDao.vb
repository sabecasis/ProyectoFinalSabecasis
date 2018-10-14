Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeEnvioDao
    Inherits AbstractDao(Of TipoDeEnvio)

    Private Shared objeto As New TipoDeEnvioDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As TipoDeEnvioDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As TipoDeEnvio) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As TipoDeEnvio) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As TipoDeEnvio) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeEnvio)
        Dim query As String = ConstantesDeDatos.OBTENER_TODOS_LOS_TIPOS_DE_ENVIO
        Dim resultado As New List(Of TipoDeEnvio)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            Dim result As TipoDeEnvio
            For Each row As DataRow In dtable.AsEnumerable
                result = New TipoDeEnvio
                result.id = row.Item("tipo_de_envio_id")
                result.tipo = row.Item("tipo_de_envio")
                resultado.Add(result)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeEnvio
        Return Nothing
    End Function
End Class
