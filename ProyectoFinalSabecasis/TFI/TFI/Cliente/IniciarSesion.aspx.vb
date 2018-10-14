Imports Modelo
Imports Negocio
Imports Seguridad
Imports NegocioYServicios

Public Class IniciarSesion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Me.IsPostBack Then
                login()
            Else
                If Not Request.Params("flag") Is Nothing AndAlso Not urlanterior.Value.Contains("IniciarSesion") Then
                    LblMensaje.InnerText = ConstantesDeMensaje.NO_TIENE_PERMISO
                End If
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

    Public Sub login()
        Dim nombre As String = nombreusuario.Value
        Dim pass As String = contrasenia.Value
        ValidacionHelper.instancia().validarCampoVacio(nombre, "usuario")
        ValidacionHelper.instancia().validarCampoVacio(pass, "password")
        Dim oSesion As Sesion = GestorSeguridad.instancia().autenticar(nombre, pass)
        If Not oSesion Is Nothing Then
            If Request.Cookies("usuario_" & nombre) Is Nothing Then
                Dim cookie As New HttpCookie("usuario_" & nombre)
                cookie.Value = oSesion.usuario.nombre
                cookie.Expires = DateTime.Now.AddMinutes(20)
                Response.Cookies.Add(cookie)
            End If
            Session.Timeout = 20
            Session("sesion") = oSesion
            If Not UserCache.instancia().mapa.ContainsKey(oSesion.usuario.nombre) Then
                UserCache.instancia().mapa.Add(oSesion.usuario.nombre, oSesion)
            Else
                UserCache.instancia().mapa.Item(oSesion.usuario.nombre) = oSesion
            End If

            GestorLogs.instancia().guardarEnBitacora(1, oSesion.usuario.id)
            Dim iniciaEnAdmin As Boolean = False
            For Each oRol As Rol In oSesion.usuario.roles
                If oRol.iniciaEnAdmin = True Then
                    iniciaEnAdmin = True
                    Exit For
                End If
            Next
            If urlanterior.Value.Equals("") Or urlanterior.Value.Contains("CerrarSesion") Or urlanterior.Value.Contains("IniciarSesion") Or urlanterior.Value.Contains("Inicio") Then
                If iniciaEnAdmin Then
                    Response.Redirect("/Admin/Inicio.aspx")
                Else
                    Response.Redirect("/Cliente/Inicio.aspx")
                End If

            Else
                If Request.QueryString("check") Is Nothing Then
                    Response.Redirect(urlanterior.Value)
                Else
                    Response.Redirect("/Cliente/Catalogo.aspx?check=" & Request.QueryString("check"))
                End If

            End If
        Else
            LblMensaje.InnerText = ConstantesDeMensaje.USUARIO_O_PASSWORD_INCORRECTOS
        End If
    End Sub

End Class