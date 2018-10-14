Imports Modelo
Imports DAL
Imports Seguridad

Public Class ElementoDeBitacoraDao
    Inherits AbstractDao(Of ElementoDeBitacora)

    Private Shared objeto As New ElementoDeBitacoraDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As ElementoDeBitacoraDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As ElementoDeBitacora) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim parametro As New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@usuario_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.usuario.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@evento_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.evento.id
            parametros.Add(parametro)

            query = ConstantesDeDatos.GUARDAR_EN_BITACORA
            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As ElementoDeBitacora) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As ElementoDeBitacora) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ElementoDeBitacora)
        Dim criterio As CriterioDeBusquedaBitacora = oObject
        Dim tabla As DataTable
        Dim dset As DataSet
        Dim resultado As New List(Of ElementoDeBitacora)
        Dim oElemento As ElementoDeBitacora
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not criterio Is Nothing Then

                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@usuario"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = criterio.usuario
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@fecha"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = criterio.fecha
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@fecha_hasta"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = criterio.fechaHasta
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@idEvento"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.valor = criterio.idEvento
                parametros.Add(parametro)

                query = ConstantesDeDatos.OBTENER_BITACORA
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                tabla = dset.Tables(0)

                For Each fila As DataRow In tabla.AsEnumerable
                    oElemento = New ElementoDeBitacora
                    oElemento.evento = New Evento
                    oElemento.evento.evento = fila.Item("evento")
                    oElemento.fechaDesde = fila.Item("fecha")
                    oElemento.hora = fila.Item("hora").ToString()
                    oElemento.id = fila.Item("bitacora_id")
                    oElemento.usuario = New Usuario
                    oElemento.usuario.nombre = fila.Item("nombre_usuario")
                    resultado.Add(oElemento)
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ElementoDeBitacora
        Return Nothing
    End Function
End Class
