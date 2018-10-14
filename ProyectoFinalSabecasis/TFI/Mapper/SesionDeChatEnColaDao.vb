Imports Modelo
Imports DAL
Imports Seguridad

Public Class SesionDeChatEnColaDao
    Inherits AbstractDao(Of SesionDeChatEnCola)
    Private Sub New()

    End Sub

    Private Shared objeto As New SesionDeChatEnColaDao

    Public Shared Function instancia() As SesionDeChatEnColaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As SesionDeChatEnCola) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_SESION_DE_CHAT_EN_COLA
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            param = New DbDto
            param.valor = oObject.sesion.id
            param.esParametroDeSalida = False
            param.parametro = "@sesion_chat_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.estado.id
            param.esParametroDeSalida = False
            param.parametro = "@estado_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As SesionDeChatEnCola) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As SesionDeChatEnCola) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_SESION_DE_CHAT_EN_COLA
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            param = New DbDto
            param.valor = oObject.id
            param.esParametroDeSalida = False
            param.parametro = "@sesion_chat_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.estado.id
            param.esParametroDeSalida = False
            param.parametro = "@estado_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of SesionDeChatEnCola)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As SesionDeChatEnCola
        Dim sesionDeChat As SesionDeChatEnCola = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try

            If Not oObject Is Nothing Then
                If oObject.criterioEntero <> 0 Then
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@sesion_de_chat_id"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_SESION_DE_CHAT
                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim dataTable As DataTable = dataSet.Tables(0)

                    For Each row In dataTable.AsEnumerable
                        sesionDeChat = New SesionDeChatEnCola
                        sesionDeChat.id = row.Item("sesion_en_cola_id")
                        sesionDeChat.estado = New Estado
                        sesionDeChat.estado.id = row.Item("estado_id")
                        sesionDeChat.sesion = New SesionDeChat
                        sesionDeChat.sesion.id = row.Item("sesion_id")
                        sesionDeChat.sesion.asesor = New Usuario
                        sesionDeChat.sesion.asesor.id = row.Item("usuario_id")
                        sesionDeChat.sesion.asesor.nombre = row.Item("nombre_usuario")
                        Exit For
                    Next
              
                End If
            Else
                query = ConstantesDeDatos.OBTENE_PROXIMA_SESION_DE_CHAT
                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)

                For Each row In dataTable.AsEnumerable
                    sesionDeChat = New SesionDeChatEnCola
                    sesionDeChat.id = row.Item("sesion_en_cola_id")
                    sesionDeChat.estado = New Estado
                    sesionDeChat.estado.id = row.Item("estado_id")
                    sesionDeChat.sesion = New SesionDeChat
                    sesionDeChat.sesion.id = row.Item("sesion_id")
                    sesionDeChat.sesion.usuario = New Usuario
                    sesionDeChat.sesion.usuario.id = row.Item("usuario_id")
                    sesionDeChat.sesion.usuario.nombre = row.Item("nombre_usuario")
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return sesionDeChat
    End Function
End Class
