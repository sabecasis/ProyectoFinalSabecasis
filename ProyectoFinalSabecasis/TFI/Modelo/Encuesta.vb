Public Class Encuesta
    Property id As Integer
    Property tipo As TipoDeEncuesta
    Property nombre As String
    Property descripcion As String
    Property preguntas As List(Of PreguntaDeEncuesta)
    Property fechaDesde As String
    Property fechaHasta As String
End Class
