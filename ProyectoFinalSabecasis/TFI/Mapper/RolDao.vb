Imports Modelo
Imports DAL
Imports Seguridad

Public Class RolDao
    Inherits AbstractDao(Of Rol)

    Private Shared objeto As New RolDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As RolDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Rol) As Boolean
        Dim esExitoso As Boolean = False
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
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
                dto.parametro = "@inicia_en_admin"
                dto.tipoDeDato = SqlDbType.Bit
                dto.valor = oObject.iniciaEnAdmin
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.CREAR_ROL
                esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                For Each oPermiso As ElementoDePermiso In oObject.permisos
                    parametros = New List(Of DbDto)
                    dto = New DbDto
                    dto.parametro = "@id_rol"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.id
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)

                    dto = New DbDto
                    dto.parametro = "@id_permiso"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oPermiso.id
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)

                    query = ConstantesDeDatos.CREAR_RELACION_PERMISO_ROL
                    ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                Next

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso

    End Function

    Public Overrides Function eliminar(oObject As Rol) As Boolean
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
                query = ConstantesDeDatos.ELIMINAR_ROL
                esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As Rol) As Boolean
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
                dto.parametro = "@inicia_en_admin"
                dto.tipoDeDato = SqlDbType.Bit
                dto.valor = oObject.iniciaEnAdmin
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.MODIFICAR_ROL
                esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                parametros = New List(Of DbDto)
                dto = New DbDto
                dto.parametro = "@id_rol"
                dto.tipoDeDato = SqlDbType.BigInt
                dto.valor = oObject.id
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.ELIMINAR_RELACION_PERMISO_ROL
                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

                For Each oPermiso As ElementoDePermiso In oObject.permisos
                    parametros = New List(Of DbDto)
                    dto = New DbDto
                    dto.parametro = "@id_rol"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.id
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)

                    dto = New DbDto
                    dto.parametro = "@id_permiso"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oPermiso.id
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)

                    query = ConstantesDeDatos.CREAR_RELACION_PERMISO_ROL
                    ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Rol)
        Dim resultado As New List(Of Rol)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_ROLES
                Dim data As DataSet = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
                Dim tabla As DataTable = data.Tables(0)
                Dim oRol As Rol
                Dim permDao As AbstractDao(Of Permiso) = PermisoDao.instancia()
                Dim criterio As New CriterioDeBusqueda()
                If Not data Is Nothing Then
                    For Each fila As DataRow In tabla.AsEnumerable
                        oRol = New Rol
                        oRol.id = fila.Item("id")
                        oRol.nombre = fila.Item("nombre")
                        oRol.iniciaEnAdmin = fila.Item("inicia_en_admin")
                        criterio.criterioEntero = oRol.id
                        oRol.permisos = permDao.obtenerMuchos(criterio)
                        resultado.Add(oRol)
                    Next
                End If
            Else
                If oObject.criterioEntero <> 0 Then
                    Dim criterio As New CriterioDeBusqueda()
                    Dim permDao As AbstractDao(Of Permiso) = PermisoDao.instancia()
                    Dim dto As New DbDto
                    dto.parametro = "@id_usuario"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_ROLES_DE_USUARIO
                    Dim data As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim tabla As DataTable = data.Tables(0)
                    Dim oRol As Rol
                    If Not data Is Nothing Then
                        For Each fila As DataRow In tabla.AsEnumerable
                            oRol = New Rol
                            oRol.id = fila.Item("id")
                            oRol.nombre = fila.Item("nombre")
                            oRol.iniciaEnAdmin = fila.Item("inicia_en_admin")
                            criterio.criterioEntero = oRol.id
                            oRol.permisos = permDao.obtenerMuchos(criterio)
                            resultado.Add(oRol)
                        Next
                    End If
                End If

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Rol
        Dim oRol As Rol = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim data As DataSet
                If Not oObject.criterioString Is Nothing Then
                    Dim dto As New DbDto
                    dto.parametro = "@nombre"
                    dto.valor = oObject.criterioString
                    dto.tipoDeDato = SqlDbType.VarChar
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)

                    query = ConstantesDeDatos.BUSCAR_ROL_POR_NOMBRE
                    data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                ElseIf oObject.criterioEntero <> 0 Then

                    Dim dto As New DbDto
                    dto.parametro = "@id"
                    dto.valor = oObject.criterioEntero
                    dto.tipoDeDato = SqlDbType.VarChar
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)
                    query = ConstantesDeDatos.BUSCAR_ROL_POR_ID
                    data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                End If

                Dim permDao As AbstractDao(Of Permiso) = PermisoDao.instancia()
                Dim criterio As New CriterioDeBusqueda
                Dim table As DataTable = data.Tables(0)
                For Each row As DataRow In table.AsEnumerable
                    oRol = New Rol
                    oRol.id = row.Item("id")
                    oRol.nombre = row.Item("nombre")
                    oRol.iniciaEnAdmin = row.Item("inicia_en_admin")
                    criterio.criterioEntero = oRol.id
                    oRol.permisos = permDao.obtenerMuchos(criterio)
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oRol
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_ROL)
    End Function
End Class
