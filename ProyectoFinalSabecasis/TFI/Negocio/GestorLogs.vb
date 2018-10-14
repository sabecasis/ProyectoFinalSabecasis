Imports Modelo
Imports Mapper
Imports Seguridad
Imports Datos

Public Class GestorLogs

    Private Shared objeto As New GestorLogs

    Public Shared Function instancia() As GestorLogs
        Return objeto
    End Function
    Private Sub New()

    End Sub

    Public Function obtenerBitacora(elemento As ElementoDeBitacora) As List(Of ElementoDeBitacora)
        Try
            Dim criterio As New CriterioDeBusquedaBitacora
            criterio.idEvento = elemento.evento.id
            criterio.fecha = elemento.fecha
            criterio.usuario = elemento.usuario.nombre
            Return ElementoDeBitacoraDao.instancia().obtenerMuchos(criterio)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Sub guardarEnBitacora(idEvento As Integer, idUsuario As Integer)
        Try
            Dim elemento As New ElementoDeBitacora
            elemento.evento = New Evento
            elemento.evento.id = idEvento
            elemento.usuario = New Usuario
            elemento.usuario.id = idUsuario
            ElementoDeBitacoraDao.instancia().crear(elemento)
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Sub

End Class
