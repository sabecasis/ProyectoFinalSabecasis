Public Class Contacto
    Property id As Integer
    Property email As String
    Property calle As String
    Property numero As Integer
    Property piso As Integer
    Property departamento As String

    Property localidad As Localidad
    Property telefonos As List(Of Telefono)


End Class
