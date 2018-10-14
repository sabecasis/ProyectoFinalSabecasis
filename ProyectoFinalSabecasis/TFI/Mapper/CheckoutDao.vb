Imports Modelo
Imports DAL
Imports Seguridad

Public Class CheckoutDao
    Inherits AbstractDao(Of Checkout)

    Private Sub New()

    End Sub

    Private Shared objeto As New CheckoutDao

    Public Shared Function instancia() As CheckoutDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Checkout) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_CHECKOUT
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim paramOut As New DbDto
            paramOut.esParametroDeSalida = True
            paramOut.parametro = "@sesion_id"
            paramOut.tipoDeDato = SqlDbType.BigInt
            paramOut.valor = oObject.idSesion
            paramOut.tamanio = 18
            parametros.Add(paramOut)

            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@usuario_id"
            param.tipoDeDato = SqlDbType.BigInt
            If Not oObject.usuario Is Nothing Then
                param.valor = oObject.usuario.id
            Else
                param.valor = DBNull.Value
            End If
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_envio"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = DBNull.Value
            param.tamanio = 18
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If esExitoso Then
                oObject.idSesion = paramOut.valor
                query = ConstantesDeDatos.CREAR_PRODUCTOS_DE_CHECKOUT

                If Not oObject.productos Is Nothing Then
                    For Each kvp As KeyValuePair(Of Integer, Integer) In oObject.productos
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@producto_id"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = kvp.Key
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@cantidad"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = kvp.Value
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@checkout_id"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oObject.idSesion
                        parametros.Add(param)

                        esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Checkout) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Checkout) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_CHECKOUT
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@sesion_id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.idSesion
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
            param.tamanio = 18
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_envio"
            param.tipoDeDato = SqlDbType.BigInt
            If oObject.envio Is Nothing Then
                param.valor = DBNull.Value
            Else
                If oObject.envio.nroEnvio <> 0 Then
                    param.valor = oObject.envio.nroEnvio
                    param.tamanio = 18
                Else
                    param.valor = DBNull.Value
                End If
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_orden"
            param.tipoDeDato = SqlDbType.BigInt
            If oObject.orden Is Nothing Then
                param.valor = DBNull.Value
            Else
                If oObject.envio.nroEnvio <> 0 Then
                    param.valor = oObject.orden.nroDeOrden
                    param.tamanio = 18
                Else
                    param.valor = DBNull.Value
                End If
            End If
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If esExitoso Then
                If Not oObject.productos Is Nothing Then
                    For Each kvp As KeyValuePair(Of Integer, Integer) In oObject.productos
                        query = ConstantesDeDatos.MODIFICAR_PRODUCTOS_DE_CHECKOUT
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@producto_id"
                        param.valor = kvp.Key
                        param.tipoDeDato = SqlDbType.BigInt
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@checkout_id"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oObject.idSesion
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@cantidad"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = kvp.Value
                        parametros.Add(param)

                        esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                        If esExitoso = False Then
                            query = ConstantesDeDatos.CREAR_PRODUCTOS_DE_CHECKOUT
                            parametros = New List(Of DbDto)

                            param = New DbDto
                            param.esParametroDeSalida = False
                            param.parametro = "@producto_id"
                            param.tipoDeDato = SqlDbType.BigInt
                            param.valor = kvp.Key
                            parametros.Add(param)

                            param = New DbDto
                            param.esParametroDeSalida = False
                            param.parametro = "@cantidad"
                            param.tipoDeDato = SqlDbType.BigInt
                            param.valor = kvp.Value
                            parametros.Add(param)

                            param = New DbDto
                            param.esParametroDeSalida = False
                            param.parametro = "@checkout_id"
                            param.tipoDeDato = SqlDbType.BigInt
                            param.valor = oObject.idSesion
                            parametros.Add(param)

                            ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Checkout)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Checkout
        Dim query As String = ConstantesDeDatos.OBTENER_CHECKOUT
        Dim parametros As New List(Of DbDto)
        Dim resultado As Checkout = Nothing
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@sesion_id"
            param.valor = oObject.criterioEntero
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New Checkout
                resultado.idSesion = row.Item("sesion_checkout_id")
                resultado.estado = New Estado
                resultado.estado.id = row.Item("estado_checkout_id")
                Exit For
            Next

            If Not resultado Is Nothing Then
                query = ConstantesDeDatos.OBTENER_PRODUCTOS_DE_CHECKOUT
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                If dset.Tables.Count > 0 Then
                    resultado.productos = New Dictionary(Of Integer, Integer)
                    dtable = dset.Tables(0)
                    For Each row As DataRow In dtable.AsEnumerable
                        resultado.productos.Add(row.Item("producto_id"), row.Item("cantidad"))
                    Next
                End If
            End If

            Return resultado
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
