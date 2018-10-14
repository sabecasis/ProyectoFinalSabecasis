Imports Modelo
Imports System.Web.Services
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarFamiliaDeProducto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            breadcrums.InnerHtml = Session("cadenabreadcrums")
            If Me.IsPostBack Then
                If Request.Form("accion").Equals(ConstantesDeEvento.GUARDAR) Then
                    guardar()
                Else
                    eliminar()
                End If
            Else
                LstFamilias.DataSource = GestorABM.instancia().obtenerTodasLasFamiliasDeProducto()
                LstFamilias.DataTextField = "familia"
                LstFamilias.DataValueField = "id"
                LstFamilias.DataBind()
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

    Private Sub eliminar()
        ValidacionHelper.instancia().validarCampoVacio(id.Value, "id")
        GestorABM.instancia().eliminarFamiliaDeProducto(id.Value)
    End Sub
    Private Sub guardar()
        Dim ofamilia As New FamiliaDeProducto
        ofamilia.id = id.Value
        ofamilia.familia = familia.Value
        GestorABM.instancia().guardarFamiliaDeProductos(ofamilia)
    End Sub
    <WebMethod> Public Shared Function buscar(familia As FamiliaDeProducto) As FamiliaDeProducto
        Return GestorABM.instancia().buscarFamiliaDeProducto(familia)
    End Function

    <WebMethod> Public Shared Function obtenerId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdFamiliaDeProducto()
    End Function

    Protected Sub LstFamilias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstFamilias.SelectedIndexChanged
        Dim oFamiliaBusqueda As New FamiliaDeProducto
        oFamiliaBusqueda.id = Convert.ToInt32(LstFamilias.SelectedValue)
        oFamiliaBusqueda.familia = ""
        Dim oFamilia As FamiliaDeProducto = GestorABM.instancia().buscarFamiliaDeProducto(oFamiliaBusqueda)
        id.Value = oFamilia.id
        familia.Value = oFamilia.familia
    End Sub
End Class