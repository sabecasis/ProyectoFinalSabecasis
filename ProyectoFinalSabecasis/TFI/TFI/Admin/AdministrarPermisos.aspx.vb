Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarPermisos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Not Me.IsPostBack Then
                Dim lista As List(Of Permiso) = GestorABM.instancia().obtenerMuchosPermisos(Nothing)
                LstPermisos.DataSource = lista
                LstPermisos.DataTextField = "nombre"
                LstPermisos.DataValueField = "nombre"
                LstPermisos.DataBind()
                Dim lista2 As List(Of Elemento) = GestorABM.instancia().obtenerTodosLosElementos()
                LstElemento.DataSource = lista2
                LstElemento.DataTextField = "nombre"
                LstElemento.DataValueField = "id"
                LstElemento.DataBind()
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

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdPermiso()
    End Function

    <WebMethod> Public Shared Function guardar(oPermiso As Permiso) As String
        Return GestorABM.instancia().guardarPermiso(oPermiso)
    End Function

    <WebMethod> Public Shared Function buscar(nombre As String) As Permiso
        Return GestorABM.instancia().buscarPermiso(nombre)
    End Function

    <WebMethod> Public Shared Function eliminar(id As Integer) As String
        Return GestorABM.instancia().eliminarPermiso(id)
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosElementosPaginados(desde As Integer, hasta As Integer) As List(Of Elemento)
        Return GestorABM.instancia().obtenerElementosPaginados(desde, hasta, 0)
    End Function

    <WebMethod> Public Shared Function obtenerCantidadDeElementos() As Integer
        Return GestorABM.instancia().obtenerCantidadDeElementosTraducibles()
    End Function

    Protected Sub LstPermisos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstPermisos.SelectedIndexChanged
        Try
            Dim oPermiso As Permiso = GestorABM.instancia().buscarPermiso(LstPermisos.SelectedValue)
            id.Value = oPermiso.id
            nombre.Value = oPermiso.nombre
            url.Value = oPermiso.url
            LstElemento.SelectedValue = oPermiso.elemento.id
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
    End Sub
End Class