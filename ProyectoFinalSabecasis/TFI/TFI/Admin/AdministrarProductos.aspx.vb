Imports System.Web.Services
Imports Modelo
Imports System.IO
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarProductos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LblMensaje.InnerText = ""
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Me.IsPostBack Then
                If Request.Form("accion").Equals(ConstantesDeEvento.GUARDAR) Then
                    guardar()
                    LblMensaje.InnerText = ConstantesDeMensaje.GUARDADO_CON_EXITO
                    LblMensaje.DataBind()
                ElseIf Request.Form("accion").Equals(ConstantesDeEvento.ELIMINAR) Then
                    eliminar()
                    LblMensaje.InnerText = ConstantesDeMensaje.ELIMINADO_CON_EXITO
                    LblMensaje.DataBind()
                ElseIf Request.Form("accion").Equals(ConstantesDeEvento.BUSCAR) Then
                    buscar(nombre.Value, id.Value)
                End If
            Else
                chCatalogos.DataSource = GestorABM.instancia().obtenerTodosLosCatalogos()
                chCatalogos.DataTextField = "catalogo"
                chCatalogos.DataValueField = "id"
                chCatalogos.DataBind()
                ltsProductos.DataSource = GestorABM.instancia().obtenerTodosLosProductos()
                ltsProductos.DataTextField = "nombre"
                ltsProductos.DataValueField = "nombre"
                ltsProductos.DataBind()
                garantia.DataSource = GestorABM.instancia().obtenerTodosLosTiposDeGarantia()
                garantia.DataTextField = "descripcion"
                garantia.DataValueField = "id"
                garantia.DataBind()
                familia.DataSource = GestorABM.instancia().obtenerTodasLasFamiliasDeProducto()
                familia.DataValueField = "id"
                familia.DataTextField = "familia"
                familia.DataBind()
                metodorep.DataSource = GestorABM.instancia().obtenerMetodosDeReposicion()
                metodorep.DataTextField = "metodo"
                metodorep.DataValueField = "id"
                metodorep.DataBind()
                metodoval.DataSource = GestorABM.instancia().obtenerMetodosDeValoracion()
                metodoval.DataTextField = "metodo"
                metodoval.DataValueField = "id"
                metodoval.DataBind()
                estado.DataSource = GestorABM.instancia().obtenerEstadosPorTipo(ConstantesDeEstado.PRODUCTO)
                estado.DataValueField = "id"
                estado.DataTextField = "estado"
                estado.DataBind()
                tipoproducto.DataSource = GestorABM.instancia().obtenerTodosLosTiposDeProducto()
                tipoproducto.DataTextField = "tipo"
                tipoproducto.DataValueField = "id"
                tipoproducto.DataBind()
                chDescuentos.DataSource = GestorABM.instancia().obtenerTodosLosDescuentos()
                chDescuentos.DataTextField = "nombre"
                chDescuentos.DataValueField = "id"
                chDescuentos.DataBind()
            End If
        Catch ex As ExcepcionDeValidacion
            LblMensaje.InnerText = ex.Message
            LblMensaje.DataBind()
        Catch exes As ExcepcionDelSistema
            LblMensaje.InnerText = exes.mensaje
            LblMensaje.DataBind()
        Catch exe As Exception
            LblMensaje.InnerText = exe.Message
            LblMensaje.DataBind()
        End Try
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    Private Sub eliminar()
        GestorABM.instancia().eliminarProducto(id.Value)
    End Sub

    Private Sub guardar()
        ValidacionHelper.instancia().validarCampoVacio(descripcion.Value, "descripion")
        ValidacionHelper.instancia().validarNoCero(garantia.Value, "garantia")
        ValidacionHelper.instancia().validarNoCero(costoestandar.Value, "costo estandar")
        ValidacionHelper.instancia().validarNoCero(estado.Value, "estado")
        ValidacionHelper.instancia().validarNoCero(familia.Value, "familia")
        ValidacionHelper.instancia().validarNoCero(metodorep.Value, "metodo reposicion")
        ValidacionHelper.instancia().validarNoCero(metodoval.Value, "metodo valoracion")
        ValidacionHelper.instancia().validarCampoVacio(nombre.Value, "nombre")
        ValidacionHelper.instancia().validarNoCero(precioventa.Value, "precio venta")
        ValidacionHelper.instancia().validarNoCero(tipoproducto.Value, "tipo de producto")
        Dim producto As New Producto
        producto.nroDeProducto = id.Value
        producto.garantia = New TipoDeGarantia()
        producto.garantia.id = garantia.Value
        producto.ciclo = ciclo.Value
        producto.costoDePosesion = costoposesion.Value
        producto.costoEstandar = costoestandar.Value
        producto.costoFinanciero = costofinanciero.Value
        producto.descripcion = descripcion.Value
        producto.caracteristicas = caracteristicas.Value
        producto.altoPaquete = altopaquete.Value
        producto.anchoPaquete = anchopaquete.Value
        producto.largoPaquete = largopaquete.Value
        producto.estado = New Estado
        producto.estado.id = estado.Value
        producto.familiaDeProducto = New FamiliaDeProducto
        producto.familiaDeProducto.id = familia.Value
        producto.metodoDeReposicion = New MetodoDeReposicion
        producto.metodoDeReposicion.id = metodorep.Value
        producto.metodoDeValoracion = New MetodoValoracion
        producto.metodoDeValoracion.id = metodoval.Value
        producto.nombre = nombre.Value
        producto.porcentajeDeGanancia = porcentajeganancia.Value
        producto.precioVenta = precioventa.Value
        producto.puntoMaximoDeReposicion = puntomaximo.Value
        producto.puntoMinimoDeReposicion = puntominimo.Value
        producto.tipoDeProducto = New TipoDeProducto
        producto.tipoDeProducto.id = tipoproducto.Value

        producto.catalogos = New List(Of Modelo.Catalogo)
        For Each item As ListItem In chCatalogos.Items
            If item.Selected = True Then
                Dim cat = New Modelo.Catalogo
                cat.id = item.Value
                producto.catalogos.Add(cat)
            End If
        Next

        producto.descuentos = New List(Of Modelo.Descuento)
        For Each item As ListItem In chDescuentos.Items
            If item.Selected = True Then
                Dim cat = New Modelo.Descuento
                cat.id = item.Value
                producto.descuentos.Add(cat)
            End If
        Next

        If Not String.IsNullOrEmpty(imagen.FileName) Then
            producto.urlImagen = "/static/imagenes_de_producto/" & imagen.FileName
        End If

        GestorABM.instancia().guardarProducto(producto)
        If Not String.IsNullOrEmpty(imagen.FileName) Then
            Try
                imagen.SaveAs(Server.MapPath("~/static/imagenes_de_producto/" & imagen.FileName))
            Catch ex As Exception

            End Try
        End If

    End Sub

    <WebMethod> Public Shared Function obtenerId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdProducto()
    End Function

    Private Sub buscar(nombreProd As String, idProducto As Integer)
        Dim prod As New Producto
        prod.nombre = nombreProd
        Dim oProducto As Producto = GestorABM.instancia().buscarProducto(prod)
        If Not oProducto Is Nothing Then
            id.Value = oProducto.nroDeProducto
            nombre.Value = oProducto.nombre
            descripcion.Value = oProducto.descripcion
            precioventa.Value = oProducto.precioVenta
            porcentajeganancia.Value = oProducto.porcentajeDeGanancia
            costoposesion.Value = oProducto.costoDePosesion
            costofinanciero.Value = oProducto.costoFinanciero
            costoestandar.Value = oProducto.costoEstandar
            puntominimo.Value = oProducto.puntoMinimoDeReposicion
            puntomaximo.Value = oProducto.puntoMaximoDeReposicion
            altopaquete.Value = oProducto.altoPaquete
            anchopaquete.Value = oProducto.anchoPaquete
            largopaquete.Value = oProducto.largoPaquete
            ciclo.Value = oProducto.ciclo
            cantidad.Value = oProducto.cantidad
            garantia.Value = oProducto.garantia.id
            estado.Value = oProducto.estado.id
            familia.Value = oProducto.familiaDeProducto.id
            metodorep.Value = oProducto.metodoDeReposicion.id
            metodoval.Value = oProducto.metodoDeValoracion.id
            tipoproducto.Value = oProducto.tipoDeProducto.id
            caracteristicas.Value = oProducto.caracteristicas

            chCatalogos.ClearSelection()
            chDescuentos.ClearSelection()
            For Each oCatalogo As Modelo.Catalogo In oProducto.catalogos
                For Each item As ListItem In chCatalogos.Items
                    If oCatalogo.id = item.Value Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
            For Each oDescuento As Modelo.Descuento In oProducto.descuentos
                For Each item As ListItem In chDescuentos.Items
                    If oDescuento.id = item.Value Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub

    Protected Sub ltsProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ltsProductos.SelectedIndexChanged
        buscar(ltsProductos.SelectedValue, 0)
    End Sub
End Class