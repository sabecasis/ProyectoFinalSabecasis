Public Class CriterioDeBusquedaProductoEspecifico
    Inherits CriterioDeBusqueda

    Public Const ASCENDENTE As String = "asc"
    Public Const DESCENDENTE As String = "desc"

    Property orden As String
    Property cantidad As Integer
    Property nroSucursal As Integer
    Property nroProducto As Integer
    Property idUsuario As Long

End Class
