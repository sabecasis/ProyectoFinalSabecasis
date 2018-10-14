Imports System.Web.Services
Imports Modelo
Imports Negocio
Imports Seguridad
Imports NegocioYServicios
Imports System.Globalization

Public Class MetodoDeEnvio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Session("sesion") Is Nothing Then
                Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
                aPerfil.InnerText = oSesion.usuario.nombre
                If Me.IsPostBack Then
                    If Request.Form("accion").Equals(ConstantesDeEvento.SELECCIONAR_ENVIO) Then
                        Dim oEnvio As New Envio
                        If Not Request.Form("infoPerfil") Is Nothing Or 1 = Convert.ToInt32(Request.Form("tipoDeEnvio")) Then
                            oEnvio.contacto = DirectCast(Session("Sesion"), Sesion).usuario.persona.contacto
                        ElseIf 2 = Convert.ToInt32(Request.Form("tipoDeEnvio")) Then
                            oEnvio.contacto = New Modelo.Contacto()
                            oEnvio.contacto.numero = DirectCast(Request.Form("sucursaloca"), String)
                            oEnvio.contacto.localidad = GestorABM.instancia().obtenerLocalidadPorCp(Request.Form("codigopostal"))
                        Else
                            Dim contacto As New Modelo.Contacto
                            contacto.calle = Request.Form("calle")
                            contacto.numero = Request.Form("nro")
                            contacto.piso = Request.Form("piso")
                            contacto.departamento = Request.Form("depto")
                            contacto.localidad = New Localidad
                            contacto.localidad.id = Convert.ToInt32(Request.Form("localidad"))
                            contacto.email = Request.Form("email")
                            contacto.telefonos = New List(Of Telefono)
                            Dim tel As New Telefono
                            tel.telefono = Request.Form("telefono")
                            tel.tipo = New TipoDeTelefono
                            tel.tipo.id = Convert.ToInt32(Request.Form("tipoTel"))
                            contacto.telefonos.Add(tel)
                            oEnvio.contacto = contacto
                        End If
                        oEnvio.tipo = New TipoDeEnvio
                        oEnvio.tipo.id = Convert.ToInt32(Request.Form("tipoDeEnvio"))
                        Dim provider As New NumberFormatInfo()
                        provider.NumberDecimalSeparator = "."
                        provider.NumberGroupSeparator = ","
                        provider.NumberGroupSizes = {3}
                        oEnvio.monto = Convert.ToDouble(Request.Form("precio"), provider)
                        oEnvio.productos = DirectCast(Session("Sesion"), Sesion).checkout.productos
                        oEnvio.estado = New Estado
                        oEnvio.estado.id = 1
                        oEnvio.sucursal = New Sucursal
                        oEnvio.sucursal.nroSucursal = 1
                        DirectCast(Session("Sesion"), Sesion).checkout.envio = oEnvio
                        Dim result As Boolean = GestorOrdenes.instancia().generarEnvio(oEnvio)
                        If result Then
                            Response.Redirect("/Cliente/PagoDeOrden.aspx")
                        Else
                            'TODO agregar mensaje de error
                        End If

                    End If
                End If
                idContacto.Value = DirectCast(Session("Sesion"), Sesion).usuario.nombre
            End If
            If (IsPostBack) Then
                ClientScript.RegisterClientScriptBlock(Me.GetType, "IsPostBack", "var isPostBack = true; ", True)
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

    <WebMethod> Public Shared Function obtenerTiposDeEnvio() As List(Of TipoDeEnvio)
        Return GestorABM.instancia().obtenerTodosLosTiposDeEnvio()
    End Function

    <WebMethod> Public Shared Function obtenerInformacionDeContacto(idContacto As String) As Modelo.Contacto
        Return GestorABM.instancia().buscarUsuario(idContacto).persona.contacto
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


    <WebMethod> Public Shared Function obtenerSucursalesDeOca(cp As String) As List(Of KeyValuePair(Of String, String))
        Return GestorOrdenes.instancia().obtenerSucursalesDeOcaPorLocalidad(cp)
    End Function

    <WebMethod> Public Shared Function obtenerPrecioEnvioPorCp(cp As String, idCheckout As String) As Double
        Return GestorOrdenes.instancia().obtenerCostoDeEnvioDeCheckout(idCheckout, cp)
    End Function

    <WebMethod> Public Shared Function obtenerPrecioEnvioPorLocalidad(idLocalidad As Integer, idCheckout As String) As Double
        Dim localidad As Localidad = GestorABM.instancia().obtenerLocalidadPorId(idLocalidad)
        If Not localidad Is Nothing Then
            Return GestorOrdenes.instancia().obtenerCostoDeEnvioDeCheckout(idCheckout, localidad.cp)
        End If
        Return 0
    End Function
End Class