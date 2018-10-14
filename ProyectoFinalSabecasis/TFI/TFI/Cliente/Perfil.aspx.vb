Imports Modelo
Imports Seguridad
Imports System.Web.Services
Imports NegocioYServicios

Public Class Perfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSesion As Sesion = DirectCast(Session("sesion"), Sesion)
        If oSesion Is Nothing Then
            referrer.Value = "/Cliente/IniciarSesion.aspx"


        Else
            If oSesion.usuario Is Nothing Then
                referrer.Value = "/Cliente/IniciarSesion.aspx"
            Else
                aPerfil.InnerText = oSesion.usuario.nombre
                usuarioId.Value = oSesion.usuario.id
                unombre.Value = oSesion.usuario.nombre

            End If
        End If
        breadcrums.InnerHtml = Session("cadenabreadcrums")

    End Sub


    <WebMethod> Public Shared Function guardar(usuario As Usuario) As String
        Return GestorABM.instancia().guardarUsuario(usuario)
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

    <WebMethod> Public Shared Function obtenerTodosLosTiposDeDocumento() As List(Of TipoDeDocumento)
        Return GestorABM.instancia().obtenerTodosLosTiposDeDocumento()
    End Function

    <WebMethod> Public Shared Function obtenerTodosLosTiposDeTelefono() As List(Of TipoDeTelefono)
        Return GestorABM.instancia().obtenerTodosLosTiposDeTelefono()
    End Function
End Class