Imports Modelo
Imports DAL
Imports Seguridad

Public Class ProductoDao
    Inherits AbstractDao(Of Producto)


    Private Shared objeto As New ProductoDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As ProductoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Producto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@garantia"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.garantia.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cantidad"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.cantidad
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@caracteristicas"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = -1

            param.valor = oObject.caracteristicas
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@ciclo"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.ciclo
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@costo_posesion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.costoDePosesion.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@costo_estandar"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.costoEstandar.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@costo_financiero"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.costoFinanciero.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@descripcion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.descripcion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@familia"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.familiaDeProducto.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@metodo_reposicion"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.metodoDeReposicion.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@metodo_valoracion"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.metodoDeValoracion.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nombre"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.nombre
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@porcentaje_ganancia"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.porcentajeDeGanancia.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@precio_venta"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.precioVenta.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@punto_maximo"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.puntoMaximoDeReposicion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@tipo_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.tipoDeProducto.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@punto_minimo"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.puntoMinimoDeReposicion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@imagen"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.urlImagen
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@alto"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.altoPaquete
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@ancho"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.anchoPaquete
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@largo"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.largoPaquete
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@peso"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.peso.ToString("R").Replace(",", ".")
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If esExitoso Then
                query = ConstantesDeDatos.ELIMINAR_CATALOGOS_DE_PRODUCTO
                parametros = New List(Of DbDto)
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@producto"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.nroDeProducto
                parametros.Add(param)

                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                If oObject.catalogos.Count > 0 Then
                    query = ConstantesDeDatos.GUARDAR_CATALOGO_PRODUCTO_XREF
                    For Each ct As Catalogo In oObject.catalogos
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.tipoDeDato = SqlDbType.BigInt
                        param.parametro = "@catalogo"
                        param.valor = ct.id
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@producto"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oObject.nroDeProducto
                        parametros.Add(param)

                        ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                    Next
                End If

                query = ConstantesDeDatos.ELIMINAR_DESCUENTOS_DE_PRODUCTO
                parametros = New List(Of DbDto)
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@producto"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.nroDeProducto
                parametros.Add(param)
                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                If Not oObject.descuentos Is Nothing AndAlso oObject.descuentos.Any() Then
                    query = ConstantesDeDatos.GUARDAR_DESCUENTO_PRODUCTO_XREF
                    For Each ct As Descuento In oObject.descuentos
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.tipoDeDato = SqlDbType.BigInt
                        param.parametro = "@descuento_id"
                        param.valor = ct.id
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@producto_id"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oObject.nroDeProducto
                        parametros.Add(param)

                        ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                    Next
                End If
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Producto) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_PRODUCTO
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroDeProducto
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As Producto) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroDeProducto
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@garantia"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.garantia.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@cantidad"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.cantidad
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@caracteristicas"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = -1

            param.valor = oObject.caracteristicas
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@ciclo"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.ciclo
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@costo_posesion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.costoDePosesion.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@costo_estandar"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.costoEstandar.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@costo_financiero"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.costoFinanciero.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@descripcion"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.descripcion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@estado"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.estado.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@familia"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.familiaDeProducto.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@metodo_reposicion"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.metodoDeReposicion.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@metodo_valoracion"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.metodoDeValoracion.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nombre"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.nombre
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@porcentaje_ganancia"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.porcentajeDeGanancia.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@precio_venta"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.precioVenta.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@punto_maximo"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.puntoMaximoDeReposicion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@tipo_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.tipoDeProducto.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@punto_minimo"
            param.tipoDeDato = SqlDbType.Int
            param.valor = oObject.puntoMinimoDeReposicion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@imagen"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.urlImagen
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@alto"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.altoPaquete
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@ancho"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.anchoPaquete
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@largo"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.largoPaquete
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@peso"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.peso.ToString("R").Replace(",", ".")
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If esExitoso Then
                query = ConstantesDeDatos.ELIMINAR_CATALOGOS_DE_PRODUCTO
                parametros = New List(Of DbDto)
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@producto"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.nroDeProducto
                parametros.Add(param)

                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                If oObject.catalogos.Count > 0 Then
                    query = ConstantesDeDatos.GUARDAR_CATALOGO_PRODUCTO_XREF
                    For Each ct As Catalogo In oObject.catalogos
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.tipoDeDato = SqlDbType.BigInt
                        param.parametro = "@catalogo"
                        param.valor = ct.id
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@producto"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oObject.nroDeProducto
                        parametros.Add(param)

                        ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                    Next
                End If

                query = ConstantesDeDatos.ELIMINAR_DESCUENTOS_DE_PRODUCTO
                parametros = New List(Of DbDto)
                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@producto"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.nroDeProducto
                parametros.Add(param)
                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                If Not oObject.descuentos Is Nothing AndAlso oObject.descuentos.Any() Then
                    query = ConstantesDeDatos.GUARDAR_DESCUENTO_PRODUCTO_XREF
                    For Each ct As Descuento In oObject.descuentos
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.tipoDeDato = SqlDbType.BigInt
                        param.parametro = "@descuento_id"
                        param.valor = ct.id
                        parametros.Add(param)

                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.parametro = "@producto_id"
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oObject.nroDeProducto
                        parametros.Add(param)

                        ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Producto)
        Dim criterio As CriterioDeBusquedaDeProducto = oObject
        Dim resultado As New List(Of Producto)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim dset As DataSet
        Dim dtable As DataTable
        Dim prod As Producto
        Try
            If Not criterio Is Nothing Then
                If criterio.idCatalogo <> 0 Then
                    query = ConstantesDeDatos.OBTENER_PRODUCTOS_DE_CATALOGO
                    Dim param As New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@catalogo_id"
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = criterio.idCatalogo
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@estado"
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = criterio.idEstadoProducto
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@precio"
                    param.tipoDeDato = SqlDbType.Int
                    param.valor = criterio.precio
                    parametros.Add(param)

                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@con_descuento"
                    param.tipoDeDato = SqlDbType.Bit
                    param.valor = criterio.conDescuento
                    parametros.Add(param)

                    dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    dtable = dset.Tables(0)
                    For Each row As DataRow In dtable.AsEnumerable
                        prod = New Producto
                        prod.nroDeProducto = row.Item("nro_de_producto")
                        prod.nombre = row.Item("nombre_producto")
                        prod.descripcion = row.Item("descripcion_producto")
                        prod.precioVenta = row.Item("precio_venta")
                        prod.urlImagen = row.Item("url_imagen")
                        Dim valoracionCriteria As New CriterioDeBusqueda
                        valoracionCriteria.criterioEntero = prod.nroDeProducto
                        prod.valoracion = ValoracionDeProductoDao.instancia().obtenerUno(valoracionCriteria)
                        resultado.Add(prod)
                    Next

                    query = ConstantesDeDatos.OBTENER_DESCUENTOS_ACTIVOS_DE_PRODUCTO

                    For Each oProd As Producto In resultado
                        parametros = New List(Of DbDto)
                        param = New DbDto
                        param.esParametroDeSalida = False
                        param.tamanio = 18
                        param.tipoDeDato = SqlDbType.BigInt
                        param.valor = oProd.nroDeProducto
                        param.parametro = "@nro_de_producto"
                        parametros = New List(Of DbDto)
                        parametros.Add(param)

                        dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                        dtable = dset.Tables(0)
                        Dim des As Descuento
                        oProd.descuentos = New List(Of Descuento)
                        For Each row As DataRow In dtable.AsEnumerable
                            des = New Descuento
                            des.id = row.Item("descuento_id")
                            des.descripcion = row.Item("descripcion_descuento")
                            des.monto = row.Item("monto")
                            des.porcentaje = row.Item("porcentaje")
                            oProd.descuentos.Add(des)
                        Next
                    Next
                End If
            Else
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_PRODUCTOS
                dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    prod = New Producto
                    prod.nroDeProducto = row.Item("nro_de_producto")
                    prod.nombre = row.Item("nombre_producto")
                    prod.descripcion = row.Item("descripcion_producto")
                    prod.precioVenta = row.Item("precio_venta")
                    prod.urlImagen = row.Item("url_imagen")
                    resultado.Add(prod)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_PRODUCTO)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Producto
        Dim query As String = ConstantesDeDatos.OBTENER_PRODUCTO
        Dim parametros As New List(Of DbDto)
        Dim resultado As Producto = Nothing
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_producto"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nombre_producto"
            param.tipoDeDato = SqlDbType.VarChar
            If oObject.criterioString Is Nothing Then
                param.valor = ""
            Else
                param.valor = oObject.criterioString
            End If
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)

            For Each fila As DataRow In dtable.AsEnumerable
                resultado = New Producto
                resultado.cantidad = fila.Item("cantidad")
                resultado.ciclo = fila.Item("ciclo")
                resultado.costoDePosesion = fila.Item("coste_posesion")
                resultado.costoEstandar = fila.Item("costo_estandar")
                resultado.costoFinanciero = fila.Item("coste_financiero")
                resultado.descripcion = fila.Item("descripcion_producto")
                resultado.estado = New Estado
                resultado.estado.id = fila.Item("estado_producto_id")
                resultado.familiaDeProducto = New FamiliaDeProducto
                resultado.familiaDeProducto.id = fila.Item("familia_de_producto_id")
                resultado.garantia = New TipoDeGarantia
                resultado.garantia.id = fila.Item("nro_de_garantia")
                resultado.garantia.dias = fila.Item("vigencia_en_dias")
                resultado.metodoDeReposicion = New MetodoDeReposicion
                resultado.metodoDeReposicion.id = fila.Item("metodo_de_reposicion_id")
                resultado.metodoDeValoracion = New MetodoValoracion
                resultado.metodoDeValoracion.id = fila.Item("metodo_valoracion_id")
                resultado.nombre = fila.Item("nombre_producto")
                resultado.nroDeProducto = fila.Item("nro_de_producto")
                resultado.porcentajeDeGanancia = fila.Item("porcentaje_de_ganancia")
                resultado.precioVenta = fila.Item("precio_venta")
                resultado.puntoMaximoDeReposicion = fila.Item("punto_de_reposicion_maximo")
                resultado.puntoMinimoDeReposicion = fila.Item("punto_de_reposicion_minimo")
                resultado.tipoDeProducto = New TipoDeProducto
                resultado.tipoDeProducto.id = fila.Item("tipo_de_producto_id")
                resultado.urlImagen = fila.Item("url_imagen")
                resultado.altoPaquete = fila.Item("alto")
                resultado.anchoPaquete = fila.Item("ancho")
                resultado.largoPaquete = fila.Item("largo")
                resultado.peso = fila.Item("peso")
                resultado.caracteristicas = fila.Item("caracteristicas")
                Exit For
            Next


            If Not resultado Is Nothing Then
                parametros = New List(Of DbDto)
                param = New DbDto
                param.esParametroDeSalida = False
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = resultado.nroDeProducto
                param.parametro = "@nro_de_producto"
                parametros = New List(Of DbDto)
                parametros.Add(param)

                query = ConstantesDeDatos.OBTENER_CATALOGOS_ASOCIADOS_A_PRODUCTO
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                Dim oCatalogo As Catalogo
                resultado.catalogos = New List(Of Catalogo)
                For Each row As DataRow In dtable.AsEnumerable
                    oCatalogo = New Catalogo
                    oCatalogo.id = row.Item("catalogo_id")
                    resultado.catalogos.Add(oCatalogo)
                Next

                query = ConstantesDeDatos.OBTENER_DESCUENTOS_ACTIVOS_DE_PRODUCTO

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                Dim des As Descuento
                resultado.descuentos = New List(Of Descuento)
                For Each row As DataRow In dtable.AsEnumerable
                    des = New Descuento
                    des.id = row.Item("descuento_id")
                    des.descripcion = row.Item("descripcion_descuento")
                    des.monto = row.Item("monto")
                    des.porcentaje = row.Item("porcentaje")
                    resultado.descuentos.Add(des)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
