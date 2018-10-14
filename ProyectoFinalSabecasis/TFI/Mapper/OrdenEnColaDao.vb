Imports Modelo
Imports DAL
Imports Seguridad

Public Class OrdenEnColaDao
    Inherits AbstractDao(Of OrdenEnCola)

    Private Sub New()

    End Sub

    Private Shared objeto As New OrdenEnColaDao

    Public Shared Function instancia() As OrdenEnColaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As OrdenEnCola) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As OrdenEnCola) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As OrdenEnCola) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_ORDEN_EN_COLA
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.valor = oObject.orden.estado.id
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@estado_orden"
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.valor = oObject.estado.id
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@estado_orden_en_cola"
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            If Not oObject.estado.id = 2 Then
                param.valor = 1
            Else
                param.valor = 0
            End If
            param.tipoDeDato = SqlDbType.Bit
            param.parametro = "@finalizado"
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.valor = oObject.orden.nroDeOrden
            param.tipoDeDato = SqlDbType.BigInt
            param.parametro = "@nro_de_orden"
            param.tamanio = 18
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of OrdenEnCola)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As OrdenEnCola
        Dim query As String = ConstantesDeDatos.OBTENER_PROXIMA_ORDEN_EN_COLA
        Dim resultado As OrdenEnCola = Nothing
        Try
            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New OrdenEnCola
                resultado.estado = New Estado
                resultado.estado.id = row.Item("estado_id")
                resultado.fechaInicio = row.Item("fecha_hora_ingreso")
                resultado.nroReintento = row.Item("nro_de_reintento")
                Dim criterio As New CriterioDeBusqueda
                criterio.criterioEntero = row.Item("nro_de_orden")
                resultado.orden = OrdenDao.instancia().obtenerUno(criterio)
                resultado.posicion = row.Item("posicion")
                resultado.prioridad = row.Item("prioridad")
                Exit For
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
