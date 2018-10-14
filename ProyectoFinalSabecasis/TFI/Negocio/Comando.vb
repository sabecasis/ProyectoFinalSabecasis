Public MustInherit Class Comando(Of T, I)

    Public MustOverride Function ejecutar(oObject As T) As I

    Public MustOverride Sub deshacer()

    Property estado As Object
End Class
