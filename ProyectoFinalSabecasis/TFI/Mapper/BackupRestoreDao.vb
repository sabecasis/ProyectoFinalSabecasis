Imports Seguridad
Imports DAL
Imports Modelo

Public Class BackupRestoreDao
    Inherits AbstractDao(Of Backup)

    Private Sub New()

    End Sub

    Private Shared objeto As New BackupRestoreDao

    Public Shared Function instancia() As BackupRestoreDao
        Return objeto
    End Function

    Public Function restaurarBackup(ruta As String) As Boolean
        Try
            ConnectionManager.obtenerInstancia().intentarRestaurar(ruta)
        Catch ex As ExcepcionDeDatos
            Try
                ConnectionManager.obtenerInstancia().intentarRestaurar(ruta)
            Catch exe As ExcepcionDeDatos
                Throw ex
            End Try
        End Try
        Return True
    End Function

    Public Function crearBackup(ruta As String) As Boolean
        Return ConnectionManager.obtenerInstancia().crearBackup(ruta)
    End Function

    Public Overrides Function crear(oObject As Backup) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@url"
                dto.valor = oObject.urlEnServidor
                dto.tipoDeDato = SqlDbType.VarChar
                dto.tamanio = 400
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.CREAR_OBJETO_BACKUP
                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                crearBackup(oObject.urlEnServidor)
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As Backup) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Backup) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Backup)
        Dim tabla As DataTable
        Dim dset As DataSet
        Dim resultado As New List(Of Backup)
        Dim oElemento As Backup
        Dim query As String = ""
        Try
            query = ConstantesDeDatos.OBTENER_LOS_ULTIMOS_BACKUP
            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            tabla = dset.Tables(0)

            For Each fila As DataRow In tabla.AsEnumerable
                oElemento = New Backup
                oElemento.fecha = fila.Item("fecha")
                oElemento.id = fila.Item("backup_id")
                oElemento.urlEnServidor = fila.Item("url_de_servidor")
                resultado.Add(oElemento)
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Backup
        Return Nothing
    End Function
End Class
