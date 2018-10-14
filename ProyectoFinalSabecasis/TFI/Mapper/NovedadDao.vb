Imports Modelo
Imports DAL
Imports Seguridad

Public Class NovedadDao
    Inherits AbstractDao(Of Novedad)

    Private Sub New()

    End Sub

    Private Shared objeto As New NovedadDao

    Public Shared Function instancia() As NovedadDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Novedad) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_NOVEDAD
        Dim parametros As New List(Of DbDto)
        Dim parametro As DbDto
        Dim result As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@newsletter_id"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            param.valor = oObject.newsletter.id
            parametros.Add(param)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@novedad"
            parametro.tamanio = -1
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.novedad
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.nombre
            parametro.tamanio = 100
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@configuracion_add_rotator"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.configuracionAddRotator
            parametro.tamanio = -1
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@titulo"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.titulo
            parametro.tamanio = 300
            parametros.Add(parametro)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Novedad) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_NOVEDAD
        Dim parametros As New List(Of DbDto)
        Dim parametro As DbDto
        Dim result As Boolean = False
        Try

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@novedad_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.id
            parametro.tamanio = 18
            parametros.Add(parametro)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function modificar(oObject As Novedad) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Novedad)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As New List(Of Novedad)
        Dim descuento As Novedad
        Dim dset As New DataSet
        Dim dtable As DataTable
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODAS_LAS_NOVEDADES
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New Novedad
                    descuento.id = row.Item("novedad_id")
                    descuento.novedad = row.Item("novedad")
                    descuento.nombre = row.Item("nombre")
                    descuento.configuracionAddRotator = row.Item("configuracion_add_rotator")
                    descuento.newsletter = New Newsletter
                    descuento.newsletter.id = row.Item("newsletter_id")
                    resultado.Add(descuento)
                Next
            Else
                query = ConstantesDeDatos.OBTENER_TODAS_LAS_NOVEDADES_POR_USUARIO

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@usuario_id"
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                param.tamanio = 18
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New Novedad
                    descuento.id = row.Item("novedad_id")
                    descuento.novedad = row.Item("novedad")
                    descuento.nombre = row.Item("nombre")
                    descuento.configuracionAddRotator = row.Item("configuracion_add_rotator")
                    descuento.newsletter = New Newsletter
                    descuento.newsletter.id = row.Item("newsletter_id")
                    resultado.Add(descuento)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Novedad
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As New List(Of Novedad)
        Dim descuento As Novedad = Nothing
        Dim dset As New DataSet
        Dim dtable As DataTable
        Try
            If Not oObject Is Nothing Then

                query = ConstantesDeDatos.OBTENER_NOVEDAD_POR_NOMBRE

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@nombre"
                param.tipoDeDato = SqlDbType.VarChar
                param.valor = oObject.criterioString
                param.tamanio = 200
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New Novedad
                    descuento.id = row.Item("novedad_id")
                    descuento.novedad = row.Item("novedad")
                    descuento.nombre = row.Item("nombre")
                    descuento.configuracionAddRotator = row.Item("configuracion_add_rotator")
                    descuento.newsletter = New Newsletter
                    descuento.newsletter.id = row.Item("newsletter_id")
                    descuento.titulo = row.Item("titulo")

                    Exit For
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return descuento
    End Function
End Class
