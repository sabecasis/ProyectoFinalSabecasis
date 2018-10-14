
Imports Modelo
Imports System.Globalization
Imports Seguridad
Imports Datos
Imports System.IO

Public Class GestorABM

    Private Shared objeto As New GestorABM

    Private Sub New()

    End Sub

    Public Shared Function instancia() As GestorABM
        Return objeto
    End Function

    Public Function obtenerPreguntaDeEncuestaPorId(id As Integer, fechaDesde As String, fechaHasta As String) As PreguntaDeEncuesta
        Dim criterio As New CriterioDeBusquedaEncuesta
        criterio.criterioEntero = id
        criterio.fechaDesde = fechaDesde
        criterio.fechaHasta = fechaHasta
        Return PreguntaDeEncuestaDao.instancia().obtenerUno(criterio)
    End Function

    Public Function guardarDescuento(oDescuento As Descuento) As Boolean
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = oDescuento.id
        criterio.criterioString = oDescuento.nombre
        Dim oDesc As Descuento = DescuentoDao.instancia().obtenerUno(criterio)
        Dim resultado As Boolean = False
        If oDesc Is Nothing Then
            resultado = DescuentoDao.instancia().crear(oDescuento)
        Else
            resultado = DescuentoDao.instancia().modificar(oDescuento)
        End If
        Return resultado
    End Function

    Public Function buscarDescuento(id, nombre) As Descuento
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = id
        criterio.criterioString = nombre
        Return DescuentoDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerProvinciaPorId(id As Integer) As Provincia
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = id
        Return ProvinciaDao.instancia().obtenerUno(criterio)
    End Function

    Public Function crearNotaDeCredito(oNota As NotaDeCredito) As Boolean
        Dim criterioFac As New CriterioDeBusqueda
        criterioFac.criterioEntero = oNota.factura.nroFactura
        criterioFac.criterioString = "porid"
        Dim oFactura As Factura = FacturaDao.instancia().obtenerUno(criterioFac)
        oNota.factura = oFactura
        Dim result As Boolean = NotaDeCreditoDao.instancia().crear(oNota)
        oNota.comprobante = GestorComprobantes.instancia().crearComprobanteDeNotaDeCredito(oNota)
        NotaDeCreditoDao.instancia().modificar(oNota)
        GestorEmail.instancia().enviarEmailNotaDeCredito(oNota)
        If oNota.devolucionDeProducto.nroDeDevolucion <> 0 Then
            oNota.devolucionDeProducto.notaDeCredito = oNota
            DevolucionDeProductoDao.instancia().modificar(oNota.devolucionDeProducto)
        End If
        Return result
    End Function

    Public Function obtenerDevolucionPorNroNotaDeCredito(nroNota As Integer) As DevolucionDeProducto
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioBoolean = True
        criterio.criterioEntero = nroNota
        Return DevolucionDeProductoDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerTodasLasNotasDeCredito() As List(Of NotaDeCredito)
        Return NotaDeCreditoDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerTodasLasDevolucionesActivas() As List(Of DevolucionDeProducto)
        Return DevolucionDeProductoDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function buscarDevolucionDeProducto(nroDevolucion As Integer) As DevolucionDeProducto
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = nroDevolucion
        Return DevolucionDeProductoDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerUsuariosDeNewsletter(id As Integer) As List(Of Newsletter)
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioBoolean = True
        criterio.criterioEntero = id
        Return NewsletterDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function obtenerTodosLosUsuarios() As List(Of Usuario)
        Return UsuarioDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerProductosDeUsuario(id As Integer) As List(Of ProductoEspecificoEnStock)
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = id
        Return ProductoEspecificoEnStockDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function obtenerNovedadPorNombre(nombre As String) As Novedad
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioString = nombre
        Return NovedadDao.instancia().obtenerUno(criterio)
    End Function

    Public Function crearNovedad(oNovedad As Novedad) As String
        ValidacionHelper.instancia().validarCampoVacio(oNovedad.nombre, "nombre")
        ValidacionHelper.instancia().validarCampoVacio(oNovedad.titulo, "titulo")
        ValidacionHelper.instancia().validarCampoVacio(oNovedad.novedad, "contenido")
        ValidacionHelper.instancia().validarNoCero(oNovedad.newsletter.id, "newssletter")
        Dim add As String = "<Ad>    <ImageUrl>/static/add_img/novedad_??.png</ImageUrl>    <NavigateUrl>/Cliente/DetallesDeNovedad.aspx?id=??</NavigateUrl>    <AlternateText></AlternateText>    <Impressions>80</Impressions>    <Keyword>??</Keyword>  </Ad>"
        add = add.Replace("??", oNovedad.nombre)
        oNovedad.configuracionAddRotator = add
        Dim result As Boolean = NovedadDao.instancia().crear(oNovedad)
        If result Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function
    Public Function obtenerMetodoDePagoPorId(id As Integer)
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = id
        Return MetodoDePagoDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerTodasLasNovedades() As List(Of Novedad)
        Return NovedadDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerArticulos() As List(Of ArticuloSoporte)
        Return ArticuloSoprteDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function eliminarArticulo(id As Integer) As String
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Dim oArt As New ArticuloSoporte
        oArt.id = id
        Dim result As Boolean = ArticuloSoprteDao.instancia().eliminar(oArt)
        If result Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function

    Public Function guararArticulo(oArticulo As ArticuloSoporte) As String
        ValidacionHelper.instancia().validarCampoVacio(oArticulo.articulo, "articulo")
        ValidacionHelper.instancia().validarCampoVacio(oArticulo.contenido, "contenido")
        Dim result As Boolean = ArticuloSoprteDao.instancia().crear(oArticulo)
        If result Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function eliminarNovedad(novedadId As Integer) As String
        Dim oNovedad As New Novedad
        oNovedad.id = novedadId
        Dim result As Boolean = NovedadDao.instancia().eliminar(oNovedad)
        If result Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function


    Public Function obtenerTodasLasNovedadesPorUsuario(idUsuario As Integer) As List(Of Novedad)
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = idUsuario
        Return NovedadDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function eliminarSugerencia(sugerencia As SugerenciaDeProducto) As String
        Try
            Dim result As Boolean = SugerenciaDeProductoDao.instancia().eliminar(sugerencia)
            If result Then
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            End If
        Catch ex As ExcepcionDeDatos
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End Try

    End Function

    Public Function obtenerNewsletterDeUsuario(id As Integer) As List(Of Newsletter)
        Dim crit As New CriterioDeBusqueda
        crit.criterioEntero = id
        Return NewsletterDao.instancia().obtenerMuchos(crit)
    End Function


    Public Function guardarOfertasPersonales(ofertas As List(Of Integer), idUsuario As Integer) As String
        Dim oferta As OfertaPersonal
        Dim resultado As Boolean = True
        oferta = New OfertaPersonal
        oferta.usuario = New Usuario
        oferta.usuario.id = idUsuario
        OfertaPersonalDao.instancia().eliminar(oferta)
        For Each oOferta As Integer In ofertas
            oferta = New OfertaPersonal
            oferta.id = oOferta
            oferta.usuario = New Usuario
            oferta.usuario.id = idUsuario
            resultado = resultado And OfertaPersonalDao.instancia().crear(oferta)
        Next
        If resultado Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function crearSugerencia(sugerencia As SugerenciaDeProducto) As String
        Try
            Dim result As Boolean = SugerenciaDeProductoDao.instancia().crear(sugerencia)
            If result Then
                Return ConstantesDeMensaje.GUARDADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_DE_GUARDADO
            End If
        Catch es As ExcepcionDeDatos
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End Try

    End Function

    Public Function guardarNewsletterDeUsuario(id As Integer, news As String()) As String
        Dim oNews As Newsletter = New Newsletter
        oNews.usuario = New Usuario
        oNews.usuario.id = id
        NewsletterDao.instancia().eliminar(oNews)
        For i As Integer = 0 To news.Count - 1
            oNews = New Newsletter()
            oNews.usuario = New Usuario
            oNews.usuario.id = id
            oNews.id = Convert.ToInt32(news(i))
            NewsletterDao.instancia().crear(oNews)
        Next


        Return ConstantesDeMensaje.GUARDADO_CON_EXITO
    End Function

    Public Function obtenerTodosLosNewsletter() As List(Of Newsletter)
        Return NewsletterDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerTodosLosDescuentos() As List(Of Descuento)
        Return DescuentoDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function guardarMensajeConsulta(oMensaje As MensajeDeConsulta) As String
        Dim resultado As Boolean = MensajeDeConsultaDao.instancia().crear(oMensaje)
        If resultado Then
            GestorEmail.instancia().enviarEmailDeContacto(oMensaje)
            Return ConstantesDeMensaje.MENSAJE_ENVIADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ENVIAR_MENSAJE
        End If
    End Function

    Public Function guardarEncuesta(oEncuesta As Encuesta) As String
        Dim resultad As Boolean = False
        Dim crit As New CriterioDeBusquedaEncuesta
        crit.criterioEntero = oEncuesta.id
        crit.criterioString = oEncuesta.nombre
        crit.fechaDesde = ""
        crit.fechaHasta = ""
        Dim oEnc As Encuesta = EncuestaDao.instancia().obtenerUno(crit)
        If oEnc Is Nothing Then
            resultad = EncuestaDao.instancia().crear(oEncuesta)
        Else
            resultad = EncuestaDao.instancia().modificar(oEncuesta)
        End If
        If resultad Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function obtenerEncuesta(id As Integer, nombre As String) As Encuesta
        Dim criterio As New CriterioDeBusquedaEncuesta
        criterio.criterioEntero = id
        criterio.criterioString = nombre
        criterio.fechaDesde = ""
        criterio.fechaHasta = ""
        Return EncuestaDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerTodosLosTiposDeEncuesta() As List(Of TipoDeEncuesta)
        Return TipoDeEncuestaDao.instancia().obtenerMuchos(Nothing)
    End Function


    Public Function obtenerTodasLasSucursales() As List(Of Sucursal)
        Return SucursalDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerTodosLosProductos() As List(Of Producto)
        Return ProductoDao.instancia().obtenerMuchos(Nothing)
    End Function


    Public Function obtenerTodosLosCatalogos() As List(Of Catalogo)
        Return CatalogoDao.instancia().obtenerMuchos(Nothing)
    End Function




    Public Function obtenerTodosLosTiposDeEnvio() As List(Of TipoDeEnvio)
        Return TipoDeEnvioDao.instancia().obtenerMuchos(Nothing)
    End Function


    Public Function buscarSucursal(oSucursal As Sucursal) As Sucursal
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioString = oSucursal.nombre
        criterio.criterioEntero = oSucursal.nroSucursal
        Return SucursalDao.instancia().obtenerUno(criterio)
    End Function

    Public Function eliminarSucursal(oSucursal As Sucursal) As String
        Dim esExitoso As Boolean = SucursalDao.instancia().eliminar(oSucursal)
        If esExitoso Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function
    Public Function buscarProducto(producto As Producto) As Producto
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = producto.nroDeProducto
        criterio.criterioString = producto.nombre
        Dim oProd As Producto = ProductoDao.instancia().obtenerUno(criterio)
        If Not oProd Is Nothing Then
            Dim oDisp As New DisponibilidadEnStock
            oDisp.producto = oProd
            oDisp.sucursal = New Sucursal
            oDisp.sucursal.nroSucursal = 1
            oDisp = DisponibilidadEnStockDao.instancia().consultarStock(oDisp)
            oProd.cantidad = oDisp.cantidad
        End If

        Return oProd
    End Function

    Public Function eliminarProducto(id As Integer) As String
        Dim dao As AbstractDao(Of Producto) = ProductoDao.instancia()
        Dim producto As New Producto
        producto.nroDeProducto = id
        Dim esExitoso As Boolean = dao.eliminar(producto)
        If esExitoso Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function

    Public Function guardarProducto(producto As Producto) As String
        ValidacionHelper.instancia().validarNoCero(producto.nroDeProducto, "dias")
        Dim dao As AbstractDao(Of Producto) = ProductoDao.instancia()
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = producto.nroDeProducto
        criterio.criterioString = producto.nombre
        Dim oProd As Producto = dao.obtenerUno(criterio)
        Dim esExitoso As Boolean = False
        If oProd Is Nothing Then
            esExitoso = dao.crear(producto)
        Else
            If String.IsNullOrEmpty(producto.urlImagen) Then
                producto.urlImagen = oProd.urlImagen
            End If
            esExitoso = dao.modificar(producto)
        End If
        If esExitoso Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function guardarFamiliaDeProductos(familia As FamiliaDeProducto) As String
        ValidacionHelper.instancia().validarNoCero(familia.familia, "familia")
        Dim dao As AbstractDao(Of FamiliaDeProducto) = FamiliaDeProductoDao.instancia()
        Dim esExitoso As Boolean = dao.crear(familia)
        If esExitoso Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function eliminarFamiliaDeProducto(id As Integer) As String
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Dim dao As AbstractDao(Of FamiliaDeProducto) = FamiliaDeProductoDao.instancia()
        Dim familia As New FamiliaDeProducto()
        familia.id = id
        Dim esExitoso As Boolean = dao.eliminar(familia)
        If esExitoso Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function

    Public Function buscarFamiliaDeProducto(familia As FamiliaDeProducto) As FamiliaDeProducto
        Dim dao As AbstractDao(Of FamiliaDeProducto) = FamiliaDeProductoDao.instancia()
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioObjeto = familia
        Return dao.obtenerUno(criterio)
    End Function

    Public Function eliminarTipoDeGarantia(id As Integer) As String
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Try
            Dim dao As AbstractDao(Of TipoDeGarantia) = TipoDeGarantiaDao.instancia()
            Dim tipo As New TipoDeGarantia
            tipo.id = id
            Dim esExitoso As Boolean = dao.eliminar(tipo)
            If esExitoso Then
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            End If
        Catch ex As Exception
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR & " " & ex.Message
        End Try

    End Function

    Public Function buscarTipoDeGarantia(tipo As TipoDeGarantia) As TipoDeGarantia
        Dim dao As AbstractDao(Of TipoDeGarantia) = TipoDeGarantiaDao.instancia()
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioObjeto = tipo
        Return dao.obtenerUno(criterio)
    End Function


    Public Function guardarTipoDeGarantia(tipo As TipoDeGarantia) As String
        ValidacionHelper.instancia().validarNoCero(tipo.dias, "dias")
        ValidacionHelper.instancia().validarCampoVacio(tipo.descripcion, "descripcion")
        Try
            Dim dao As AbstractDao(Of TipoDeGarantia) = TipoDeGarantiaDao.instancia()
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioObjeto = tipo
            Dim tipoDeGarantia = dao.obtenerUno(criterio)
            Dim esExitoso As Boolean = False
            If Not tipoDeGarantia Is Nothing Then
                esExitoso = dao.modificar(tipo)
            Else
                esExitoso = dao.crear(tipo)
            End If
            If esExitoso Then
                Return ConstantesDeMensaje.GUARDADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_DE_GUARDADO
            End If
        Catch ex As Exception
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO & " " & ex.Message
        End Try
    End Function


    Public Function obtenerTodosLosEventos() As List(Of Evento)
        Try
            Return EventoDao.instancia().obtenerMuchos(Nothing)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerTodosLosElementos() As List(Of Elemento)
        Return ElementoDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerCantidadDeElementosTraducibles() As Integer
        Try
            Return ElementoDao.instancia().obtenerMuchos(Nothing).Count
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function eliminarUsuario(id As Integer) As String
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Try
            Dim oUsuario As New Usuario
            oUsuario.id = id
            Dim dao As AbstractDao(Of Usuario) = UsuarioDao.instancia()
            Dim esExitoso As Boolean = dao.eliminar(oUsuario)
            If esExitoso Then
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function buscarUsuario(nombre As String) As Usuario
        ValidacionHelper.instancia().validarCampoVacio(nombre, "nombre")
        Try
            Dim oUsuario As Usuario
            Dim critero As New CriterioDeBusqueda
            critero.criterioString = nombre
            Dim dao As AbstractDao(Of Usuario) = UsuarioDao.instancia()
            oUsuario = dao.obtenerUno(critero)
            Return oUsuario
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerElementosDeIdioma(idIdioma As Integer) As List(Of ElementoDeIdioma)
        Try
            Dim dao As AbstractDao(Of ElementoDeIdioma) = ElementoDeIdiomaDao.instancia()
            Dim criterio As New CriterioDeBusquedaElemento
            criterio.criterioEntero = idIdioma
            Return dao.obtenerMuchos(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerElementosPaginados(desde As Integer, hasta As Integer, esTraducible As Integer) As List(Of Elemento)
        Try
            Dim dao As AbstractDao(Of Elemento) = ElementoDao.instancia()
            Dim criterio As New CriterioDeBusquedaElemento
            criterio.flagTraducible = esTraducible
            criterio.desde = desde
            criterio.hasta = hasta
            Dim elementos As List(Of Elemento) = dao.obtenerMuchos(criterio)
            Return elementos
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function guardarUsuario(oUsuario As Usuario) As String
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.nombre, "usuario")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.apellido, "apellido")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.nombre, "nombre")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.tipoDeDocumento.id, "tipo de documento")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.contacto.calle, "calle")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.contacto.email, "email")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.contacto.numero, "numero")
        ValidacionHelper.instancia().validarCampoVacio(oUsuario.persona.contacto.telefonos(0).telefono, "telefono")
        Try
            Dim esExitoso As Boolean = False

            Dim dao As AbstractDao(Of Usuario) = UsuarioDao.instancia()
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioString = oUsuario.nombre
            Dim bUsuario = dao.obtenerUno(criterio)
            If Not bUsuario Is Nothing Then
                If bUsuario.id = oUsuario.id Then
                    If oUsuario.contadorMalaPassword > 3 AndAlso oUsuario.bloqueado = False Then
                        oUsuario.contadorMalaPassword = 0
                    End If
                    esExitoso = dao.modificar(oUsuario)
                Else
                    Return ConstantesDeMensaje.ERROR_USUARIO_EXISTENTE
                End If
            Else
                ValidacionHelper.instancia().validarCampoVacio(oUsuario.password, "password")
                If oUsuario.password.Length < 5 Or oUsuario.password.Length > 8 Then
                    Throw New ExcepcionDeValidacion(ConstantesDeMensaje.LARGO_PASSWORD)
                End If
                esExitoso = dao.crear(oUsuario)
            End If
            If esExitoso Then
                Return ConstantesDeMensaje.GUARDADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_DE_GUARDADO
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function



    Public Function buscarIdioma(id) As Idioma
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Try
            Dim dao As AbstractDao(Of Idioma) = IdiomaDao.instancia()
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioEntero = id
            Dim oIdioma As Idioma
            oIdioma = dao.obtenerUno(criterio)
            Return oIdioma
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function eliminarIdioma(id As Integer) As String
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Try
            Dim dao As AbstractDao(Of Idioma) = IdiomaDao.instancia()
            Dim esExitoso As Boolean = False
            Dim idioma As New Idioma
            idioma.id = id
            esExitoso = dao.eliminar(idioma)
            If esExitoso Then
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function guardarIdioma(idioma As Idioma) As String
        ValidacionHelper.instancia().validarCampoVacio(idioma.nombre, "nombre")
        ValidacionHelper.instancia().validarNoCero(idioma.id, "id")
        Try
            Dim dao As AbstractDao(Of Idioma) = IdiomaDao.instancia()
            Dim criterio As New CriterioDeBusqueda
            Dim esExitoso As Boolean = False
            criterio.criterioEntero = idioma.id
            Dim oIdioma As Idioma = dao.obtenerUno(criterio)
            If oIdioma Is Nothing Then
                esExitoso = dao.crear(idioma)
            Else
                esExitoso = dao.modificar(idioma)
            End If
            If esExitoso Then
                Return ConstantesDeMensaje.GUARDADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_DE_GUARDADO
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function eliminarPermiso(id As Integer) As String
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Try
            Dim dao As AbstractDao(Of Permiso) = PermisoDao.instancia()
            Dim oPermiso As New Permiso
            oPermiso.id = id
            Dim valor As Boolean = dao.eliminar(oPermiso)
            If valor = False Then
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            Else
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO + " id: " + id.ToString()
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerTodosLosTiposDeGarantia() As List(Of TipoDeGarantia)
        Dim dao As AbstractDao(Of TipoDeGarantia) = TipoDeGarantiaDao.instancia()
        Return dao.obtenerMuchos(Nothing)
    End Function
    Public Function obtenerTodasLasFamiliasDeProducto() As List(Of FamiliaDeProducto)
        Dim dao As AbstractDao(Of FamiliaDeProducto) = FamiliaDeProductoDao.instancia()
        Return dao.obtenerMuchos(Nothing)
    End Function

    Public Function obtenerTodosLosTiposDeProducto() As List(Of TipoDeProducto)
        Dim dao As AbstractDao(Of TipoDeProducto) = TipoDeProductoDao.instancia()
        Return dao.obtenerMuchos(Nothing)
    End Function

    Public Function obtenerMetodosDeReposicion() As List(Of MetodoDeReposicion)
        Dim dao As AbstractDao(Of MetodoDeReposicion) = MetodoDeReposicionDao.instancia()
        Return dao.obtenerMuchos(Nothing)
    End Function

    Public Function obtenerMetodosDeValoracion() As List(Of MetodoValoracion)
        Dim dao As AbstractDao(Of MetodoValoracion) = MetodoValoracionDao.instancia()
        Return dao.obtenerMuchos(Nothing)
    End Function


    Public Function guardarSucursal(sucursal As Sucursal) As String
        ValidacionHelper.instancia().validarCampoVacio(sucursal.nombre, "nombre")
        ValidacionHelper.instancia().validarNoCero(sucursal.nroSucursal, "numero sucursal")
        Dim esExitoso As Boolean = SucursalDao.instancia().crear(sucursal)
        If esExitoso Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function obtenerEstadosPorTipo(tipo As String) As List(Of Estado)
        ValidacionHelper.instancia().validarCampoVacio(tipo, "tipo")
        Dim dao As AbstractDao(Of Estado) = EstadoDao.instancia()
        Dim criteria As New CriterioDeBusqueda
        criteria.criterioString = tipo
        Return dao.obtenerMuchos(criteria)
    End Function

    Public Function obtenerTodosLosTiposDeDocumento() As List(Of TipoDeDocumento)
        Try
            Return TipoDeDocumentoDao.instancia().obtenerMuchos(Nothing)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerTodosLosTiposDeTelefono() As List(Of TipoDeTelefono)
        Try
            Return TipoDeTelefonoDao.instancia().obtenerMuchos(Nothing)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerMuchosPaises(oObject As Pais) As List(Of Pais)
        Try
            Return PaisDao.instancia().obtenerMuchos(Nothing)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function


    Public Function obtenerLocalidadPorId(id As Integer) As Localidad
        ValidacionHelper.instancia().validarNoCero(id, "id")
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = id
        Return LocalidadDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerLocalidadPorCp(cp As String) As Localidad
        ValidacionHelper.instancia().validarCampoVacio(cp, "cp")

        Dim criterio As New CriterioDeBusqueda
        criterio.criterioString = cp
        Return LocalidadDao.instancia().obtenerUno(criterio)
    End Function



    Public Function obtenerMuchasLocalidades(id As Integer) As List(Of Localidad)
        Try
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioEntero = id
            Return LocalidadDao.instancia().obtenerMuchos(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerMuchasProvinicas(id As Integer) As List(Of Provincia)
        Try
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioEntero = id
            Return ProvinciaDao.instancia().obtenerMuchos(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function buscarPermiso(nombre As String) As Permiso
        ValidacionHelper.instancia().validarCampoVacio(nombre, "nombre")

        Dim oPermiso As Permiso = Nothing
        Try
            Dim dao As AbstractDao(Of Permiso) = PermisoDao.instancia()
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioString = nombre
            oPermiso = dao.obtenerUno(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        Return oPermiso
    End Function

    Public Function obtenerTodosLosIdiomasCreados() As List(Of Idioma)
        Try
            Dim dao As IdiomaDao = IdiomaDao.instancia()
            Return dao.obtenerMuchos(Nothing)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerTodosLosIdiomas() As List(Of Idioma)
        Try
            Dim oIdioma As Idioma
            Dim listaIdiomas As New List(Of Idioma)
            For i As Integer = 0 To CultureInfo.GetCultures(CultureTypes.AllCultures).Count - 1
                oIdioma = New Idioma
                oIdioma.id = CultureInfo.GetCultures(CultureTypes.AllCultures)(i).LCID
                oIdioma.nombre = CultureInfo.GetCultures(CultureTypes.AllCultures)(i).DisplayName
                listaIdiomas.Add(oIdioma)
            Next

            Return listaIdiomas
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function guardarPermiso(oPermiso As Permiso) As String
        ValidacionHelper.instancia().validarCampoVacio(oPermiso.nombre, "nombre")
        ValidacionHelper.instancia().validarNoCero(oPermiso.id, "id")
        ValidacionHelper.instancia().validarNoCero(oPermiso.elemento.id, "elemento")
        Dim esExitoso As Boolean = False
        Try
            Dim dao As AbstractDao(Of Permiso) = PermisoDao.instancia()
            Dim criterio As CriterioDeBusqueda = Nothing
            If Not oPermiso Is Nothing Then
                criterio = New CriterioDeBusqueda
                criterio.criterioString = oPermiso.nombre
            End If
            Dim elementoExistente As Permiso = dao.obtenerUno(criterio)
            If Not elementoExistente Is Nothing Then
                esExitoso = dao.modificar(oPermiso)
            Else
                esExitoso = dao.crear(oPermiso)
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        If esExitoso Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function guardarElemento(oElemento As Elemento) As String
        ValidacionHelper.instancia().validarCampoVacio(oElemento.nombre, "nombre")
        ValidacionHelper.instancia().validarNoCero(oElemento.id, "id")
        Dim esExitoso As Boolean = False
        Try
            Dim dao As AbstractDao(Of Elemento) = ElementoDao.instancia()
            Dim criterio As CriterioDeBusqueda = Nothing
            If Not oElemento Is Nothing Then
                criterio = New CriterioDeBusqueda
                criterio.criterioEntero = oElemento.id
            End If
            Dim elementoExistente As Elemento = dao.obtenerUno(criterio)
            If Not elementoExistente Is Nothing Then
                esExitoso = dao.modificar(oElemento)
            Else
                esExitoso = dao.crear(oElemento)
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        If esExitoso Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function eliminarRol(id As Integer) As String
        Try
            Dim oRol As New Rol()
            oRol.id = id
            Dim esExitoso As Boolean = RolDao.instancia().eliminar(oRol)
            If esExitoso Then
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
            Else
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function buscarRol(nombre As String) As Rol
        Dim oRol As Rol = Nothing
        Try
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioString = nombre
            oRol = RolDao.instancia().obtenerUno(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        Return oRol
    End Function

    Public Function guardarRol(oElemento As Rol) As String
        Dim esExitoso As Boolean = False
        Try
            Dim dao As AbstractDao(Of Rol) = RolDao.instancia()
            Dim criterio As CriterioDeBusqueda = Nothing
            If Not oElemento Is Nothing Then
                criterio = New CriterioDeBusqueda()
                criterio.criterioString = oElemento.nombre
            End If
            Dim elementoExistente As Rol = dao.obtenerUno(criterio)
            If Not elementoExistente Is Nothing Then
                esExitoso = dao.modificar(oElemento)
            Else
                esExitoso = dao.crear(oElemento)
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        If esExitoso Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function obtenerMuchosPermisos(oObject As Permiso) As List(Of Permiso)
        Try
            Dim dao As AbstractDao(Of Permiso) = PermisoDao.instancia()
            Dim criterio As CriterioDeBusqueda = Nothing
            If Not oObject Is Nothing Then
                criterio = New CriterioDeBusqueda()
                criterio.criterioString = oObject.nombre
            End If
            Dim permisos As List(Of Permiso) = dao.obtenerMuchos(criterio)
            Return permisos
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        Return Nothing
    End Function

    Public Function obtenerMuchosRoles(oRol As Rol) As List(Of Rol)
        Try
            Dim dao As AbstractDao(Of Rol) = RolDao.instancia()
            Dim criterio As CriterioDeBusqueda = Nothing
            If Not oRol Is Nothing Then
                criterio = New CriterioDeBusqueda()
                criterio.criterioEntero = oRol.id
                criterio.criterioString = oRol.nombre
            End If
            Return dao.obtenerMuchos(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
        Return Nothing
    End Function

    Public Function buscarElemento(nombreElemento As String) As Elemento
        Try
            Dim dao As AbstractDao(Of Elemento) = ElementoDao.instancia()
            Dim elemento As Elemento
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioString = nombreElemento
            elemento = dao.obtenerUno(criterio)
            If elemento Is Nothing Then
                'TODO tirar mensaje
            End If
            Return elemento
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function eliminarElemento(id As Integer) As String
        Try
            Dim dao As AbstractDao(Of Elemento) = ElementoDao.instancia()
            Dim oElemento As New Elemento
            oElemento.id = id
            Dim valor As Boolean = dao.eliminar(oElemento)
            If valor = False Then
                Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
            Else
                Return ConstantesDeMensaje.ELIMINADO_CON_EXITO + " id: " + id.ToString()
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

End Class
