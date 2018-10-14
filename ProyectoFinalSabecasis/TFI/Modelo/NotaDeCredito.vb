Public Class NotaDeCredito
    Property nroNotaDeCredito As Integer
    Property descripcion As String
    Property factura As Factura
    Property monto As Double
    Property comprobante As Byte()
    Property estaActiva As Boolean
    Property fechaCaducidad As Date
    Property fechaEmision As Date
    Property impuestos As List(Of Impuesto)
    Property sucursal As Sucursal
    Property devolucionDeProducto As DevolucionDeProducto
End Class
