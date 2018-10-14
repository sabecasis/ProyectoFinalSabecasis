Imports Modelo
Imports DAL
Imports Seguridad

Public Class OpcionDeEncuestaDao
    Inherits AbstractDao(Of OpcionDeEncuesta)
    Private Sub New()

    End Sub

    Private Shared objeto As New OpcionDeEncuestaDao

    Public Shared Function instancia() As OpcionDeEncuestaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As OpcionDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As OpcionDeEncuesta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As OpcionDeEncuesta) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.MODIFICAR_OPCION_DE_PREGUNTA
                esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of OpcionDeEncuesta)
        Dim query As String = ConstantesDeDatos.OBTENER_OPCIONES_DE_PREGUNTA
        Dim resultado As OpcionDeEncuesta = Nothing
        Dim parametros As New List(Of DbDto)
        Dim respuesta As New List(Of OpcionDeEncuesta)
        Dim oCriterio As CriterioDeBusquedaEncuesta = oObject
        Dim param As DbDto
        Try
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
                resultado = New OpcionDeEncuesta
                resultado.id = row.Item("opcion_id")
                resultado.activa = row.Item("activa")
                resultado.opcion = row.Item("opcion")
                resultado.cantidadDeSelecciones = row.Item("cantidad_de_selecciones")
                respuesta.Add(resultado)
            Next

            If respuesta.Count > 0 Then
                For Each opc As OpcionDeEncuesta In respuesta
                    query = ConstantesDeDatos.OBTENER_VARIACIONES_DE_PREGUNTA_EN_EL_TIEMPO
                    parametros = New List(Of DbDto)
                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@opcion_id"
                    param.tamanio = 18
                    param.valor = opc.id
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@fecha_desde"
                    param.tamanio = 20
                    param.valor = oCriterio.fechaDesde
                    param.tipoDeDato = SqlDbType.VarChar
                    parametros.Add(param)


                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@fecha_hasta"
                    param.tamanio = 20
                    param.valor = oCriterio.fechaHasta
                    param.tipoDeDato = SqlDbType.VarChar
                    parametros.Add(param)

                    opc.variaciones = New List(Of VariacionDePreguntaDeEncuesta)
                    Dim variacion As VariacionDePreguntaDeEncuesta
                    dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    If dset.Tables.Count > 0 Then
                        dtable = dset.Tables(0)
                        For Each row As DataRow In dtable.AsEnumerable
                            variacion = New VariacionDePreguntaDeEncuesta()
                            variacion.cantidad = row.Item("cantidad")
                            variacion.fecha = row.Item("fecha")
                            opc.variaciones.Add(variacion)
                        Next
                    End If

                Next
            End If


        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return respuesta
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As OpcionDeEncuesta
        Return Nothing
    End Function
End Class
