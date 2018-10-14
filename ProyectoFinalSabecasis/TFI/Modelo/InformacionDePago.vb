Public Class InformacionDePago
    Property id As Long
    Property metodo As MetodoDePago
    Property titular As String
    Property email As String
    Property nroDeTarjeta As String
    Property cvv As String
    Property mesVencimiento As String
    Property anioVencimiento As String
    Property persona As Persona
    Property passwordPaypal As String
    Property nroNotaDeCredito As Integer

End Class
