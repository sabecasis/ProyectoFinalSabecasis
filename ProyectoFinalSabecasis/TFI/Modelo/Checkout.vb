Public Class Checkout
    Property idSesion As Long
    Property usuario As Usuario
    Property fecha As Date
    Property estado As Estado
    Property envio As Envio
    Property productos As Dictionary(Of Integer, Integer)
    Property orden As Orden
    Property totalAPagar As Double

End Class
