Imports Modelo
Imports DAL
Imports Seguridad

Public Class EstadoDao
    Inherits AbstractDao(Of Estado)

    Private Sub New()

    End Sub

    Private Shared objeto As New EstadoDao

    Public Shared Function instancia() As EstadoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Estado) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Estado) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Estado) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Estado)
        Dim query As String = ConstantesDeDatos.OBTENER_ESTADOS
        Dim resultado As New List(Of Estado)
        Dim parametros As New List(Of DbDto)
        Try
            Dim tabla As DataTable
            Dim dset As DataSet

            Dim dto As New DbDto
            dto.parametro = "@tipo"
            dto.valor = oObject.criterioString
            dto.tipoDeDato = SqlDbType.VarChar
            dto.esParametroDeSalida = False
            parametros.Add(dto)

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            tabla = dset.Tables(0)
            Dim estado As Estado
            For Each fila As DataRow In tabla.AsEnumerable
                estado = New Estado
                estado.estado = fila.Item("estado")
                estado.id = fila.Item("id")
                resultado.Add(estado)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Estado
        Return Nothing
    End Function
End Class
