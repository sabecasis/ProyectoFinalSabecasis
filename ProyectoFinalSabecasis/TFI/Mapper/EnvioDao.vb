Imports Modelo
Imports DAL
Imports Seguridad

Public Class EnvioDao
    Inherits AbstractDao(Of Envio)

    Private Sub New()

    End Sub

    Private Shared objeto = New EnvioDao

    Public Shared Function instancia() As EnvioDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Envio) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_ENVIO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            Dim paramNroEnvio = New DbDto
            paramNroEnvio.esParametroDeSalida = True
            paramNroEnvio.parametro = "@nro_de_envio"
            paramNroEnvio.tamanio = 18
            paramNroEnvio.tipoDeDato = SqlDbType.BigInt
            paramNroEnvio.valor = DBNull.Value
            parametros.Add(paramNroEnvio)

            param = New DbDto
            param.valor = oObject.contacto.id
            param.esParametroDeSalida = False
            param.parametro = "@contacto_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.monto.ToString("R").Replace(",", ".")
            param.esParametroDeSalida = False
            param.parametro = "@monto"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.tipo.id
            param.esParametroDeSalida = False
            param.parametro = "@tipo_De_envio_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.sucursal.nroSucursal
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_sucursal"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            If result Then
                oObject.nroEnvio = paramNroEnvio.valor
                query = ConstantesDeDatos.CREAR_PRODUCTOS_DE_ENVIO


                For Each kvp As KeyValuePair(Of Integer, Integer) In oObject.productos
                    parametros = New List(Of DbDto)
                    param = New DbDto
                    param.valor = kvp.Key
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_producto"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)

                    param = New DbDto
                    param.valor = oObject.nroEnvio
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_de_envio"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)

                    param = New DbDto
                    param.valor = kvp.Value
                    param.esParametroDeSalida = False
                    param.parametro = "@cantidad"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)

                    result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Envio) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Envio) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Envio)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Envio
        Dim resultado As Envio = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim data As DataSet
                If oObject.criterioBoolean = False Then
                    Dim dto As New DbDto
                    dto.parametro = "@nro_de_producto_especifico"
                    dto.valor = oObject.criterioEntero
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)
                    query = ConstantesDeDatos.BUSCAR_ENVIO

                Else
                    Dim dto As New DbDto
                    dto.parametro = "@nro_envio"
                    dto.valor = oObject.criterioEntero
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)
                    query = ConstantesDeDatos.BUSCAR_ENVIO_POR_ID
                End If

                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim table As DataTable = data.Tables(0)
                For Each row As DataRow In table.AsEnumerable
                    resultado = New Envio
                    resultado.estado = New Estado
                    resultado.estado.estado = row.Item("estado")
                    resultado.nroEnvio = row.Item("nro_de_envio")
                    resultado.monto = row.Item("monto")
                    resultado.tipo = New TipoDeEnvio
                    resultado.tipo.tipo = row.Item("tipo_de_envio")
                    If Not IsDBNull(row.Item("fecha_enviado")) Then
                        resultado.fechaEnviado = row.Item("fecha_enviado")
                    End If
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return resultado
    End Function
End Class
