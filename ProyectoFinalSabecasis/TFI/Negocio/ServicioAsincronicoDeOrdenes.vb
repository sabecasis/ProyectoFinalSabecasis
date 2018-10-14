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
            orden.orden.facturas = New List(Of Factura)
            Dim cobrado As Boolean = GestorDePagos.instancia().cobrarProducto(orden.orden)
            If cobrado Then
                'Dim oEgreso As EgresoDeStock = GestorStock.instancia().obtenerProductosParaEgreso()
                orden.estado.id = 2
                orden.orden.estado.id = 2
                GestorOrdenes.instancia().actualizarOrdenEnCola(orden)
            Else
                'TODO poner orden para reintento
            End If
        End If
    End Sub
End Class
