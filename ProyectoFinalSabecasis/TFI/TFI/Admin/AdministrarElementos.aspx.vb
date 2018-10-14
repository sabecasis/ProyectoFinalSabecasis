Imports System.Web.Services
Imports Modelo
Imports Seguridad
Imports NegocioYServicios

Public Class AdministrarElementos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Not Me.IsPostBack Then
                Dim lista As List(Of Elemento) = GestorABM.instancia().obtenerTodosLosElementos()
                LstElementos.DataSource = lista
                LstElementos.DataTextField = "nombre"
                LstElementos.DataBind()
            End If
            breadcrums.InnerHtml = Session("cadenabreadcrums")
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

    <WebMethod> Public Shared Function obtenerId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdElemento()
    End Function

    <WebMethod> Public Shared Function guardar(oElemento As Elemento) As String
        Return GestorABM.instancia().guardarElemento(oElemento)
    End Function

    <WebMethod(CacheDuration:=0)> Public Shared Function buscar(nombre As String) As Elemento
        Dim elemento As Elemento = GestorABM.instancia().buscarElemento(nombre)
        If elemento Is Nothing Then
            Throw New ExcepcionDelSistema(New Exception(ConstantesDeMensaje.NO_SE_ENCONTRO_LA_BUSQUEDA))
        End If
        Return elemento
    End Function

    <WebMethod> Public Shared Function eliminar(id As Integer) As String
        Return GestorABM.instancia().eliminarElemento(id)
    End Function

    Protected Sub LstElementos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstElementos.SelectedIndexChanged
        Dim oElemento As Elemento = GestorABM.instancia().buscarElemento(LstElementos.SelectedValue)
        id.Value = oElemento.id
        nombre.Value = oElemento.nombre
        leyenda.Value = oElemento.leyendaPorDefecto
    End Sub
End Class