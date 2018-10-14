Public Class EstadoDeCuenta
    Property totalFacturado As Double
    Property totalEnDebito As Double
    Property totalEnCredito As Double
    Property notasDeCredito As List(Of NotaDeCredito)
    Property facturas As List(Of Factura)
    Property notasDeDebito As List(Of NotaDeDebito)
    Property persona As Persona
End Class
