Public Class EgresoDeStock
    Property nroEgreso As Integer
    Property sucursal As Sucursal
    Property motivo As String
    Property usuario As Usuario
    Property comprobante As Byte()
    Property fecha As String
    Property productosEspecificosEnStock As List(Of ProductoEspecificoEnStock)
    Property productos As List(Of Producto)
    Property cantidad As Integer
    Property estado As Estado


End Class
