Imports System.Web.SessionState
Imports System.Threading
Imports Negocio
Imports System.Threading.Tasks
Imports NegocioYServicios

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Dim hilo = New Thread(New System.Threading.ParameterizedThreadStart(AddressOf correrHiloDeOrdenes))
        hilo.IsBackground = True
        hilo.Start()
        Application("hilo") = hilo
    End Sub

    Private Shared Async Function correrHiloDeOrdenes() As Tasks.Task
        While (True)
            Try
                ServicioAsincronicoDeOrdenes.instancia().procesarOrden()
                Await Task.Delay(5000)
            Catch ex As Exception
                'TODO
            End Try
        End While
    End Function

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim hilo As Thread = DirectCast(Application("hilo"), Thread)
        If hilo.IsAlive Then
            hilo.Abort()
        End If
    End Sub

End Class