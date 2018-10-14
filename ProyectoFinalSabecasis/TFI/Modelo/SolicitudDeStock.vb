Public Class SolicitudDeStock
    Property sucursal As Sucursal
    Property nroPedido As Integer
    Property producto As Producto
    Property cantidad As Integer
    Property fecha As String
    Property estaIngersado As Boolean
    Property ingresosDeStock As List(Of IngresoDeStock)
    Property comprobante As Byte()
    Property usuario As Usuario
End Class
