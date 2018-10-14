Public Class DetalleDeFactura
    Property precioUnitario As Double
    Property sucursal As Sucursal
    Property tipoDeFactura As TipoDeFactura
    Property cantidad As Integer
    Property producto As Producto
    Property id As Long
    Property descuentos As List(Of Descuento)

End Class
