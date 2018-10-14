Imports Modelo
Imports DAL
Imports Seguridad

Public Class FamiliaDeProductoDao
    Inherits AbstractDao(Of FamiliaDeProducto)

    Private Sub New()

    End Sub

    Private Shared objeto As New FamiliaDeProductoDao

    Public Shared Function instancia() As FamiliaDeProductoDao
        Return objeto
    End Function


    Public Overrides Function crear(oObject As FamiliaDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_FAMILIA_DE_PRODUCTO
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@familia"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.familia
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As FamiliaDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_FAMILIA_DE_PRODUCTOS
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
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

    Public Overrides Function modificar(oObject As FamiliaDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of FamiliaDeProducto)
        Dim query As String = ConstantesDeDatos.OBTENER_TODAS_LAS_FAMILIAS_DE_PRODUCTOS
        Dim resultado As New List(Of FamiliaDeProducto)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim familia As FamiliaDeProducto

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each fila As DataRow In dtable.AsEnumerable
                familia = New FamiliaDeProducto
                familia.familia = fila.Item("clasificacion")
                familia.id = fila.Item("clasificacion_id")
                resultado.Add(familia)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_FAMILIA_DE_PRODUCTO)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As FamiliaDeProducto
        Dim query As String = ConstantesDeDatos.OBTENER_FAMILIA_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim familia As FamiliaDeProducto = Nothing
        Dim criterio As FamiliaDeProducto = oObject.criterioObjeto
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim param As New DbDto
            If Not criterio Is Nothing Then
                param.esParametroDeSalida = False
                param.parametro = "@nombre"
                param.valor = criterio.familia
                param.tipoDeDato = SqlDbType.VarChar
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@id"
                param.valor = criterio.id
                param.tipoDeDato = SqlDbType.BigInt
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)

                For Each fila In dtable.AsEnumerable
                    familia = New FamiliaDeProducto
                    familia.familia = fila.Item("clasificacion")
                    familia.id = fila.Item("clasificacion_id")
                    Exit For
                Next
            Else
                Throw New Exception(ConstantesDeMensaje.CRITERIO_NULO)
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return familia
    End Function
End Class
