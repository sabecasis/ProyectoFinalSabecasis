Imports System.Web.Services
Imports Modelo
Imports NegocioYServicios
Imports Seguridad

Public Class Registrarse
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        breadcrums.InnerHtml = Session("cadenabreadcrums")
        Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
        If Not oSesion Is Nothing Then
            aPerfil.InnerText = oSesion.usuario.nombre
        End If
    End Sub

    <WebMethod> Public Shared Function guardar(usuario As Usuario) As String
        Return GestorABM.instancia().guardarUsuario(usuario)
    End Function

    <WebMethod> Public Shared Sub enviarEmail(email As String, idUsuario As String)
        GestorSeguridad.instancia().enviarEmailDeConfirmacion(email, idUsuario)
    End Sub

    <WebMethod> Public Shared Function obtenerProximoId() As Integer
        Return ObtenerProximoIdHelper.instancia().obtenerProximoIdUsuario()
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

    <WebMethod> Public Shared Function obtenerTodosLosTiposDeDocumento() As List(Of TipoDeDocumento)
        Return GestorABM.instancia().obtenerTodosLosTiposDeDocumento()
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosTiposDeTelefono() As List(Of TipoDeTelefono)
        Return GestorABM.instancia().obtenerTodosLosTiposDeTelefono()
    End Function
End Class