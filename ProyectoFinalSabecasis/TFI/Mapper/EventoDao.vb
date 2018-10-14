Imports Modelo
Imports DAL
Imports Seguridad

Public Class EventoDao
    Inherits AbstractDao(Of Evento)

    Private Shared objeto As New EventoDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As EventoDao
        Return objeto
    End Function


    Public Overrides Function crear(oObject As Evento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Evento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Evento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Evento)
        Dim resultado As New List(Of Evento)
        Dim query As String = ""
        Try
            Dim tabla As DataTable
            Dim dset As DataSet
            Dim oEvento As Evento
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_EVENTOS
                dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
                tabla = dset.Tables(0)
                For Each fila As DataRow In tabla.AsEnumerable
                    oEvento = New Evento
                    oEvento.id = fila.Item("evento_id")
                    oEvento.evento = fila.Item("evento")
                    resultado.Add(oEvento)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Evento
        Return Nothing
    End Function
End Class
