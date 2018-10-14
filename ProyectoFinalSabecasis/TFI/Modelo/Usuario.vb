Public Class Usuario
    Property id As Integer
    Property persona As Persona
    Property nombre As String
    Property password As String
    Property bloqueado As Boolean
    Property roles As List(Of Rol)
    Property contadorMalaPassword As Integer



End Class
