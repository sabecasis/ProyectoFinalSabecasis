Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Collections.Concurrent
Imports Modelo

Public Class ConnectionManager

    Private Shared oConnManager As ConnectionManager = New ConnectionManager
    Private Sub New()
    End Sub

    Public Shared Function obtenerInstancia() As ConnectionManager
        Return oConnManager
    End Function

    Public Function obtenerConexion() As SqlConnection
        Return obtenerConexion("mainAppConnection")
    End Function

    Public Function obtenerConexion(nombreConexion As String) As SqlConnection
        Dim config As System.Configuration.Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/TFI")
        Dim connString As System.Configuration.ConnectionStringSettings = config.ConnectionStrings.ConnectionStrings(nombreConexion)
        Dim connectionstring As String = connString.ConnectionString
        Dim conexion As SqlConnection
        conexion = New SqlConnection(connectionstring)
        Return conexion
    End Function

    Public Function ejecutarEnDB(parametros As List(Of DbDto), storedProcedure As String) As Boolean
        Dim conexion As SqlConnection = ConnectionManager.obtenerInstancia().obtenerConexion()
        Dim esExitoso As Boolean = False
        Try
            Dim comando As SqlCommand = New SqlCommand(storedProcedure, conexion)
            comando.CommandType = CommandType.StoredProcedure
            Dim parametro As SqlParameter
            If Not parametros Is Nothing Then
                Dim registros As Integer = parametros.Count
                For Each oParam As DbDto In parametros
                    parametro = New SqlParameter()
                    parametro.DbType = oParam.tipoDeDato
                    parametro.ParameterName = oParam.parametro
                    If oParam.esParametroDeSalida = True Then
                        parametro.Direction = ParameterDirection.Output
                    Else
                        parametro.Value = oParam.valor
                    End If
                    If oParam.tamanio <> 0 Then
                        parametro.Size = oParam.tamanio
                    End If
                    comando.Parameters.Add(parametro)
                Next
            End If
            'siempre habrá un parámetro de salida
            parametro = New SqlParameter()
            parametro.DbType = SqlDbType.Int
            parametro.ParameterName = "@resultado"
            parametro.Direction = ParameterDirection.Output
            comando.Parameters.Add(parametro)

            conexion.Open()
            comando.ExecuteScalar()
            If Not parametros Is Nothing Then
                For Each oParam As DbDto In parametros
                    If oParam.esParametroDeSalida = True Then
                        oParam.valor = comando.Parameters(oParam.parametro).Value
                    End If
                Next
            End If
            esExitoso = (comando.Parameters("@resultado").Value > 0)
        Catch ex As Exception
            Dim eee As String = ex.Message
            'TODO manejar la excepción
        Finally
            conexion.Close()
        End Try
        Return esExitoso
    End Function

    Public Function obtenerProximoId(tabla As String) As Integer
        Try
            Dim dto As New DbDto
            dto.parametro = "@tabla"
            dto.tipoDeDato = SqlDbType.VarChar
            dto.valor = tabla
            dto.esParametroDeSalida = False
            Dim parametros As New List(Of DbDto)
            parametros.Add(dto)

            Dim data As DataSet = leerBD(parametros, ConstantesDeDatos.OBTENER_PROXIMO_ID)
            Dim dtabla As DataTable = data.Tables(0)

            If Not data Is Nothing Then
                For Each fila As DataRow In dtabla.AsEnumerable
                    Return fila.Item(0)
                Next
            End If
        Catch ex As Exception
            'TODO
        End Try
        Return 0
    End Function

    Public Function leerBD(parametros As List(Of DbDto), storedProcedure As String) As DataSet
        Dim conexion As SqlConnection = ConnectionManager.obtenerInstancia().obtenerConexion()
        Dim dataset As New DataSet("genericDataSet")
        Try
            Dim comando As SqlCommand = New SqlCommand(storedProcedure, conexion)
            comando.CommandType = CommandType.StoredProcedure
            If Not parametros Is Nothing Then
                Dim registros As Integer = parametros.Count
                Dim parametro As SqlParameter
                
                For Each oParam As DbDto In parametros
                    parametro = New SqlParameter()
                    parametro.DbType = oParam.tipoDeDato
                    parametro.ParameterName = oParam.parametro
                    parametro.Value = oParam.valor
                    If oParam.tamanio <> 0 Then
                        parametro.Size = oParam.tamanio
                    End If
                    comando.Parameters.Add(parametro)
                Next
            End If

            Dim dataAdapter As New SqlDataAdapter(comando)
            conexion.Open()
            dataAdapter.Fill(dataset)

        Catch ex As Exception
            'TODO do something
            Dim eee As Integer = 0
        Finally
            conexion.Close()
        End Try

        Return dataset
    End Function

    Public Sub intentarRestaurar(ruta As String)
        Dim query As String = ""
        Dim conexion As SqlConnection = obtenerConexion("masterConnection")
        Try
            'Nos aseguramos de que el stored procedure exista en la base master (no podemos depender de un tercero para esto)
            query = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[restore_db]') AND type in (N'P', N'PC')) DROP PROCEDURE [dbo].[restore_db]"
            Dim comando As SqlCommand = New SqlCommand(query, conexion)
            comando.CommandType = CommandType.Text
            conexion.Open()

            comando.ExecuteNonQuery()

            comando.Dispose()

            query = "CREATE PROCEDURE [dbo].[restore_db]  @Archivo varchar(255) AS SET NOCOUNT ON IF (not EXISTS (SELECT name  FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = 'tfi_db' OR name = 'tfi_db'))) CREATE DATABASE tfi_db; declare @logfile varchar(MAX); declare @mdffile varchar(MAX); SELECT @logfile= [physical_name] FROM sys.[master_files] WHERE [database_id] IN (DB_ID('tfi_db'), DB_ID('tfi_db')) and name='tfi_db_log' ORDER BY [type], DB_NAME([database_id]); SELECT @mdffile= [physical_name] FROM sys.[master_files] WHERE [database_id] IN (DB_ID('tfi_db'), DB_ID('tfi_db')) and name='tfi_db' ORDER BY [type], DB_NAME([database_id]); ALTER DATABASE tfi_db SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE tfi_db FROM DISK = @Archivo WITH MOVE 'tfi_db' TO @mdffile, MOVE 'tfi_db_log' TO  @logfile, REPLACE;  ALTER DATABASE tfi_db SET MULTI_USER;"
            comando = New SqlCommand(query, conexion)
            comando.CommandType = CommandType.Text
            comando.ExecuteNonQuery()

            comando.Dispose()

            comando = New SqlCommand(ConstantesDeDatos.RESTAURAR_BACKUP, conexion)
            comando.CommandType = CommandType.StoredProcedure

            Dim param As New SqlParameter("@Archivo", SqlDbType.VarChar)
            param.Value = ruta
            comando.Parameters.Add(param)

            comando.ExecuteNonQuery()

            comando.Dispose()
            conexion.Close()

        Catch e As Exception
            conexion.Close()
            Throw e
        End Try

    End Sub


    Public Function crearBackup(ruta As String) As Boolean
        Dim conexion As SqlConnection = ConnectionManager.obtenerInstancia().obtenerConexion()
        Try

            Dim comando As SqlCommand = New SqlCommand(ConstantesDeDatos.CREAR_BACKUP, conexion)
            comando.CommandType = CommandType.StoredProcedure

            Dim file As String = ruta.Substring(ruta.LastIndexOf("\") + 1)
            Dim folder As String = ruta.Substring(0, ruta.LastIndexOf("\"))

            comando.Parameters.AddWithValue("@folder", folder)
            comando.Parameters.AddWithValue("@file", file)

            conexion.Open()

            comando.ExecuteNonQuery()

            comando.Dispose()
            conexion.Close()

            Return True

        Catch e As Exception
            conexion.Close()
            Throw e
        End Try

    End Function
End Class
