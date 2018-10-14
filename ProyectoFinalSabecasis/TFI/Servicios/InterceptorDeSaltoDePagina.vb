Imports System.Web
Imports Seguridad
Imports Modelo

Public Class InterceptorDeSaltoDePagina
    Implements IHttpModule

    Public Sub Dispose() Implements IHttpModule.Dispose

    End Sub

    Public Sub Init(context As HttpApplication) Implements IHttpModule.Init
        AddHandler context.PreRequestHandlerExecute, AddressOf Me.controlarPermisos
    End Sub


    Private Sub controlarPermisos(ByVal source As Object, ByVal e As EventArgs)
        Dim aplicacion As HttpApplication = DirectCast(source, HttpApplication)
        Dim context As HttpContext = aplicacion.Context
        Try
            Dim oSesion As Sesion = Nothing
            If Not System.Web.HttpContext.Current.Session Is Nothing Then
                oSesion = System.Web.HttpContext.Current.Session("sesion")
            End If

            Dim autorizado As Boolean = False

            Dim direccion As String = context.Request.FilePath
            If direccion.Contains(".aspx") Then
                If Not oSesion Is Nothing Then
                    For Each oPermiso As Permiso In oSesion.permisos
                        If direccion.Contains(oPermiso.url) Then
                            autorizado = True
                            Exit For
                        End If
                    Next
                End If

                Dim base As List(Of Permiso) = GestorSeguridad.instancia().obtenerPermisosSiempreAutorizados()
                For Each oPermiso As Permiso In base
                    If direccion.Contains(oPermiso.url) Then
                        autorizado = True
                        Exit For
                    End If
                Next
            Else
                autorizado = True
            End If

            If autorizado = False Then
                context.Response.Redirect("/Cliente/IniciarSesion.aspx?flag=1")
            End If
        Catch ex As Exception
            context.Response.Redirect("/Cliente/IniciarSesion.aspx?flag=1")
        End Try

    End Sub
End Class
