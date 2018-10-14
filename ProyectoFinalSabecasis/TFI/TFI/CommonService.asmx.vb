Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports Modelo
Imports Seguridad
Imports NegocioYServicios

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CommonService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function traducir(dto As IdiomaDto) As List(Of ElementoDeIdioma)
        Dim elementos As List(Of ElementoDeIdioma) = GestorIdioma.instancia().obtenerElementosTraducidos(dto.leyendas, dto.idioma)
        Return elementos
    End Function

    <WebMethod()> _
    Public Function obtenerIdiomas() As List(Of Idioma)
        Return GestorABM.instancia().obtenerTodosLosIdiomasCreados()
    End Function

    <WebMethod()> _
    Public Function obtenerPermisos(usuario As String) As List(Of Permiso)
        Dim oSesion As Sesion = Nothing
        Try
            oSesion = UserCache.instancia().mapa.Item(usuario)
        Catch e As Exception
        End Try
        If Not oSesion Is Nothing Then
            Return oSesion.permisos
        Else
            Return New List(Of Permiso)
        End If
    End Function

    <WebMethod()> _
    Public Function esEnAdmin(usuario As String) As Boolean
        Dim oSesion As Sesion = Nothing
        Dim iniciaEnAdmin As Boolean = False
        Try
            oSesion = UserCache.instancia().mapa.Item(usuario)
        Catch e As Exception
        End Try
        If Not oSesion Is Nothing Then

            For Each oRol As Rol In oSesion.usuario.roles
                If oRol.iniciaEnAdmin = True Then
                    iniciaEnAdmin = True
                    Exit For
                End If
            Next

        End If
        Return iniciaEnAdmin
    End Function

    <WebMethod()> _
    Public Function obtenerPermisosHijos(dto As PresentacionDto) As List(Of Permiso)
        Dim oSesion As Sesion = Nothing
        Try
            oSesion = UserCache.instancia().mapa.Item(dto.usuario)
        Catch e As Exception
        End Try
        Dim idPadre As Integer = 0
        Dim resultados As New List(Of Permiso)
        If Not oSesion Is Nothing Then
            For Each oPermiso As Permiso In oSesion.permisos
                If oPermiso.url.Equals(dto.url) Then
                    idPadre = oPermiso.id
                    Exit For
                End If
            Next
            For Each oPermiso As Permiso In oSesion.permisos
                If Not oPermiso.permisoPadre Is Nothing Then
                    If oPermiso.permisoPadre.id = idPadre Then
                        resultados.Add(oPermiso)
                    End If
                End If
            Next
        Else
            Dim base As List(Of Permiso) = GestorSeguridad.instancia().obtenerPermisosBase()
            For Each oPermiso As Permiso In base
                If oPermiso.url.Equals(dto.url) Then
                    idPadre = oPermiso.id
                    Exit For
                End If
            Next
            For Each oPermiso As Permiso In base
                If Not oPermiso.permisoPadre Is Nothing Then
                    If oPermiso.permisoPadre.id = idPadre Then
                        resultados.Add(oPermiso)
                    End If
                End If
            Next
        End If
        Return resultados
    End Function
End Class