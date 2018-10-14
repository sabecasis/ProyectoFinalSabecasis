Imports Modelo

Public Class ServicioAsincronicoDeOrdenes
    Private Sub New()

    End Sub

    Private Shared objeto As New ServicioAsincronicoDeOrdenes

    Public Shared Function instancia() As ServicioAsincronicoDeOrdenes
        Return objeto
    End Function

    Public Sub procesarOrden()
        Dim orden As OrdenEnCola = GestorOrdenes.instancia().obtenerProximaOrdenEnCola()
        If Not orden Is Nothing Then
            Dim cobrado As Boolean = GestorDePagos.instancia().cobrarProducto(orden.orden)
            If cobrado Then
                orden.estado.id = 2
                orden.orden.estado.id = 2
                GestorOrdenes.instancia().actualizarOrdenEnCola(orden)
            End If
        End If
    End Sub
End Class
