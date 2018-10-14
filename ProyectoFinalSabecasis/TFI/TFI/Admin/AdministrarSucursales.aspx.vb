Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarSucursales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.IsPostBack Then
                If Request.Form("accion").Equals(ConstantesDeEvento.GUARDAR) Then
                    guardar()
                Else
                    eliminar()
                End If
            End If
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
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

    Private Sub guardar()
        ValidacionHelper.instancia().validarCampoVacio(Request.Form("nombre"), "nombre")
        ValidacionHelper.instancia().validarCampoVacio(Request.Form("calle"), "calle")
        ValidacionHelper.instancia().validarNoCero(Request.Form("nro"), "numero")
        ValidacionHelper.instancia().validarNoCero(Request.Form("localidad"), "localidad")
        Dim oSucursal As New Sucursal
        oSucursal.nroSucursal = Request.Form("id")
        oSucursal.estado = New Estado
        oSucursal.estado.id = Request.Form("estado")
        oSucursal.nombre = Request.Form("nombre")
        oSucursal.contacto = New Modelo.Contacto()
        oSucursal.contacto.calle = Request.Form("calle")
        oSucursal.contacto.departamento = Request.Form("depto")
        oSucursal.contacto.email = Request.Form("email")
        oSucursal.contacto.numero = Request.Form("nro")
        oSucursal.contacto.piso = Request.Form("piso")
        oSucursal.contacto.telefonos = New List(Of Telefono)
        Dim tel As New Telefono
        tel.tipo = New TipoDeTelefono
        tel.tipo.id = Request.Form("tipoTel")
        tel.telefono = Request.Form("telefono")
        oSucursal.contacto.telefonos.Add(tel)
        oSucursal.contacto.localidad = New Localidad
        oSucursal.contacto.localidad.id = Request.Form("localidad")
        GestorABM.instancia().guardarSucursal(oSucursal)
    End Sub

    Private Sub eliminar()
        Dim oSucursal As New Sucursal
        oSucursal.nroSucursal = Request.Form("id")
        GestorABM.instancia().eliminarSucursal(oSucursal)
    End Sub

    <WebMethod> Public Shared Function obtenerEstadosDeSucursal() As List(Of Estado)
        Return GestorABM.instancia().obtenerEstadosPorTipo(ConstantesDeEstado.SUCURSAL)
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosPaises() As List(Of Pais)
        Return GestorABM.instancia().obtenerMuchosPaises(Nothing)
    End Function

    <WebMethod> Public Shared Function obtenerTodasLasProvinciasPorPais(idPais As Integer) As List(Of Provincia)
        Return GestorABM.instancia().obtenerMuchasProvinicas(idPais)
    End Function

    <WebMethod> Public Shared Function obtenerTodasLasLocalidadesPorProvincia(idProvincia As Integer) As List(Of Localidad)
        Return GestorABM.instancia().obtenerMuchasLocalidades(idProvincia)
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosTiposDeTelefono() As List(Of TipoDeTelefono)
        Return GestorABM.instancia().obtenerTodosLosTiposDeTelefono()
    End Function

    <WebMethod> Public Shared Function obtenerId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdSucursal()
    End Function

    <WebMethod> Public Shared Function buscar(oSucursal As Sucursal) As Sucursal
        Return GestorABM.instancia().buscarSucursal(oSucursal)
    End Function
End Class