Public Class Producto
    Property nroDeProducto As Integer
    Property nombre As String
    Property descripcion As String
    Property precioVenta As Double
    Property estado As Estado
    Property porcentajeDeGanancia As Double
    Property familiaDeProducto As FamiliaDeProducto
    Property metodoDeValoracion As MetodoValoracion
    Property costoDePosesion As Double
    Property costoFinanciero As Double
    Property costoEstandar As Double
    Property metodoDeReposicion As MetodoDeReposicion
    Property puntoMinimoDeReposicion As Integer
    Property puntoMaximoDeReposicion As Integer
    Property ciclo As Integer
    Property tipoDeProducto As TipoDeProducto
    Property cantidad As Integer
    Property garantia As TipoDeGarantia
    Property urlImagen As String
    Property catalogos As List(Of Catalogo)
    Property descuentos As List(Of Descuento)
    Property valoracion As ValoracionDeProducto
    Property altoPaquete As Integer
    Property anchoPaquete As Integer
    Property largoPaquete As Integer
    Property peso As Double
    Property caracteristicas As String
End Class
