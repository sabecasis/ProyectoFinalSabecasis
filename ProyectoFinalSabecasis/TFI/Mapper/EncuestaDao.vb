Imports Modelo
Imports DAL
Imports Seguridad

Public Class EncuestaDao
    Inherits AbstractDao(Of Encuesta)

    Private Sub New()

    End Sub

    Private Shared objeto As New EncuestaDao

    Public Shared Function instancia() As EncuestaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Encuesta) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_ENCUESTA
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
            param.valor = oObject.nombre
            param.esParametroDeSalida = False
            param.parametro = "@nombre"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.descripcion
            param.esParametroDeSalida = False
            param.parametro = "@descripcion"
            param.tamanio = 400
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.tipo.id
            param.esParametroDeSalida = False
            param.parametro = "@tipo_de_encuesta_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.fechaDesde
            param.esParametroDeSalida = False
            param.parametro = "@fecha_desde"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.fechaHasta
            param.esParametroDeSalida = False
            param.parametro = "@fecha_hasta"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Encuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Encuesta) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_ENCUESTA
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim result As Boolean = False
        Try
            If oObject.preguntas Is Nothing Then
                Dim paramNroEnvio = New DbDto
                paramNroEnvio.esParametroDeSalida = False
                paramNroEnvio.parametro = "@id"
                paramNroEnvio.tamanio = 18
                paramNroEnvio.tipoDeDato = SqlDbType.BigInt
                paramNroEnvio.valor = oObject.id
                parametros.Add(paramNroEnvio)


                param = New DbDto
                param.valor = oObject.descripcion
                param.esParametroDeSalida = False
                param.parametro = "@descripcion"
                param.tamanio = 400
                param.tipoDeDato = SqlDbType.VarChar
                parametros.Add(param)

                param = New DbDto
                param.valor = oObject.tipo.id
                param.esParametroDeSalida = False
                param.parametro = "@tipo_de_encuesta_id"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                parametros.Add(param)

                param = New DbDto
                param.valor = oObject.fechaDesde
                param.esParametroDeSalida = False
                param.parametro = "@fecha_desde"
                param.tamanio = 20
                param.tipoDeDato = SqlDbType.VarChar
                parametros.Add(param)

                param = New DbDto
                param.valor = oObject.fechaHasta
                param.esParametroDeSalida = False
                param.parametro = "@fecha_hasta"
                param.tamanio = 20
                param.tipoDeDato = SqlDbType.VarChar
                parametros.Add(param)

                result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Else
                query = ConstantesDeDatos.GUARDAR_PREGUNTAS_DE_ENCUESTA
                Dim opciones As String
                For Each preg As PreguntaDeEncuesta In oObject.preguntas
                    opciones = ""
                    For Each opt As OpcionDeEncuesta In preg.respuestas
                        opciones = opciones & opt.opcion & ";"
                    Next
                    Dim paramNroEnvio = New DbDto
                    paramNroEnvio.esParametroDeSalida = False
                    paramNroEnvio.parametro = "@pregunta_id"
                    paramNroEnvio.tamanio = 18
                    paramNroEnvio.tipoDeDato = SqlDbType.BigInt
                    paramNroEnvio.valor = preg.id
                    parametros.Add(paramNroEnvio)


                    param = New DbDto
                    param.valor = preg.pregunta
                    param.esParametroDeSalida = False
                    param.parametro = "@pregunta"
                    param.tamanio = 400
                    param.tipoDeDato = SqlDbType.VarChar
                    parametros.Add(param)


                    param = New DbDto
                    param.valor = oObject.id
                    param.esParametroDeSalida = False
                    param.parametro = "@encuesta_id"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)

                    param = New DbDto
                    param.valor = opciones
                    param.esParametroDeSalida = False
                    param.parametro = "@opciones"
                    param.tamanio = 600
                    param.tipoDeDato = SqlDbType.VarChar
                    parametros.Add(param)

                    param = New DbDto
                    param.valor = preg.activa
                    param.esParametroDeSalida = False
                    param.parametro = "@activa"
                    param.tipoDeDato = SqlDbType.Bit
                    parametros.Add(param)

                    result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                Next

            End If


        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Encuesta)
        Dim query As String = ConstantesDeDatos.OBTENER_ENCUESTAS_POR_TIPO
        Dim resultado As Encuesta = Nothing
        Dim parametros As New List(Of DbDto)
        Dim encuestas As New List(Of Encuesta)
        Dim param As DbDto
        Dim oCriterio As CriterioDeBusquedaEncuesta = oObject
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.valor = oObject.criterioEntero
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)


            Dim dset As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            Dim dtable As DataTable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New Encuesta
                resultado.id = row.Item("encuesta_id")
                resultado.descripcion = row.Item("descripcion")
                resultado.nombre = row.Item("nombre")
                resultado.tipo = New TipoDeEncuesta
                resultado.tipo.id = row.Item("tipo_de_encuesta_id")
                resultado.fechaHasta = Convert.ToDateTime(row.Item("fecha_hasta")).ToString("yyyy-MM-dd")
                resultado.fechaDesde = Convert.ToDateTime(row.Item("fecha_desde")).ToString("yyyy-MM-dd")
                Dim crit As New CriterioDeBusquedaEncuesta
                crit.criterioEntero = resultado.id
                crit.fechaDesde = oCriterio.fechaDesde
                crit.fechaHasta = oCriterio.fechaHasta
                crit.criterioBoolean = True
                resultado.preguntas = PreguntaDeEncuestaDao.instancia().obtenerMuchos(crit)
                encuestas.Add(resultado)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return encuestas
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_ENCUESTA)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Encuesta
        Dim query As String = ConstantesDeDatos.OBTENER_ENCUESTA
        Dim resultado As Encuesta = Nothing
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim oCriterio As CriterioDeBusquedaEncuesta = oObject
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.valor = oObject.criterioEntero
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nombre"
            param.tamanio = 200
            param.valor = oObject.criterioString
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)



            Dim dset As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            Dim dtable As DataTable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New Encuesta
                resultado.id = row.Item("encuesta_id")
                resultado.descripcion = row.Item("descripcion")
                resultado.nombre = row.Item("nombre")
                resultado.tipo = New TipoDeEncuesta
                resultado.tipo.id = row.Item("tipo_de_encuesta_id")
                resultado.fechaDesde = Convert.ToDateTime(row.Item("fecha_hasta")).ToString("yyyy-MM-dd")
                resultado.fechaHasta = Convert.ToDateTime(row.Item("fecha_hasta")).ToString("yyyy-MM-dd")
                Dim crit As New CriterioDeBusquedaEncuesta
                crit.criterioEntero = resultado.id
                crit.criterioBoolean = True
                crit.fechaDesde = oCriterio.fechaDesde
                crit.fechaHasta = oCriterio.fechaHasta
                resultado.preguntas = PreguntaDeEncuestaDao.instancia().obtenerMuchos(crit)
                Exit For
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
