Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeGarantiaDao
    Inherits AbstractDao(Of TipoDeGarantia)

    Private Sub New()

    End Sub

    Private Shared objeto As New TipoDeGarantiaDao

    Public Shared Function instancia() As TipoDeGarantiaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As TipoDeGarantia) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_TIPO_DE_GARANTIA
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param As New DbDto
            param.parametro = "@id"
            param.valor = oObject.id
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.descripcion
            param.parametro = "@descripcion"
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.parametro = "@dias"
            param.valor = oObject.dias
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.Int
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As TipoDeGarantia) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_TIPO_DE_GARANTIA
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.id
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As TipoDeGarantia) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeGarantia)
        Dim query As String = ConstantesDeDatos.OBTENER_TODOS_LOS_TIPOS_DE_GARANTIA
        Dim resultado As New List(Of TipoDeGarantia)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim tipo As TipoDeGarantia

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each fila As DataRow In dtable.AsEnumerable
                tipo = New TipoDeGarantia
                tipo.id = fila.Item("nro_de_garantía")
                tipo.descripcion = fila.Item("descripcion")
                tipo.dias = fila.Item("vigencia_en_dias")
                resultado.Add(tipo)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_TIPO_GARANTIA)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeGarantia
        Dim query As String = ConstantesDeDatos.OBTENER_TIPO_DE_GARANTIA
        Dim parametros As New List(Of DbDto)
         Dim resultado As TipoDeGarantia = Nothing
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim tipo As TipoDeGarantia = oObject.criterioObjeto
            If Not tipo Is Nothing Then
                Dim param As New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@id"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = tipo.id
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@dias"
                param.tipoDeDato = SqlDbType.Int
                param.valor = tipo.dias
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@descripcion"
                param.tipoDeDato = SqlDbType.VarChar
                param.valor = tipo.descripcion
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                dtable = dset.Tables(0)

                For Each fila As DataRow In dtable.AsEnumerable
                    resultado = New TipoDeGarantia
                    resultado.descripcion = fila.Item("descripcion")
                    resultado.dias = fila.Item("vigencia_en_dias")
                    resultado.id = fila.Item("nro_de_garantía")
                    Exit For
                Next
            Else
                Throw New Exception(ConstantesDeMensaje.CRITERIO_NULO)
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
