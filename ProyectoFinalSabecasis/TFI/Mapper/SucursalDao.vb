Imports Modelo
Imports DAL
Imports Seguridad

Public Class SucursalDao
    Inherits AbstractDao(Of Sucursal)

    Private Sub New()

    End Sub

    Private Shared objeto As New SucursalDao

    Public Shared Function instancia() As SucursalDao
        Return objeto
    End Function


    Public Overrides Function crear(oObject As Sucursal) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_SUCURSAL
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim parametro As New DbDto
            parametro.esParametroDeSalida = False
            parametro.valor = oObject.nroSucursal
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.parametro = "@nro_sucursal"
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre"
            parametro.valor = oObject.nombre
            parametro.tipoDeDato = SqlDbType.VarChar
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@estado_id"
            parametro.valor = oObject.estado.id
            parametro.tipoDeDato = SqlDbType.BigInt
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@calle"
            parametro.valor = oObject.contacto.calle
            parametro.tipoDeDato = SqlDbType.VarChar
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nro_puerta"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.contacto.numero
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@piso"
            parametro.tipoDeDato = SqlDbType.Int
            parametro.valor = oObject.contacto.piso
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@departamento"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.contacto.departamento
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@localidad_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.contacto.localidad.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@email"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.contacto.email
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@telefono"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.contacto.telefonos.Item(0).telefono
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@id_tipo_tel"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.contacto.telefonos.Item(0).tipo.id
            parametros.Add(parametro)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Sucursal) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_SUCURSAL
        Dim parametros As New List(Of DbDto)
        Dim esExitoso As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.nroSucursal
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As Sucursal) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Sucursal)
        Dim query As String = ConstantesDeDatos.OBTENER_TODAS_LAS_SUCURSALES
        Dim resultado As New List(Of Sucursal)
        Dim suc As Sucursal
        Try
          
            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                suc = New Sucursal
                suc.nroSucursal = row.Item("nro_de_sucursal")
                suc.nombre = row.Item("nombre")
                suc.estado = New Estado
                suc.estado.id = row.Item("estado_id")
                suc.contacto = New Contacto
                suc.contacto.id = row.Item("contacto_id")
                suc.contacto.calle = row.Item("calle")
                suc.contacto.departamento = row.Item("departamento")
                suc.contacto.email = row.Item("email")
                suc.contacto.localidad = New Localidad
                suc.contacto.localidad.id = row.Item("localidad_id")
                suc.contacto.localidad.provincia = New Provincia
                suc.contacto.localidad.provincia.id = row.Item("provincia_id")
                suc.contacto.localidad.provincia.pais = New Pais
                suc.contacto.localidad.provincia.pais.id = row.Item("pais_id")
                suc.contacto.numero = row.Item("nro_puerta")
                suc.contacto.piso = row.Item("piso")
                suc.contacto.telefonos = New List(Of Telefono)
                Dim tel As New Telefono
                tel.id = row.Item("telefono_id")
                tel.telefono = row.Item("telefono")
                tel.tipo = New TipoDeTelefono
                tel.tipo.id = row.Item("tipo_telefono_id")
                suc.contacto.telefonos.Add(tel)
                resultado.Add(suc)
                Exit For
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_SUCURSAL)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Sucursal
        Dim query As String = ConstantesDeDatos.OBTENER_SUCURSAL
        Dim parametros As New List(Of DbDto)
        Dim resultado As Sucursal = Nothing
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.criterioEntero
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nombre"
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.criterioString
            parametros.Add(param)

            Dim dset As DataSet
            Dim dtable As DataTable

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                resultado = New Sucursal
                resultado.nroSucursal = row.Item("nro_de_sucursal")
                resultado.nombre = row.Item("nombre")
                resultado.estado = New Estado
                resultado.estado.id = row.Item("estado_id")
                resultado.contacto = New Contacto
                resultado.contacto.id = row.Item("contacto_id")
                resultado.contacto.calle = row.Item("calle")
                resultado.contacto.departamento = row.Item("departamento")
                resultado.contacto.email = row.Item("email")
                resultado.contacto.localidad = New Localidad
                resultado.contacto.localidad.id = row.Item("localidad_id")
                resultado.contacto.localidad.provincia = New Provincia
                resultado.contacto.localidad.provincia.id = row.Item("provincia_id")
                resultado.contacto.localidad.provincia.pais = New Pais
                resultado.contacto.localidad.provincia.pais.id = row.Item("pais_id")
                resultado.contacto.numero = row.Item("nro_puerta")
                resultado.contacto.piso = row.Item("piso")
                resultado.contacto.telefonos = New List(Of Telefono)
                Dim tel As New Telefono
                tel.id = row.Item("telefono_id")
                tel.telefono = row.Item("telefono")
                tel.tipo = New TipoDeTelefono
                tel.tipo.id = row.Item("tipo_telefono_id")
                resultado.contacto.telefonos.Add(tel)
                Exit For
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function
End Class
