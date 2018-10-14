Imports Modelo
Imports System.Web.Services
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarTiposDeGarantia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

    Private Sub guardar()

        Dim tipo As New TipoDeGarantia
        tipo.id = Request.Form("id")
        tipo.descripcion = Request.Form("descripcion")
        tipo.dias = Request.Form("dias")
        Dim resultado As String = GestorABM.instancia().guardarTipoDeGarantia(tipo)

    End Sub

    Private Sub eliminar()
        Dim resultado As String = GestorABM.instancia().eliminarTipoDeGarantia(Request.Form("id"))

    End Sub

    <WebMethod> Public Shared Function buscar(tipo As TipoDeGarantia)
        Return GestorABM.instancia().buscarTipoDeGarantia(tipo)
    End Function

    <WebMethod> Public Shared Function obtenerId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdTipoDeGarantia()
    End Function

End Class