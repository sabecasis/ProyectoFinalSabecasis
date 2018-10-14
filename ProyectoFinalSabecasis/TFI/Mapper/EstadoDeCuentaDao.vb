Imports Modelo
Imports DAL
Imports Seguridad

Public Class EstadoDeCuentaDao
    Inherits AbstractDao(Of EstadoDeCuenta)

    Private Sub New()

    End Sub

    Private Shared _instancia As New EstadoDeCuentaDao

    Public Shared Function instancia() As EstadoDeCuentaDao
        Return _instancia
    End Function

    Public Overrides Function crear(oObject As EstadoDeCuenta) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_ESTADO_DE_CUENTA
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            param.esParametroDeSalida = False
            param.parametro = "@persona_id"
            param.valor = oObject.persona.id
            parametros.Add(param)

            Dim result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Return result
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return False
    End Function

    Public Overrides Function eliminar(oObject As EstadoDeCuenta) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As EstadoDeCuenta) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_ESTADO_DE_CUENTA
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            param.esParametroDeSalida = False
            param.parametro = "@persona_id"
            param.valor = oObject.persona.id
            parametros.Add(param)

            param = New DbDto
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            param.esParametroDeSalida = False
            param.parametro = "@monto_haber"
            param.valor = oObject.totalEnCredito.ToString("R")
            parametros.Add(param)

            param = New DbDto
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            param.esParametroDeSalida = False
            param.parametro = "@monto_debe"
            param.valor = oObject.totalEnDebito.ToString("R")
            parametros.Add(param)

            Dim result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Return result
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return False
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of EstadoDeCuenta)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As EstadoDeCuenta
        If oObject.criterioEntero <> 0 Then
            Dim query As String = ConstantesDeDatos.OBTENER_ESTADO_DE_CUENTA_POR_PERSONA
            Dim resultado As EstadoDeCuenta = Nothing
            Dim parametros As New List(Of DbDto)
            Try
                Dim dset As DataSet
                Dim dtable As DataTable

                Dim param As New DbDto
                param.esParametroDeSalida = True
                param.parametro = "@persona_id"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    resultado = New EstadoDeCuenta
                    resultado.totalEnCredito = row.Item("monto_haber")
                    resultado.totalEnDebito = row.Item("monto_debe")
                    resultado.persona = New Persona
                    resultado.persona.id = row.Item("persona_id")
                    Exit For
                Next
                Return resultado
            Catch ex As Exception
                Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
            End Try
        End If
        Return Nothing
    End Function
End Class
