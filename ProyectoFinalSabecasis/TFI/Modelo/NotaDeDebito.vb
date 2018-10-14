Public Class NotaDeDebito
    Property nroNotaDeDebito As Integer
    Property descripcion As String
    Property factura As Factura
    Property monto As Double
    Property comprobante As Byte()
    Property estaActiva As Boolean
    Property fechaEmision As Date
    Property impuestos As List(Of Impuesto)
    Property sucursal As Sucursal
End Class
