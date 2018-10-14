Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarUsuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = Session("sesion")
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        If Not Me.IsPostBack Then
            tipoDoc.DataSource = GestorABM.instancia().obtenerTodosLosTiposDeDocumento()
            tipoDoc.DataTextField = "tipo"
            tipoDoc.DataValueField = "id"
            tipoDoc.DataBind()
            tipoTel.DataSource = GestorABM.instancia().obtenerTodosLosTiposDeTelefono()
            tipoTel.DataTextField = "tipo"
            tipoTel.DataValueField = "id"
            tipoTel.DataBind()
            LstUsuarios.DataSource = GestorABM.instancia().obtenerTodosLosUsuarios()
            LstUsuarios.DataTextField = "nombre"
            LstUsuarios.DataValueField = "nombre"
            LstUsuarios.DataBind()
            chkRoles.DataSource = GestorABM.instancia().obtenerMuchosRoles(Nothing)
            chkRoles.DataTextField = "nombre"
            chkRoles.DataValueField = "id"
            chkRoles.DataBind()
        End If
    End Sub

    <WebMethod> Public Shared Function guardar(usuario As Usuario) As String
        Return GestorABM.instancia().guardarUsuario(usuario)
    End Function

  
    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdUsuario()
    End Function

    <WebMethod> Public Shared Function eliminar(id As Integer) As String
        Return GestorABM.instancia().eliminarUsuario(id)
    End Function

    <WebMethod> Public Shared Function buscar(nombre As String) As Usuario
        Return GestorABM.instancia().buscarUsuario(nombre)
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


    Protected Sub LstUsuarios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstUsuarios.SelectedIndexChanged
        Dim oUsuario As Usuario = GestorABM.instancia().buscarUsuario(LstUsuarios.SelectedValue)
        id.Value = oUsuario.id
        bloqueado.Value = oUsuario.bloqueado
        nombreusuario.Value = oUsuario.nombre
        nombre.Value = oUsuario.persona.nombre
        apellido.Value = oUsuario.persona.apellido
        documento.Value = oUsuario.persona.documento
        tipoDoc.Value = oUsuario.persona.tipoDeDocumento.id
        telefono.Value = oUsuario.persona.contacto.telefonos(0).telefono
        tipoTel.Value = oUsuario.persona.contacto.telefonos(0).tipo.id
        calle.Value = oUsuario.persona.contacto.calle
        nro.Value = oUsuario.persona.contacto.numero
        piso.Value = oUsuario.persona.contacto.piso
        depto.Value = oUsuario.persona.contacto.departamento
        email.Value = oUsuario.persona.contacto.email

        pais.DataSource = GestorABM.instancia().obtenerMuchosPaises(Nothing)
        pais.DataTextField = "pais"
        pais.DataValueField = "id"
        pais.DataBind()
        pais.Value = oUsuario.persona.contacto.localidad.provincia.pais.id
        provincia.DataSource = GestorABM.instancia().obtenerMuchasProvinicas(oUsuario.persona.contacto.localidad.provincia.pais.id)
        provincia.DataTextField = "provincia"
        provincia.DataValueField = "id"
        provincia.DataBind()
        provincia.Value = oUsuario.persona.contacto.localidad.provincia.id
        localidad.DataSource = GestorABM.instancia().obtenerMuchasLocalidades(oUsuario.persona.contacto.localidad.provincia.id)
        localidad.DataTextField = "localidad"
        localidad.DataValueField = "id"
        localidad.DataBind()
        localidad.Value = oUsuario.persona.contacto.localidad.id
        For Each item As ListItem In chkRoles.Items
            item.Selected = False
        Next
        For Each oRol As Rol In oUsuario.roles
            chkRoles.Items.FindByValue(oRol.id.ToString()).Selected = True
        Next
    End Sub
End Class