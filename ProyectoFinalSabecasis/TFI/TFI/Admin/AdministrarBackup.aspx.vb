Imports Modelo
Imports System.Web.Services
Imports NegocioYServicios
Imports Seguridad

Public Class AdministrarBackup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim oSesion As Sesion = Session("sesion")
            If Not oSesion Is Nothing Then
                aPerfil.InnerText = oSesion.usuario.nombre
            End If
            If Me.IsPostBack Then
                If ConstantesDeEvento.BACKUP.Equals(Request.Form("accion")) Then
                    GestorLogs.instancia().guardarEnBitacora(3, oSesion.usuario.id)
                    GestorBackup.instancia().crearBackup()
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

    <WebMethod> Public Shared Function restaurarBackup(url As String) As String
        Return GestorBackup.instancia().restaurarBackup(url)
    End Function

    <WebMethod> Public Shared Function obtenerBackups() As List(Of Backup)
        Return GestorBackup.instancia().obtenerBackups()
    End Function

End Class