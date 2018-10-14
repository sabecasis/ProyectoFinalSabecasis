Public Class PreguntaDeEncuesta
    Property id As Integer
    Property respuestas As List(Of OpcionDeEncuesta)

    Property pregunta As String
    Property activa As Boolean
    Property encuesta As Encuesta

End Class
