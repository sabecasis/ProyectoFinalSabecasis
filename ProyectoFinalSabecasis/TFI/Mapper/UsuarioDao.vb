Imports Modelo
Imports DAL
Imports Seguridad

Public Class UsuarioDao
    Inherits AbstractDao(Of Usuario)

    Private Shared objeto As New UsuarioDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As UsuarioDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Usuario) As Boolean
        Dim esExitoso As Boolean = False
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try

            Dim parametro As New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre_usuario"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.nombre.ToLower
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@contrasenia"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = Criptografia.ObtenerInstancia().CypherTripleDES(Criptografia.ObtenerInstancia().GetHashMD5(oObject.password.Trim()), ConstantesDeSeguridad.ENC_KEY, True)
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.nombre
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@apellido"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.apellido
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@documento"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.documento
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_tipo_doc"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.tipoDeDocumento.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@calle"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.calle
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nro_puerta"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.numero
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@piso"
            parametro.tipoDeDato = SqlDbType.Int
            parametro.valor = oObject.persona.contacto.piso
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@departamento"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.departamento
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@localidad_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.persona.contacto.localidad.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@email"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.email
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@telefono"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.telefonos.Item(0).telefono
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_tipo_tel"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.persona.contacto.telefonos.Item(0).tipo.id
            parametros.Add(parametro)

            query = ConstantesDeDatos.CREAR_USUARIO
            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            parametros = New List(Of DbDto)
            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_usuario"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.id
            parametros.Add(parametro)

            query = ConstantesDeDatos.ELIMINAR_ROLES_DE_USUARIO
            ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            For Each oRol In oObject.roles
                parametros = New List(Of DbDto)
                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@id_usuario"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.valor = oObject.id
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@id_rol"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.valor = oRol.id
                parametros.Add(parametro)

                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, ConstantesDeDatos.CREAR_ROLES_DE_USUARIO)
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Usuario) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.id
            parametros.Add(param)
            query = ConstantesDeDatos.ELIMINAR_USUARIO
            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As Usuario) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim parametro As New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre_usuario"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.nombre
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@contrasenia"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = Criptografia.ObtenerInstancia().CypherTripleDES(Criptografia.ObtenerInstancia().GetHashMD5(oObject.password), ConstantesDeSeguridad.ENC_KEY, True)
            parametros.Add(parametro)


            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.nombre
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@bloqueado"
            parametro.tipoDeDato = SqlDbType.Bit
            parametro.valor = oObject.bloqueado
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@apellido"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.apellido
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@documento"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.documento
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_tipo_doc"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.tipoDeDocumento.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@calle"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.calle
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nro_puerta"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.numero
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@piso"
            parametro.tipoDeDato = SqlDbType.Int
            parametro.valor = oObject.persona.contacto.piso
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@departamento"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.departamento
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@localidad_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.persona.contacto.localidad.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@email"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.email
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@telefono"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.persona.contacto.telefonos.Item(0).telefono
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_tipo_tel"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.persona.contacto.telefonos.Item(0).tipo.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@contador_mala_password"
            parametro.tipoDeDato = SqlDbType.Int
            parametro.valor = oObject.contadorMalaPassword
            parametros.Add(parametro)

            query = ConstantesDeDatos.MODIFICAR_USUARIO
            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)


            parametros = New List(Of DbDto)
            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_usuario"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.id
            parametros.Add(parametro)

            query = ConstantesDeDatos.ELIMINAR_ROLES_DE_USUARIO
            ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            For Each oRol In oObject.roles
                parametros = New List(Of DbDto)
                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@id_usuario"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.valor = oObject.id
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@id_rol"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.valor = oRol.id
                parametros.Add(parametro)
                query = ConstantesDeDatos.CREAR_ROLES_DE_USUARIO
                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Usuario)
        Dim oUsuario As Usuario = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim listaUsuarios As New List(Of Usuario)
        Try
            query = ConstantesDeDatos.BUSCAR_TODOS_LOS_USUARIOS
                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim tabla As DataTable = dataSet.Tables(0)
                For Each row In tabla.AsEnumerable
                    oUsuario = New Usuario
                    oUsuario.nombre = row.Item("nombre_usuario")
                    oUsuario.id = row.Item("usuario_id")
                    oUsuario.bloqueado = row.Item("bloqueado")
                oUsuario.password = Criptografia.ObtenerInstancia().DecypherTripleDES(row.Item("password"), ConstantesDeSeguridad.ENC_KEY, True)
                    oUsuario.persona = New Persona
                    oUsuario.persona.id = row.Item("persona_id")
                    oUsuario.persona.nombre = row.Item("nombre")
                    oUsuario.persona.apellido = row.Item("apellido")
                    oUsuario.persona.documento = row.Item("nro_documento")
                    oUsuario.persona.tipoDeDocumento = New TipoDeDocumento()
                    oUsuario.persona.tipoDeDocumento.id = row.Item("tipo_documento_id")
                    oUsuario.persona.contacto = New Contacto
                    oUsuario.persona.contacto.id = row.Item("contacto_id")
                    oUsuario.persona.contacto.calle = row.Item("calle")
                    oUsuario.persona.contacto.numero = row.Item("nro_puerta")
                    oUsuario.persona.contacto.piso = row.Item("piso")
                    oUsuario.persona.contacto.departamento = row.Item("departamento")
                    oUsuario.persona.contacto.email = row.Item("email")
                    oUsuario.persona.contacto.localidad = New Localidad
                    oUsuario.persona.contacto.localidad.id = row.Item("localidad_id")
                    oUsuario.persona.contacto.localidad.provincia = New Provincia
                    oUsuario.persona.contacto.localidad.provincia.id = row.Item("provincia_id")
                    oUsuario.persona.contacto.localidad.provincia.pais = New Pais
                    oUsuario.persona.contacto.localidad.provincia.pais.id = row.Item("pais_id")
                    oUsuario.persona.contacto.telefonos = New List(Of Telefono)
                    Dim tel As New Telefono()
                    tel.id = row.Item("telefono_id")
                    tel.telefono = row.Item("telefono")
                    tel.tipo = New TipoDeTelefono()
                    tel.tipo.id = row.Item("tipo_telefono_id")
                    oUsuario.persona.contacto.telefonos.Add(tel)
                    Dim critRol As New CriterioDeBusqueda
                    critRol.criterioEntero = oUsuario.id
                    Dim rDao As AbstractDao(Of Rol) = RolDao.instancia()
                    oUsuario.roles = rDao.obtenerMuchos(critRol)
                listaUsuarios.Add(oUsuario)
                Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return listaUsuarios
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_USUARIO)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Usuario
        Dim oUsuario As Usuario = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try
            If Not oObject Is Nothing Then
                If Not oObject.criterioString Is Nothing Then

                    Dim param As New DbDto
                    param.parametro = "@nombre_usuario"
                    param.esParametroDeSalida = False
                    param.tipoDeDato = SqlDbType.VarChar
                    param.valor = oObject.criterioString
                    parametros.Add(param)

                    query = ConstantesDeDatos.BUSCAR_USUARIO
                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim tabla As DataTable = dataSet.Tables(0)
                    For Each row In tabla.AsEnumerable
                        oUsuario = New Usuario
                        oUsuario.nombre = row.Item("nombre_usuario")
                        oUsuario.id = row.Item("usuario_id")
                        oUsuario.bloqueado = row.Item("bloqueado")
                        oUsuario.contadorMalaPassword = row.Item("contador_mala_password")
                        oUsuario.password = Criptografia.ObtenerInstancia().DecypherTripleDES(row.Item("password"), ConstantesDeSeguridad.ENC_KEY, True)
                        oUsuario.persona = New Persona
                        oUsuario.persona.id = row.Item("persona_id")
                        oUsuario.persona.nombre = row.Item("nombre")
                        oUsuario.persona.apellido = row.Item("apellido")
                        oUsuario.persona.documento = row.Item("nro_documento")
                        oUsuario.persona.tipoDeDocumento = New TipoDeDocumento()
                        oUsuario.persona.tipoDeDocumento.id = row.Item("tipo_documento_id")
                        oUsuario.persona.contacto = New Contacto
                        oUsuario.persona.contacto.id = row.Item("contacto_id")
                        oUsuario.persona.contacto.calle = row.Item("calle")
                        oUsuario.persona.contacto.numero = row.Item("nro_puerta")
                        oUsuario.persona.contacto.piso = row.Item("piso")
                        oUsuario.persona.contacto.departamento = row.Item("departamento")
                        oUsuario.persona.contacto.email = row.Item("email")
                        oUsuario.persona.contacto.localidad = New Localidad
                        oUsuario.persona.contacto.localidad.id = row.Item("localidad_id")
                        oUsuario.persona.contacto.localidad.provincia = New Provincia
                        oUsuario.persona.contacto.localidad.provincia.id = row.Item("provincia_id")
                        oUsuario.persona.contacto.localidad.provincia.pais = New Pais
                        oUsuario.persona.contacto.localidad.provincia.pais.id = row.Item("pais_id")
                        oUsuario.persona.contacto.telefonos = New List(Of Telefono)
                        Dim tel As New Telefono()
                        tel.id = row.Item("telefono_id")
                        tel.telefono = row.Item("telefono")
                        tel.tipo = New TipoDeTelefono()
                        tel.tipo.id = row.Item("tipo_telefono_id")
                        oUsuario.persona.contacto.telefonos.Add(tel)
                        Dim critRol As New CriterioDeBusqueda
                        critRol.criterioEntero = oUsuario.id
                        Dim rDao As AbstractDao(Of Rol) = RolDao.instancia()
                        oUsuario.roles = rDao.obtenerMuchos(critRol)
                        Exit For
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oUsuario
    End Function

    Public Function modificarContrasenia(contrasenia, usuario) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim param As New DbDto
            param.parametro = "@nombre_usuario"
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = usuario
            parametros.Add(param)

            param = New DbDto
            param.parametro = "@contrasenia"
            param.esParametroDeSalida = False
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = Criptografia.ObtenerInstancia().CypherTripleDES(Criptografia.ObtenerInstancia().GetHashMD5(contrasenia), ConstantesDeSeguridad.ENC_KEY, True)
            parametros.Add(param)

            query = ConstantesDeDatos.CAMBIAR_PASSWORD
            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.ToString)
        End Try
        Return resultado
    End Function
End Class
