Imports Modelo
Imports DAL
Imports Seguridad

Public Class ValoracionDeProductoDao
    Inherits AbstractDao(Of ValoracionDeProducto)

    Private Sub New()

    End Sub

    Private Shared _instancia As New ValoracionDeProductoDao

    Public Shared Function instancia() As ValoracionDeProductoDao
        Return _instancia
    End Function


    Public Overrides Function crear(oObject As ValoracionDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_VALORACION_DE_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim result As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.producto.nroDeProducto
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As ValoracionDeProducto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As ValoracionDeProducto) As Boolean
        Dim query As String = ConstantesDeDatos.INCREMENTAR_VALORACION_DE_PRODUCTO
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.producto.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@valoracion"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.valoracion
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ValoracionDeProducto)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ValoracionDeProducto
        Dim oValoracion As ValoracionDeProducto = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try
            If Not oObject Is Nothing Then
                If Not oObject.criterioEntero = Nothing Then

                    Dim param As New DbDto
                    param.parametro = "@nro_de_producto"
                    param.esParametroDeSalida = False
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.criterioEntero
                    parametros.Add(param)

                    query = ConstantesDeDatos.OBTENER_VALORACION_DE_PRODUCTO
                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim tabla As DataTable = dataSet.Tables(0)
                    For Each row In tabla.AsEnumerable
                        oValoracion = New ValoracionDeProducto
                        oValoracion.valoracion = row.Item("puntaje")
                        oValoracion.producto = New Producto
                        oValoracion.producto.nroDeProducto = row.Item("nro_de_producto")
                        oValoracion.nroDeRating = row.Item("nro_de_rating")
                        oValoracion.cantidadDeVotos = row.Item("cantidad_de_votos")
                        Exit For
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oValoracion
    End Function
End Class
