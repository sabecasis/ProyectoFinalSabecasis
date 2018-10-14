Imports Modelo
Imports Seguridad
Imports System.Net.Mail
Imports Datos
Imports System.IO

Public Class GestorSeguridad
    Private Shared objeto As New GestorSeguridad
    Private Sub New()

    End Sub
    Public Shared Function instancia() As GestorSeguridad
        Return objeto
    End Function

    Public Function buscarFormularios(criterio As String) As List(Of String)
        Dim result As New List(Of String)
        Dim permisos As List(Of Permiso) = PermisoDao.instancia().obtenerMuchos(Nothing)
        For Each oPerm As Permiso In permisos
            If oPerm.url.ToUpper.Contains(criterio.ToUpper) Then
                result.Add(oPerm.url)
            End If
        Next
        Return result
    End Function
    Public Function obtenerPermisosBase() As List(Of Permiso)
        Try
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioEntero = 2 'ROL usuario ANONIMO
            Dim listaPermisos As List(Of Permiso) = PermisoDao.instancia().obtenerMuchos(criterio)
            Return listaPermisos
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerPermisosSiempreAutorizados() As List(Of Permiso)
        Try
            Dim criterio As New CriterioDeBusqueda
            criterio.criterioEntero = 3 'ROL paginas siempre autorizadas
            Dim listaPermisos As List(Of Permiso) = PermisoDao.instancia().obtenerMuchos(criterio)
            Return listaPermisos
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function


    Public Sub enviarEmailConfirmacionPassword(usuario As String)
        Try
            Dim oUsuario As Usuario = GestorABM.instancia().buscarUsuario(usuario)
            enviarEmail(ConstantesDeMensaje.MENSAJE_CONFIRMAR_PASSWORD, ConstantesDeMensaje.ASUNTO_CONFIRMAR_PASSWORD, oUsuario.persona.contacto.email)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Sub

    Public Function autenticar(usuario As String, password As String) As Sesion
        Try
            Dim resultado As Sesion = Nothing
            If Not usuario Is Nothing Then
                Dim oUsuario As Usuario = GestorABM.instancia().buscarUsuario(usuario)
                If Not oUsuario Is Nothing Then
                    If password.Trim.Equals(oUsuario.password.Trim) Then
                        If Not oUsuario.bloqueado Then
                            resultado = New Sesion
                            resultado.usuario = oUsuario
                            Dim permisos As New List(Of Permiso)
                            permisos.AddRange(GestorSeguridad.instancia().obtenerPermisosBase())
                            For Each oRol As Rol In oUsuario.roles
                                For Each oPermiso As Permiso In oRol.permisos
                                    If Not permisos.Contains(oPermiso) Then
                                        permisos.Add(oPermiso)
                                    End If
                                Next
                            Next
                            resultado.permisos = permisos
                        End If
                    End If
                End If
            End If
            Return resultado
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Private Sub enviarEmail(cuerpo As String, asunto As String, direccion As String)
        enviarEmail(cuerpo, asunto, direccion, Nothing)
    End Sub


    Public Sub enviarEmail(cuerpo As String, asunto As String, direccion As String, adjunto As Byte())
        Try
            Dim SmtpServer As New SmtpClient("localhost", 8080)
            Dim mail As New MailMessage()

            SmtpServer.Host = ConstantesDeSeguridad.SMTP_SERVER
            SmtpServer.Port = ConstantesDeSeguridad.SMTP_PORT
            SmtpServer.EnableSsl = True
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network
            SmtpServer.UseDefaultCredentials = False
            SmtpServer.Credentials = New Net.NetworkCredential(ConstantesDeSeguridad.EMAIL, ConstantesDeSeguridad.PASSWORD)

            mail = New MailMessage()
            mail.From = New MailAddress(ConstantesDeSeguridad.EMAIL)
            mail.To.Add(direccion)
            mail.Subject = asunto
            mail.Body = cuerpo

            If Not adjunto Is Nothing Then
                mail.Attachments.Add(New Attachment(New MemoryStream(adjunto), Date.Now.ToString("dd-MM-yyyy") & ".pdf"))
            End If

            SmtpServer.Send(mail)

        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Sub

    Public Sub enviarEmailDeConfirmacion(email As String, usuario As String)
        Try
            enviarEmail(ConstantesDeMensaje.MENSAJE_EMAIL_DESBLOQUEO & "http://localhost:41721/Cliente/Activar.aspx?id=" & usuario, ConstantesDeMensaje.ASUNTO_EMAIL_DESBLOQUEO, email)
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Sub

    Public Sub enviarEmailRecuperarContrasenia(email As String, usuario As String)
        Try
            enviarEmail(ConstantesDeMensaje.MENSAJE_EMAIL_PASSWORD & "http://localhost:41721/Cliente/CambiarPassword.aspx?id=" & usuario, ConstantesDeMensaje.ASUNTO_EMAIL_PASSWORD, email)
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Sub

    Public Function cambiarPassword(usuario As String, password As String) As String
        Try
            Dim result As Boolean = UsuarioDao.instancia().modificarContrasenia(password, usuario)
            If result Then
                Return ConstantesDeMensaje.PASSWORD_CAMBIADA
            Else
                Return ConstantesDeMensaje.PASSWORD_NO_CAMBIADA
            End If
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function
End Class
