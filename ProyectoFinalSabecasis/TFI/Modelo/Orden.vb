Public Class Orden
    Property nroDeOrden As Long
    Property fechaInicio As String

    Property totalAPagar As Double
    Property fechaFinalizacion As String
    Property estado As Estado
    Property usuario As Usuario
    Property detalles As List(Of DetalleDeOrden)

    Property informacionDePago As InformacionDePago
    Property cuotas As Integer
    Property factura As Factura
    Property envio As Envio
    Property egreso As EgresoDeStock
    Property sucursal As Sucursal
    Property recargoPorTarjeta As Double

End Class
