Imports Datos
Imports Modelo
Imports System.IO
Imports Seguridad

Public Class GestorBackup
    Private Sub New()

    End Sub

    Private Shared objeto As New GestorBackup

    Public Shared Function instancia() As GestorBackup
        Return objeto
    End Function

    Public Function restaurarBackup(url As String) As String
        Dim resultado As Boolean = BackupRestoreDao.instancia().restaurarBackup(url)
        If resultado Then
            Return ConstantesDeMensaje.BACKUP_RESTAURADO
        Else
            Return ConstantesDeMensaje.ERROR_AL_RESTAURAR
        End If
    End Function

    Public Function obtenerBackups() As List(Of Backup)
        Return BackupRestoreDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function crearBackup() As Boolean
        Dim oBackup As New Backup
        oBackup.fecha = DateTime.Now
        oBackup.urlEnServidor = Path.Combine(Path.Combine("C:\", "backups"), oBackup.fecha.ToString("MM-dd-yyyy-HH-mm") & ".bak")
        oBackup.secuencia = 1
        Return BackupRestoreDao.instancia().crear(oBackup)
    End Function
End Class
