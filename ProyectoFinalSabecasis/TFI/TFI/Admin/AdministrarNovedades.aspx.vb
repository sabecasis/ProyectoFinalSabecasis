Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarNovedades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.IsPostBack And ConstantesDeEvento.GUARDAR.Equals(Request.Form("accion")) Then
                ValidacionHelper.instancia().validarCampoVacio(ImagenNovedad.FileName, "Imagen pequeña")
                ValidacionHelper.instancia().validarCampoVacio(ImagenPrincipal.FileName, "Imagen grande")
                ValidacionHelper.instancia().validarCampoVacio(Request.Form("nombre"), "nombre")
                If ImagenNovedad.FileName.Substring(ImagenNovedad.FileName.LastIndexOf(".")).Equals(".png") Then
                    ImagenNovedad.SaveAs(Server.MapPath(" ").Replace("Admin", "") & "static/add_img/novedad_" & Request.Form("nombre") & ".png")
                    ImagenPrincipal.SaveAs(Server.MapPath("").Replace("Admin", "") & "static/add_img/novedad_" & Request.Form("nombre") & "_grande.png")
                Else
                    Throw New ExcepcionDeValidacion("Debe ser una imagen en formato png")
                End If
                Dim recargador = New RecargadorDeNovedades()
                Dim newsid As Integer = Convert.ToInt32(Request.Form("newsletterscombo"))

                recargador.recargar(newsid, System.Web.HttpContext.Current)
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

    <WebMethod> Public Shared Function obtenerTodosLosNewsletter() As List(Of Newsletter)
        Return GestorABM.instancia().obtenerTodosLosNewsletter()
    End Function

    <WebMethod> Public Shared Function obtenerNovedadPorNombre(nombre As String) As Novedad
        Return GestorABM.instancia().obtenerNovedadPorNombre(nombre)
    End Function

    <WebMethod> Public Shared Function guardar(oNovedad As Novedad) As String
        Return GestorABM.instancia().crearNovedad(oNovedad)
    End Function

    <WebMethod> Public Shared Function eliminar(idNovedad As Integer) As String
        Return GestorABM.instancia().eliminarNovedad(idNovedad)
    End Function

    <WebMethod> Public Shared Function obtenerTodasLasNovedades() As List(Of Novedad)
        Return GestorABM.instancia().obtenerTodasLasNovedades()
    End Function

End Class