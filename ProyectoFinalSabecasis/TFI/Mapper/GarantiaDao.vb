Imports Modelo
Imports DAL
Imports Seguridad

Public Class GarantiaDao
    Inherits AbstractDao(Of Garantia)

    Private Sub New()

    End Sub

    Private Shared objeto As New GarantiaDao

    Public Shared Function instancia() As GarantiaDao
        Return objeto
    End Function


    Public Overrides Function crear(oObject As Garantia) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_GARANTIA
        Dim parametros As New List(Of DbDto)
        Dim parametro As DbDto
        Dim result As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_garantia"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            param.valor = oObject.nroGarantia
            parametros.Add(param)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nro_de_serie"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.productoEspecifico.nroDeSerie
            parametro.tamanio = 18
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@fecha_inicio"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.fechaInicio.ToString("MM-dd-yyyy")
            parametro.tamanio = 20
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@fecha_fin"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.fechaFin.ToString("MM-dd-yyyy")
            parametro.tamanio = 20
            parametros.Add(parametro)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Garantia) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Garantia) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Garantia)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Garantia
        Dim resultado As Garantia = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_GARANTIA_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New Garantia
                resultado.nroGarantia = row.Item("nro_de_garantia")
                resultado.fechaFin = row.Item("fecha_de_fin")
                Exit For
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
