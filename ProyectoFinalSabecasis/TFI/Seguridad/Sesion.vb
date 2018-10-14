Imports Modelo
Imports System.Runtime.Serialization

Public Class Sesion

    Property usuario As Usuario
    Property idioma As Integer

    Property permisos As List(Of Permiso)
    Property checkout As Checkout

End Class
