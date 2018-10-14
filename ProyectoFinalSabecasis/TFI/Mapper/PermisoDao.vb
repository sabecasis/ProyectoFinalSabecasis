Imports Modelo
Imports DAL
Imports Seguridad

Public Class PermisoDao
    Inherits AbstractDao(Of Permiso)

    Private Shared objeto As New PermisoDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As PermisoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Permiso) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@nombre"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.nombre
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@url"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.url
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@elemento_id"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.elemento.id
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.CREAR_PERMISO
                esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Permiso) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.ELIMINAR_PERMISO
                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As Permiso) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@nombre"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.nombre
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@url"
                dto.tipoDeDato = SqlDbType.BigInt
                dto.valor = oObject.url
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@elemento_id"
                dto.tipoDeDato = SqlDbType.BigInt
                dto.valor = oObject.elemento.id
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.MODIFICAR_PERMISO
                esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Permiso)
        Dim resultado As New List(Of Permiso)
        Dim data As DataSet = Nothing
        Dim tabla As DataTable = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_PERMISOS
                data = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
                tabla = data.Tables(0)
            Else
                Dim criteria As CriterioDeBusqueda = oObject

                Dim dto As New DbDto
                dto.parametro = "@id_rol"
                dto.valor = criteria.criterioEntero
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)
                query = ConstantesDeDatos.OBTENER_PERMISOS_POR_ROL
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                tabla = data.Tables(0)
            End If

            Dim oPermiso As Permiso
            If Not data Is Nothing Then
                For Each fila As DataRow In tabla.AsEnumerable
                    oPermiso = New Permiso
                    oPermiso.id = fila.Item("id")
                    oPermiso.nombre = fila.Item("permiso")
                    oPermiso.url = fila.Item("url")
                    If Not IsDBNull(fila.Item("permiso_padre_id")) Then
                        oPermiso.permisoPadre = New Permiso
                        oPermiso.permisoPadre.id = fila.Item("permiso_padre_id")
                    End If
                    oPermiso.elemento = New Elemento
                    oPermiso.elemento.id = fila.Item("elemento_id")
                    If Not IsDBNull(fila.Item("leyenda_por_defecto")) Then
                        oPermiso.elemento.leyendaPorDefecto = fila.Item("leyenda_por_defecto")
                    Else
                        oPermiso.elemento.leyendaPorDefecto = ""
                    End If
                    If Not IsDBNull(fila.Item("nombre_elemento")) Then
                        oPermiso.elemento.nombre = fila.Item("nombre_elemento")
                    Else
                        oPermiso.elemento.nombre = ""
                    End If

                    resultado.Add(oPermiso)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Permiso
        Dim oPermiso As Permiso = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim data As DataSet

                Dim dto As New DbDto
                dto.parametro = "@nombre"
                dto.valor = oObject.criterioString
                dto.tipoDeDato = SqlDbType.VarChar
                dto.esParametroDeSalida = False
                parametros.Add(dto)
                query = ConstantesDeDatos.BUSCAR_PERMISO_POR_NOMBRE
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                Dim table As DataTable = data.Tables(0)
                For Each row As DataRow In table.AsEnumerable
                    oPermiso = New Permiso
                    oPermiso.id = row.Item("id")
                    oPermiso.nombre = row.Item("permiso")
                    oPermiso.url = row.Item("url")
                    If Not IsDBNull(row.Item("permiso_padre_id")) Then
                        oPermiso.permisoPadre = New Permiso
                        oPermiso.permisoPadre.id = row.Item("permiso_padre_id")
                    End If
                    oPermiso.elemento = New Elemento
                    oPermiso.elemento.id = row.Item("elemento_id")
                    If Not IsDBNull(row.Item("leyenda_por_defecto")) Then
                        oPermiso.elemento.leyendaPorDefecto = row.Item("leyenda_por_defecto")
                    Else
                        oPermiso.elemento.leyendaPorDefecto = ""
                    End If
                    If Not IsDBNull(row.Item("nombre_elemento")) Then
                        oPermiso.elemento.nombre = row.Item("nombre_elemento")
                    Else
                        oPermiso.elemento.nombre = ""
                    End If
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return oPermiso
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_PERMISO)
    End Function
End Class
