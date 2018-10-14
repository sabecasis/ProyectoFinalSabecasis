Imports Modelo
Imports DAL
Imports Seguridad

Public Class ComentarioDeChatDao
    Inherits AbstractDao(Of ComentarioDeChat)
    Private Sub New()

    End Sub

    Private Shared objeto As New ComentarioDeChatDao

    Public Shared Function instancia() As ComentarioDeChatDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As ComentarioDeChat) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_COMENTARIO_DE_CHAT
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            Dim paramfecha = New DbDto
            paramfecha.esParametroDeSalida = True
            paramfecha.parametro = "@fecha_hora"
            paramfecha.tipoDeDato = SqlDbType.VarChar
            paramfecha.tamanio = 30
            paramfecha.valor = DBNull.Value
            parametros.Add(paramfecha)

            param = New DbDto
            param.valor = oObject.usuario.id
            param.esParametroDeSalida = False
            param.parametro = "@usuario_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.comentario
            param.esParametroDeSalida = False
            param.parametro = "@comentario"
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.sesion.id
            param.esParametroDeSalida = False
            param.parametro = "@sesion_de_chat"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If result Then
                oObject.fecha = paramfecha.valor
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As ComentarioDeChat) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As ComentarioDeChat) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ComentarioDeChat)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ComentarioDeChat
        Dim resultado As New List(Of Evento)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim coment As ComentarioDeChat = Nothing
        Try
            Dim tabla As DataTable
            Dim dset As DataSet

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@sesion_chat_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            If Not oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_ULTIMO_COMENTARIO

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                tabla = dset.Tables(0)
                For Each fila As DataRow In tabla.AsEnumerable
                    coment = New ComentarioDeChat
                    coment.fecha = fila.Item("fecha_hora")
                    coment.usuario = New Usuario
                    coment.usuario.id = fila.Item("usuario_id")
                    coment.usuario.nombre = fila.Item("nombre_usuario")
                    coment.comentario = fila.Item("comentario")
                    Exit For
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return coment
    End Function
End Class
