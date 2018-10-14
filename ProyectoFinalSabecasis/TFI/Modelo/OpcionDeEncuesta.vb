Public Class OpcionDeEncuesta
    Property id As Integer
    Property opcion As String
    Property cantidadDeSelecciones As Integer
    Property activa As Boolean
    Property variaciones As List(Of VariacionDePreguntaDeEncuesta)
End Class
