Public Class ProductoEspecificoEnStock
    Property producto As Producto
    Property nroDeSerie As Integer
    Property sucursal As Sucursal
    Property estado As Estado
    Property precioCompra As Double
    Property precioVenta As Double
    Property fechaModificacion As DateTime
    Property motivoModificacion As String
    Property ingreso As IngresoDeStock
    Property egreso As EgresoDeStock

    Property garantia As Garantia
End Class
