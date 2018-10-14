Imports Modelo
Imports Datos
Imports Seguridad

Public Class GestorStock
    Private Sub New()

    End Sub

    Private Shared objeto As New GestorStock

    Public Shared Function instancia() As GestorStock
        Return objeto
    End Function

    Public Function calcularPrecioVenta(oProdEsp As ProductoEspecificoEnStock) As Double
        Return oProdEsp.producto.precioVenta
    End Function

    Public Function obtenerProductosEspecificosPorUsuario(idUsuario As Long) As List(Of ProductoEspecificoEnStock)
        Dim crit As New CriterioDeBusquedaProductoEspecifico
        crit.idUsuario = idUsuario
        Return ProductoEspecificoEnStockDao.instancia().obtenerMuchos(crit)
    End Function

    Public Function consultarStock(nroDeProducto As Integer, nroDeSucursal As Integer) As DisponibilidadEnStock
        Dim oDisponibilidad As New DisponibilidadEnStock
        oDisponibilidad.producto = New Producto
        oDisponibilidad.producto.nroDeProducto = nroDeProducto
        oDisponibilidad.sucursal = New Sucursal
        oDisponibilidad.sucursal.nroSucursal = nroDeSucursal
        Return DisponibilidadEnStockDao.instancia().consultarStock(oDisponibilidad)
    End Function

    Public Function buscarInstanciasEspecificasParaEgreso(nroSucursal As Integer, nroProducto As Integer, cantidad As Integer) As List(Of ProductoEspecificoEnStock)
        Dim oDisponibilidad As DisponibilidadEnStock = consultarStock(nroProducto, nroSucursal)
        Dim result As List(Of ProductoEspecificoEnStock) = Nothing
        If oDisponibilidad.cantidad >= cantidad Then
            Dim criterio As New CriterioDeBusquedaProductoEspecifico
            Dim criterioProd As New CriterioDeBusqueda
            criterioProd.criterioEntero = nroProducto
            criterioProd.criterioString = ""
            Dim oProd As Producto = ProductoDao.instancia().obtenerUno(criterioProd)
            If oProd.metodoDeReposicion.id = ConstantesDeMetodoDeValoracion.UEPS Then
                criterio.orden = CriterioDeBusquedaProductoEspecifico.DESCENDENTE
            Else
                criterio.orden = CriterioDeBusquedaProductoEspecifico.ASCENDENTE
            End If
            criterio.cantidad = cantidad
            criterio.nroProducto = nroProducto
            criterio.nroSucursal = nroSucursal
            result = ProductoEspecificoEnStockDao.instancia().obtenerMuchos(criterio)
        End If
        Return result
    End Function

    Public Function eliminarIngresoDeStock(oIngreso As IngresoDeStock) As String
        Dim dao As AbstractDao(Of IngresoDeStock) = IngresoDeStockDao.instancia()
        Dim resul As Boolean = dao.eliminar(oIngreso)
        If resul Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If

    End Function

    Public Function obtenerProductoEspecificoEnStock(nroDeSerie As Integer) As ProductoEspecificoEnStock
        Dim criterio As New CriterioDeBusquedaProductoEspecifico
        criterio.criterioEntero = nroDeSerie
        Dim produEspecifico As ProductoEspecificoEnStock = ProductoEspecificoEnStockDao.instancia().obtenerUno(criterio)
        Dim critProducto As New CriterioDeBusquedaDeProducto
        critProducto.criterioEntero = produEspecifico.producto.nroDeProducto
        critProducto.criterioString = ""
        produEspecifico.producto = ProductoDao.instancia().obtenerUno(critProducto)
        Return produEspecifico
    End Function
    Public Function eliminarEgresoDeStock(oEgreso As EgresoDeStock) As String
        Dim result As Boolean = EgresoDeStockDao.instancia().eliminar(oEgreso)
        If result Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function

    Public Function eliminarSolicitudDeStock(oSolicitud As SolicitudDeStock) As String
        Dim result As Boolean = SolicitudDeStockDao.instancia().eliminar(oSolicitud)
        If result Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function

    Public Function guardarSolicitudDeStock(oSolicitud As SolicitudDeStock) As String
        Dim dao As AbstractDao(Of SolicitudDeStock) = SolicitudDeStockDao.instancia()
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = oSolicitud.nroPedido
        Dim solic As SolicitudDeStock = dao.obtenerUno(criterio)
        Dim result As Boolean = False
        If solic Is Nothing Then
            result = dao.crear(oSolicitud)
        Else
            result = dao.modificar(oSolicitud)
        End If
        If result Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function


    Public Function obtenerTodosLosEstadosDeEgreso() As List(Of Estado)
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioString = ConstantesDeEstado.EGRESO_DE_STOCK
        Return EstadoDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function buscarEgresoDeStock(nroEgreso As Integer) As EgresoDeStock
        Dim dao As AbstractDao(Of EgresoDeStock) = EgresoDeStockDao.instancia()
        Dim crit As New CriterioDeBusqueda
        crit.criterioEntero = nroEgreso
        Return dao.obtenerUno(crit)
    End Function

    Public Function buscarSolicitudDeStock(nroSolicitud As Integer) As SolicitudDeStock
        Dim criteria As New CriterioDeBusqueda
        criteria.criterioEntero = nroSolicitud
        Return SolicitudDeStockDao.instancia().obtenerUno(criteria)
    End Function


    Public Function obtenerSolicitudesDeStockActivas(nroSucursal As Integer, nroProducto As Integer) As List(Of SolicitudDeStock)
        Dim criterio As New CriterioDeBusquedaSolicitud
        criterio.criterioBoolean = False 'Que no estén en estado ingresado 
        criterio.nroProducto = nroProducto
        criterio.nroSucursal = nroSucursal
        Return SolicitudDeStockDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function guardarEgresoDeStock(oEgreso As EgresoDeStock) As String
        Dim crit As New CriterioDeBusqueda
        crit.criterioEntero = oEgreso.nroEgreso
        Dim oEgr As EgresoDeStock = EgresoDeStockDao.instancia().obtenerUno(crit)
        Dim resultado As Boolean
        If oEgr Is Nothing Then
            resultado = EgresoDeStockDao.instancia().crear(oEgreso)
        Else
            resultado = EgresoDeStockDao.instancia().modificar(oEgreso)
        End If
        If resultado Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function guardarIngresoDeStock(oIngreso As IngresoDeStock) As String
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = oIngreso.nroIngreso
        Dim dao As AbstractDao(Of IngresoDeStock) = IngresoDeStockDao.instancia()
        Dim oIng As IngresoDeStock = dao.obtenerUno(criterio)
        Dim result As Boolean = False
        If oIng Is Nothing Then
            result = dao.crear(oIngreso)
            If result Then
                Dim crit As New CriterioDeBusqueda
                crit.criterioEntero = oIngreso.producto.nroDeProducto
                crit.criterioString = ""
                Dim oProd As Producto = ProductoDao.instancia().obtenerUno(crit)
                Dim oProdEsp As ProductoEspecificoEnStock
                For i As Integer = 0 To oIngreso.cantidad - 1
                    oProdEsp = New ProductoEspecificoEnStock
                    oProdEsp.precioCompra = oIngreso.precioDeCompra
                    oProdEsp.producto = oProd
                    oProdEsp.ingreso = oIngreso
                    oProdEsp.motivoModificacion = ConstantesDeMotivosDeModificacionDeStock.INGRESO.ToString
                    oProdEsp.sucursal = oIngreso.sucursal
                    oProdEsp.estado = New Estado
                    oProdEsp.estado.id = 1
                    oProdEsp.fechaModificacion = oIngreso.fecha
                    oProdEsp.precioVenta = calcularPrecioVenta(oProdEsp)
                    ProductoEspecificoEnStockDao.instancia().crear(oProdEsp)
                Next
                Dim disp As New DisponibilidadEnStock
                disp.cantidad = oIngreso.cantidad
                disp.producto = oIngreso.producto
                disp.sucursal = oIngreso.sucursal
                DisponibilidadEnStockDao.instancia().agregarStock(disp)
            End If
        Else
            result = dao.modificar(oIngreso)
        End If
        If result Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function


    Public Function buscarIngresoDeStock(id As Integer) As IngresoDeStock
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = id
        Return IngresoDeStockDao.instancia().obtenerUno(criterio)
    End Function
End Class
