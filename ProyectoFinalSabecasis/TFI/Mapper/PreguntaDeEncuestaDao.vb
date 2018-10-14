Imports Modelo
Imports DAL
Imports Seguridad

Public Class PreguntaDeEncuestaDao
    Inherits AbstractDao(Of PreguntaDeEncuesta)
    Private Sub New()

    End Sub

    Private Shared objeto As New PreguntaDeEncuestaDao

    Public Shared Function instancia() As PreguntaDeEncuestaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As PreguntaDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As PreguntaDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As PreguntaDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of PreguntaDeEncuesta)
        Dim query As String = ""
        Dim resultado As PreguntaDeEncuesta = Nothing
        Dim parametros As New List(Of DbDto)
        Dim respuesta As New List(Of PreguntaDeEncuesta)
        Dim oCriterio As CriterioDeBusquedaEncuesta = oObject
        Dim param As DbDto
        Try
            If oObject.criterioBoolean = True Then
                query = ConstantesDeDatos.OBTENER_PREGUNTAS_DE_ENCUESTA
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@encuesta_id"
                param.tamanio = 18
                param.valor = oObject.criterioEntero
                param.tipoDeDato = SqlDbType.BigInt
                parametros.Add(param)


                Dim dset As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dtable As DataTable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    resultado = New PreguntaDeEncuesta
                    resultado.id = row.Item("pregunta_id")
                    resultado.activa = row.Item("activa")
                    resultado.pregunta = row.Item("pregunta")
                    Dim crit As New CriterioDeBusquedaEncuesta
                    crit.criterioEntero = resultado.id
                    crit.fechaDesde = oCriterio.fechaDesde
                    crit.fechaHasta = oCriterio.fechaHasta
                    resultado.respuestas = OpcionDeEncuestaDao.instancia().obtenerMuchos(crit)
                    respuesta.Add(resultado)
                Next
            Else
                query = ConstantesDeDatos.OBTENER_PREGUNTAS_POR_TIPO
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@tipo_de_encuesta_id"
                param.tamanio = 18
                param.valor = oObject.criterioEntero
                param.tipoDeDato = SqlDbType.BigInt
                parametros.Add(param)


                Dim dset As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dtable As DataTable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    resultado = New PreguntaDeEncuesta
                    resultado.id = row.Item("pregunta_id")
                    resultado.activa = row.Item("activa")
                    resultado.pregunta = row.Item("pregunta")
                    Dim crit As New CriterioDeBusquedaEncuesta
                    crit.criterioEntero = resultado.id
                    crit.fechaDesde = oCriterio.fechaDesde
                    crit.fechaHasta = oCriterio.fechaHasta
                    resultado.respuestas = OpcionDeEncuestaDao.instancia().obtenerMuchos(crit)
                    respuesta.Add(resultado)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return respuesta
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_PREGUNTA_ENCUESTA)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As PreguntaDeEncuesta
        Dim query As String = ""
        Dim resultado As PreguntaDeEncuesta = Nothing
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim oCriterio As CriterioDeBusquedaEncuesta = oObject
        Try
                query = ConstantesDeDatos.OBTENER_PREGUNTA_DE_ENCUESTA
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@pregunta_id"
                param.tamanio = 18
                param.valor = oObject.criterioEntero
                param.tipoDeDato = SqlDbType.BigInt
                parametros.Add(param)


                Dim dset As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dtable As DataTable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    resultado = New PreguntaDeEncuesta
                    resultado.id = row.Item("pregunta_id")
                    resultado.activa = row.Item("activa")
                    resultado.pregunta = row.Item("pregunta")
                    resultado.encuesta = New Encuesta
                    resultado.encuesta.id = row.Item("encuesta_id")
                Dim crit As New CriterioDeBusquedaEncuesta
                crit.criterioEntero = resultado.id
                crit.fechaDesde = oCriterio.fechaDesde
                crit.fechaHasta = oCriterio.fechaHasta
                    resultado.respuestas = OpcionDeEncuestaDao.instancia().obtenerMuchos(crit)
                    Exit For
                Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
