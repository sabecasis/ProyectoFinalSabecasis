Imports Modelo
Imports DAL
Imports Seguridad

Public Class OrdenDao
    Inherits AbstractDao(Of Orden)

    Private Sub New()

    End Sub


    Private Shared objeto As New OrdenDao

    Public Shared Function instancia() As OrdenDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Orden) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_ORDEN
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            Dim paramNroOrden = New DbDto
            paramNroOrden.esParametroDeSalida = True
            paramNroOrden.parametro = "@nro_de_orden"
            paramNroOrden.tamanio = 18
            paramNroOrden.tipoDeDato = SqlDbType.BigInt
            paramNroOrden.valor = DBNull.Value
            parametros.Add(paramNroOrden)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@usuario_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.usuario.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cant_cuotas"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.cuotas
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_envio"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.envio.nroEnvio
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@metodo_de_pago_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.informacionDePago.metodo.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@titular"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            If Not oObject.informacionDePago.titular Is Nothing Then
                param.valor = oObject.informacionDePago.titular
            Else
                param.valor = DBNull.Value
            End If
            parametros.Add(param)

            

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@numero_tarjeta"
            param.tamanio = 50
            param.tipoDeDato = SqlDbType.VarChar
            If Not oObject.informacionDePago.nroDeTarjeta Is Nothing Then
                param.valor = Criptografia.ObtenerInstancia().CypherTripleDES(oObject.informacionDePago.nroDeTarjeta, ConstantesDeSeguridad.ENC_KEY, True)
            Else
                param.valor = DBNull.Value
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cvv"
            param.tamanio = 3
            param.tipoDeDato = SqlDbType.VarChar
            If Not oObject.informacionDePago.cvv Is Nothing Then
                param.valor = Criptografia.ObtenerInstancia().CypherTripleDES(oObject.informacionDePago.cvv, ConstantesDeSeguridad.ENC_KEY, True)
            Else
                param.valor = DBNull.Value
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@mes_vencimiento"
            param.tamanio = 2
            param.tipoDeDato = SqlDbType.VarChar
            If Not oObject.informacionDePago.mesVencimiento Is Nothing Then
                param.valor = oObject.informacionDePago.mesVencimiento
            Else
                param.valor = DBNull.Value
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@anio_vencimiento"
            param.tamanio = 4
            param.tipoDeDato = SqlDbType.VarChar
            If Not oObject.informacionDePago.anioVencimiento Is Nothing Then
                param.valor = oObject.informacionDePago.anioVencimiento
            Else
                param.valor = DBNull.Value
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@persona_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.informacionDePago.persona.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@total_a_pagar"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.totalAPagar.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@recargo_por_tajeta"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.recargoPorTarjeta.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_nota_de_credito"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.BigInt
            If oObject.informacionDePago.nroNotaDeCredito <> 0 Then
                param.valor = oObject.informacionDePago.nroNotaDeCredito
            Else
                param.valor = DBNull.Value
            End If

            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)


            If result Then
                oObject.nroDeOrden = paramNroOrden.valor
                query = ConstantesDeDatos.CREAR_PRODUCTOS_DE_ORDEN
                For Each kvp As DetalleDeOrden In oObject.detalles
                    parametros = New List(Of DbDto)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@producto_id"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = kvp.producto.nroDeProducto
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_de_orden"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.nroDeOrden
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@cantidad"
                    param.tipoDeDato = SqlDbType.Int
                    param.valor = kvp.cantidad
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@monto"
                    param.tipoDeDato = SqlDbType.Int
                    param.valor = kvp.monto
                    parametros.Add(param)

                    result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                Next

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Orden) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Orden) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_ORDEN
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            Dim paramNroOrden = New DbDto
            paramNroOrden.esParametroDeSalida = False
            paramNroOrden.parametro = "@nro_de_orden"
            paramNroOrden.tamanio = 18
            paramNroOrden.tipoDeDato = SqlDbType.BigInt
            paramNroOrden.valor = oObject.nroDeOrden
            parametros.Add(paramNroOrden)


            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@total_a_pagar"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.totalAPagar.ToString("R").Replace(",", ".")
            parametros.Add(param)

            paramNroOrden = New DbDto
            paramNroOrden.esParametroDeSalida = False
            paramNroOrden.parametro = "@nro_de_egreso"
            paramNroOrden.tamanio = 18
            paramNroOrden.tipoDeDato = SqlDbType.BigInt
            If oObject.egreso Is Nothing Then
                paramNroOrden.valor = DBNull.Value
            ElseIf oObject.egreso.nroEgreso = 0 Then
                paramNroOrden.valor = DBNull.Value
            Else
                paramNroOrden.valor = oObject.egreso.nroEgreso
            End If

            parametros.Add(paramNroOrden)

            paramNroOrden = New DbDto
            paramNroOrden.esParametroDeSalida = False
            paramNroOrden.parametro = "@nro_de_sucursal"
            paramNroOrden.tamanio = 18
            paramNroOrden.tipoDeDato = SqlDbType.BigInt
            If oObject.sucursal Is Nothing Then
                paramNroOrden.valor = DBNull.Value
            ElseIf oObject.sucursal.nroSucursal = 0 Then
                paramNroOrden.valor = DBNull.Value
            Else
                paramNroOrden.valor = oObject.sucursal.nroSucursal
            End If

            parametros.Add(paramNroOrden)

            paramNroOrden = New DbDto
            paramNroOrden.esParametroDeSalida = False
            paramNroOrden.parametro = "@estado_id"
            paramNroOrden.tamanio = 18
            paramNroOrden.tipoDeDato = SqlDbType.BigInt
            If oObject.estado.id = 0 Then
                paramNroOrden.valor = DBNull.Value
            Else
                paramNroOrden.valor = oObject.estado.id
            End If

            parametros.Add(paramNroOrden)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Orden)
        Dim ordenes As New List(Of Orden)
        Dim resultado As Orden = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_ORDENES
        Dim oCriterio As CriterioDeBusquedaDeOrden = oObject
        Dim parametros As New List(Of DbDto)
        Try
            If Not oCriterio Is Nothing Then

                Dim param As New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_de_orden"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oCriterio.nroDeOrden
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nro_de_factura"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oCriterio.nroDeFactura
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@fecha_desde"
                param.tamanio = 20
                param.tipoDeDato = SqlDbType.VarChar
                If Not oCriterio.fechaDesde Is Nothing Then
                    param.valor = oCriterio.fechaDesde
                Else
                    param.valor = ""
                End If
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@usuario"
                param.tamanio = 200
                param.tipoDeDato = SqlDbType.VarChar
                If Not oCriterio.usuario Is Nothing Then
                    param.valor = oCriterio.usuario
                Else
                    param.valor = ""
                End If

                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@fecha_hasta"
                param.tamanio = 20
                param.tipoDeDato = SqlDbType.VarChar
                If Not oCriterio.fechaHasta Is Nothing Then
                    param.valor = oCriterio.fechaHasta
                Else
                    param.valor = ""
                End If

                parametros.Add(param)

                Dim dset As DataSet
                Dim dtable As DataTable
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    resultado = New Orden
                    If IsDBNull(row.Item("cuotas")) Then
                        resultado.cuotas = 1
                    Else
                        resultado.cuotas = row.Item("cuotas")
                    End If
                    resultado.estado = New Estado
                    resultado.estado.id = row.Item("estado_id")
                    resultado.fechaInicio = row.Item("fecha_inicio")
                    resultado.informacionDePago = New InformacionDePago
                    resultado.informacionDePago.id = row.Item("informacion_id")
                    If Not IsDBNull(row.Item("nro_nota_de_credito")) Then
                        resultado.informacionDePago.nroNotaDeCredito = row.Item("nro_nota_de_credito")
                    End If

                    If IsDBNull(row.Item("anio_vencimiento")) Then
                        resultado.informacionDePago.anioVencimiento = Nothing
                    Else
                        resultado.informacionDePago.anioVencimiento = row.Item("anio_vencimiento")
                    End If
                    If Not IsDBNull(row.Item("cvv")) Then
                        resultado.informacionDePago.cvv = Criptografia.ObtenerInstancia().DecypherTripleDES(row.Item("cvv"), ConstantesDeSeguridad.ENC_KEY, True)
                    End If

                    If Not IsDBNull(row.Item("mes_vencimiento")) Then
                        resultado.informacionDePago.mesVencimiento = row.Item("mes_vencimiento")
                    End If
                    Dim criterioMetodo As New CriterioDeBusqueda
                    criterioMetodo.criterioEntero = row.Item("metodo_de_pago_id")
                    resultado.informacionDePago.metodo = MetodoDePagoDao.instancia().obtenerUno(criterioMetodo)

                    If Not IsDBNull(row.Item("numero_tarjeta")) Then
                        resultado.informacionDePago.nroDeTarjeta = Criptografia.ObtenerInstancia().DecypherTripleDES(row.Item("numero_tarjeta"), ConstantesDeSeguridad.ENC_KEY, True)
                    End If

                    resultado.informacionDePago.persona = New Persona
                    resultado.informacionDePago.persona.id = row.Item("persona_id")
                    If Not IsDBNull(row.Item("titular")) Then
                        resultado.informacionDePago.titular = row.Item("titular")
                    End If
                    resultado.nroDeOrden = row.Item("nro_de_orden")
                    resultado.totalAPagar = row.Item("total_a_pagar")
                    resultado.usuario = New Usuario
                    resultado.usuario.id = row.Item("usuario_id")
                    resultado.usuario.nombre = row.Item("nombre_usuario")
                    Dim critusuario As New CriterioDeBusqueda
                    critusuario.criterioString = resultado.usuario.nombre
                    resultado.usuario = UsuarioDao.instancia().obtenerUno(critusuario)
                    resultado.envio = New Envio
                    resultado.envio.nroEnvio = row.Item("nro_de_envio")
                    resultado.envio.monto = row.Item("monto_envio")
                    If Not IsDBNull(row.Item("nro_de_egreso")) Then
                        resultado.egreso = New EgresoDeStock
                        resultado.egreso.nroEgreso = row.Item("nro_de_egreso")
                    End If
                    If Not IsDBNull(row.Item("nro_de_sucursal")) Then
                        resultado.sucursal = New Sucursal
                        resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                    End If
                    resultado.recargoPorTarjeta = row.Item("recargo_por_tarjeta")
                    resultado.factura = New Factura()
                    Dim criterioFactura As New CriterioDeBusqueda
                    criterioFactura.criterioBoolean = True
                    criterioFactura.criterioEntero = resultado.nroDeOrden
                    resultado.factura = FacturaDao.instancia().obtenerUno(criterioFactura)
                    ordenes.Add(resultado)
                Next

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return ordenes
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Orden
        Dim resultado As Orden = Nothing
        Dim query As String = ConstantesDeDatos.OBTENER_ORDEN
        Dim parametros As New List(Of DbDto)
        If Not oObject Is Nothing Then
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_orden"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New Orden
                If IsDBNull(row.Item("cuotas")) Then
                    resultado.cuotas = 1
                Else
                    resultado.cuotas = row.Item("cuotas")
                End If
                resultado.estado = New Estado
                resultado.estado.id = row.Item("estado_id")
                resultado.fechaInicio = row.Item("fecha_inicio")
                resultado.informacionDePago = New InformacionDePago
                resultado.informacionDePago.id = row.Item("informacion_id")
                If Not IsDBNull(row.Item("nro_nota_de_credito")) Then
                    resultado.informacionDePago.nroNotaDeCredito = row.Item("nro_nota_de_credito")
                End If

                If IsDBNull(row.Item("anio_vencimiento")) Then
                    resultado.informacionDePago.anioVencimiento = Nothing
                Else
                    resultado.informacionDePago.anioVencimiento = row.Item("anio_vencimiento")
                End If
                If Not IsDBNull(row.Item("cvv")) Then
                    resultado.informacionDePago.cvv = Criptografia.ObtenerInstancia().DecypherTripleDES(row.Item("cvv"), ConstantesDeSeguridad.ENC_KEY, True)
                End If

                If Not IsDBNull(row.Item("mes_vencimiento")) Then
                    resultado.informacionDePago.mesVencimiento = row.Item("mes_vencimiento")
                End If
                Dim criterioMetodo As New CriterioDeBusqueda
                criterioMetodo.criterioEntero = row.Item("metodo_de_pago_id")
                resultado.informacionDePago.metodo = MetodoDePagoDao.instancia().obtenerUno(criterioMetodo)

                If Not IsDBNull(row.Item("numero_tarjeta")) Then
                    resultado.informacionDePago.nroDeTarjeta = Criptografia.ObtenerInstancia().DecypherTripleDES(row.Item("numero_tarjeta"), ConstantesDeSeguridad.ENC_KEY, True)
                End If

                resultado.informacionDePago.persona = New Persona
                resultado.informacionDePago.persona.id = row.Item("persona_id")
                If Not IsDBNull(row.Item("titular")) Then
                    resultado.informacionDePago.titular = row.Item("titular")
                End If
                resultado.nroDeOrden = row.Item("nro_de_orden")
                resultado.totalAPagar = row.Item("total_a_pagar")
                resultado.usuario = New Usuario
                resultado.usuario.id = row.Item("usuario_id")
                resultado.usuario.nombre = row.Item("nombre_usuario")
                Dim critusuario As New CriterioDeBusqueda
                critusuario.criterioString = resultado.usuario.nombre
                resultado.usuario = UsuarioDao.instancia().obtenerUno(critusuario)
                Dim criterioEnvio As New CriterioDeBusqueda
                criterioEnvio.criterioBoolean = True
                criterioEnvio.criterioEntero = row.Item("nro_de_envio")
                resultado.envio = EnvioDao.instancia().obtenerUno(criterioEnvio)
                If Not IsDBNull(row.Item("nro_de_egreso")) Then
                    resultado.egreso = New EgresoDeStock
                    resultado.egreso.nroEgreso = row.Item("nro_de_egreso")
                End If
                If Not IsDBNull(row.Item("nro_de_sucursal")) Then
                    resultado.sucursal = New Sucursal
                    resultado.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                End If
                resultado.recargoPorTarjeta = row.Item("recargo_por_tarjeta")
                Exit For
            Next

            query = ConstantesDeDatos.OBTENER_PRODUCTOS_DE_ORDEN
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            Dim detalle As DetalleDeOrden
            resultado.detalles = New List(Of DetalleDeOrden)
            For Each row As DataRow In dtable.AsEnumerable
                detalle = New DetalleDeOrden
                detalle.cantidad = row.Item("cantidad")
                detalle.monto = row.Item("monto_de_pago")
                detalle.producto = New Producto
                detalle.producto.nroDeProducto = row.Item("nro_de_producto")
                detalle.producto.nombre = row.Item("nombre_producto")
                Dim criterioProd As New CriterioDeBusqueda
                criterioProd.criterioEntero = detalle.producto.nroDeProducto
                criterioProd.criterioString = detalle.producto.nombre
                detalle.producto = ProductoDao.instancia().obtenerUno(criterioProd)
                detalle.producto.garantia = New TipoDeGarantia
                detalle.producto.garantia.dias = row.Item("vigencia_en_dias")
                resultado.detalles.Add(detalle)
            Next

        End If
        Return resultado
    End Function
End Class
