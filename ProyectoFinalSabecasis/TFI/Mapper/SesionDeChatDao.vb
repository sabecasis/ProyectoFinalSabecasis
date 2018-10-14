Imports Modelo
Imports DAL
Imports Seguridad

Public Class SesionDeChatDao
    Inherits AbstractDao(Of SesionDeChat)
    Private Sub New()

    End Sub


    Private Shared objeto As New SesionDeChatDao

    Public Shared Function instancia() As SesionDeChatDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As SesionDeChat) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_SESION_DE_CHAT
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            Dim paramNroEnvio = New DbDto
            paramNroEnvio.esParametroDeSalida = True
            paramNroEnvio.parametro = "@id"
            paramNroEnvio.tamanio = 18
            paramNroEnvio.tipoDeDato = SqlDbType.BigInt
            paramNroEnvio.valor = DBNull.Value
            parametros.Add(paramNroEnvio)

            param = New DbDto
            param.valor = oObject.usuario.id
            param.esParametroDeSalida = False
            param.parametro = "@usuario_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.estado.id
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If result Then
                oObject.id = paramNroEnvio.valor
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As SesionDeChat) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As SesionDeChat) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_SESION_DE_CHAT
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            Dim paramNroEnvio = New DbDto
            paramNroEnvio.esParametroDeSalida = False
            paramNroEnvio.parametro = "@id"
            paramNroEnvio.tamanio = 18
            paramNroEnvio.tipoDeDato = SqlDbType.BigInt
            paramNroEnvio.valor = oObject.id
            parametros.Add(paramNroEnvio)

            param = New DbDto
            param.valor = oObject.asesor.id
            param.esParametroDeSalida = False
            param.parametro = "@usuario_soporte_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.estado.id
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of SesionDeChat)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As SesionDeChat
        Return Nothing
    End Function
End Class
