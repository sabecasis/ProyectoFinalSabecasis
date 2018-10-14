Public Class Factura
    Property nroFactura As Long
    Property sucursal As Sucursal
    Property montoDeVenta As Double
    Property egresoDeStock As EgresoDeStock
    Property usuario As Usuario
    Property fechaDeCobro As DateTime
    Property fechaDeVenta As DateTime
    Property montoDeCobro As Double
    Property tipoDeFactura As TipoDeFactura
    Property orden As Orden
    Property comprobante As Byte()
    Property email As Email
    Property detallesDeFactura As List(Of DetalleDeFactura)
    Property notaDeCreditoAplicada As NotaDeCredito
    Property recargoPorTarjeta As Double
End Class
