Public Class SesionDeChat
    Property usuario As Usuario
    Property asesor As Usuario
    Property fechaHoraInicio As DateTime
    Property fechaHoraFin As DateTime
    Property estado As Estado
    Property comentarios As String
    Property id As Integer
    Property mensajes As List(Of ComentarioDeChat)

End Class
