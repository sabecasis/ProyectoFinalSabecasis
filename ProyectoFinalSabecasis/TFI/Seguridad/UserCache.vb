Imports Seguridad
Imports Modelo

Public Class UserCache

    Private Shared objeto As New UserCache

    Property mapa As New Dictionary(Of String, Sesion)
    Property checkoutsIntermedios As Dictionary(Of String, Checkout)


    Public Shared Function instancia() As UserCache
        Return objeto
    End Function

    Private Sub New()

    End Sub


End Class
