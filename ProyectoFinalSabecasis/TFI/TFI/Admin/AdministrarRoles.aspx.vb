Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarRoles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            breadcrums.InnerHtml = Session("cadenabreadcrums")
            If Not Me.IsPostBack Then
                LstRoles.DataSource = GestorABM.instancia().obtenerMuchosRoles(Nothing)
                LstRoles.DataTextField = "nombre"
                LstRoles.DataValueField = "nombre"
                LstRoles.DataBind()
                chkPermisos.DataSource = GestorABM.instancia().obtenerMuchosPermisos(Nothing)
                chkPermisos.DataTextField = "nombre"
                chkPermisos.DataValueField = "id"
                chkPermisos.DataBind()
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
    End Sub

    <WebMethod> Public Shared Function guardar(rol As Rol) As String
        Return GestorABM.instancia().guardarRol(rol)
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosRoles() As List(Of Rol)
        Dim resultado As List(Of Rol) = GestorABM.instancia().obtenerMuchosRoles(Nothing)
        Return resultado
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosPermisos() As List(Of Permiso)
        Dim resultado As List(Of Permiso) = GestorABM.instancia().obtenerMuchosPermisos(Nothing)
        Return resultado
    End Function

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdRol()
    End Function

    <WebMethod> Public Shared Function eliminar(id As Integer) As String
        Return GestorABM.instancia().eliminarRol(id)
    End Function

    <WebMethod> Public Shared Function buscar(nombre As String) As Rol
        Return GestorABM.instancia().buscarRol(nombre)
    End Function


    Protected Sub LstRoles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstRoles.SelectedIndexChanged
        Dim oRol As Rol = GestorABM.instancia().buscarRol(LstRoles.SelectedValue)
        If Not oRol Is Nothing Then
            id.Value = oRol.id
            nombre.Value = oRol.nombre
            iniciaadmin.Checked = oRol.iniciaEnAdmin

            For Each item As ListItem In chkPermisos.Items
                item.Selected = False
            Next
            For Each oPermiso As Permiso In oRol.permisos
                chkPermisos.Items.FindByValue(oPermiso.id.ToString()).Selected = True
            Next
        End If

    End Sub
End Class