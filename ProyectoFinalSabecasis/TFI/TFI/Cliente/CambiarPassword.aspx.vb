Imports Modelo
Imports Seguridad
Imports NegocioYServicios

Public Class CambiarPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.IsPostBack Then
                cambiarContrasenia(contrasenia.Value, contrasenia2.Value, nombreusuario.Value)
            Else
                If Not String.IsNullOrEmpty(Request.Params("id")) Then
                    nombreusuario.Value = Criptografia.ObtenerInstancia().DecypherTripleDES(Request.Params("id"), ConstantesDeSeguridad.ENC_KEY, True)
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


    Private Sub cambiarContrasenia(contrasenia, contrasenia2, usuario)
        ValidacionHelper.instancia().validarCampoVacio(usuario, ConstantesDeMensaje.PASSWORD_VACIA)
        ValidacionHelper.instancia().validarCampoVacio(contrasenia, ConstantesDeMensaje.PASSWORD_VACIA)
        ValidacionHelper.instancia().validarCampoVacio(contrasenia2, ConstantesDeMensaje.PASSWORD_VACIA)
        If contrasenia.Equals(contrasenia2) Then
            Dim mensaje As String = GestorSeguridad.instancia().cambiarPassword(usuario, contrasenia)
            mansajeEnPantalla.InnerText = mensaje
            GestorSeguridad.instancia().enviarEmailConfirmacionPassword(usuario)
        Else
            mansajeEnPantalla.InnerText = ConstantesDeMensaje.PASSWORDS_NO_COINCIDEN
        End If
    End Sub

End Class