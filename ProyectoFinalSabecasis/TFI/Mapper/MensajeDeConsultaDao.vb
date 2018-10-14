Imports Modelo
Imports DAL
Imports Seguridad

Public Class MensajeDeConsultaDao
    Inherits AbstractDao(Of MensajeDeConsulta)
    Private Sub New()

    End Sub

    Private Shared objeto As New MensajeDeConsultaDao

    Public Shared Function instancia() As MensajeDeConsultaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As MensajeDeConsulta) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_MENSAJE_DE_CONSULTA
        Dim parametros As New List(Of DbDto)
        Dim parametro As DbDto
        Dim result As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@mensaje"
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.mensaje
            parametros.Add(param)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@email"
            param.tamanio = 200
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.email
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@identificacion"
            parametro.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 200
            parametro.valor = oObject.identificacion
            parametros.Add(parametro)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As MensajeDeConsulta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As MensajeDeConsulta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of MensajeDeConsulta)
        Dim tabla As DataTable
        Dim dset As DataSet
        Dim resultado As New List(Of MensajeDeConsulta)
        Dim oElemento As MensajeDeConsulta
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@estado_leido"
                parametro.tipoDeDato = SqlDbType.Bit
                parametro.valor = oObject.criterioEntero
                parametros.Add(parametro)

                query = ConstantesDeDatos.OBTENER_MENSAJES_DE_CONSULTA_POR_ESTADO
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                tabla = dset.Tables(0)

                For Each fila As DataRow In tabla.AsEnumerable
                    oElemento = New MensajeDeConsulta
                    oElemento.fecha = fila.Item("fecha")
                    oElemento.mensaje = fila.Item("mensaje")
                    oElemento.id = fila.Item("mensaje_id")
                    oElemento.email = fila.Item("email")
                    oElemento.identificacion = fila.Item("identificacion")
                    oElemento.leido = fila.Item("flag_leido")
                    oElemento.respondido = fila.Item("flag_respondido")
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As MensajeDeConsulta
        Return Nothing
    End Function
End Class
