Imports Modelo
Imports DAL
Imports Seguridad

Public Class IdiomaDao
    Inherits AbstractDao(Of Idioma)

    Private Shared objeto As New IdiomaDao
    Private Sub New()

    End Sub

    Public Shared Function instancia() As IdiomaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Idioma) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim parametro As New DbDto()
            parametro.parametro = "@id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.esParametroDeSalida = False
            parametro.valor = oObject.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.nombre
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@descripcion"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.descripcion
            parametros.Add(parametro)

            query = ConstantesDeDatos.CREAR_IDIOMA
            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            For Each oElemento In oObject.elementos
                parametros = New List(Of DbDto)
                parametro = New DbDto()
                parametro.parametro = "@idioma_id"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.esParametroDeSalida = False
                parametro.valor = oObject.id
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@texto"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = oElemento.texto
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@elemento_id"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = oElemento.elemento.id
                parametros.Add(parametro)
                query = ConstantesDeDatos.CREAR_ELEMENTO_DE_IDIOMA
                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Idioma) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim parametro As New DbDto()
            parametro.parametro = "@id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.esParametroDeSalida = False
            parametro.valor = oObject.id
            parametros.Add(parametro)

            query = ConstantesDeDatos.ELIMINAR_IDIOMA
            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As Idioma) As Boolean
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            Dim parametro As New DbDto()
            parametro.parametro = "@id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.esParametroDeSalida = False
            parametro.valor = oObject.id
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nombre"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.nombre
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@descripcion"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.valor = oObject.descripcion
            parametros.Add(parametro)

            query = ConstantesDeDatos.MODIFICAR_IDIOMA
            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            parametros = New List(Of DbDto)
            parametro = New DbDto()
            parametro.parametro = "@idioma_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.esParametroDeSalida = False
            parametro.valor = oObject.id
            parametros.Add(parametro)


            For Each oElemento In oObject.elementos
                parametros = New List(Of DbDto)
                parametro = New DbDto()
                parametro.parametro = "@idioma_id"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.esParametroDeSalida = False
                parametro.valor = oObject.id
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@texto"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = oElemento.texto
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@elemento_id"
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.valor = oElemento.elemento.id
                parametros.Add(parametro)
                query = ConstantesDeDatos.CREAR_ELEMENTO_DE_IDIOMA
                ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Next

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Idioma)
        Dim resultado As New List(Of Idioma)
        Dim data As DataSet
        Dim tabla As DataTable
        Dim oIdioma As Idioma
        Dim query As String = ""
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_IDIOMAS
                data = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
                tabla = data.Tables(0)
                For Each row As DataRow In tabla.AsEnumerable
                    oIdioma = New Idioma
                    oIdioma.id = row.Item("idioma_id")
                    oIdioma.descripcion = row.Item("descripcion")
                    oIdioma.nombre = row.Item("idioma")
                    resultado.Add(oIdioma)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Idioma
        Dim idioma As Idioma = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            Dim data As DataSet
            Dim tabla As DataTable
            If Not oObject Is Nothing Then

                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@id"
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.valor = oObject.criterioEntero
                parametros.Add(parametro)

                query = ConstantesDeDatos.OBTENER_IDIOMA_POR_ID
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                tabla = data.Tables(0)

                For Each row In tabla.AsEnumerable
                    idioma = New Idioma
                    idioma.id = row.Item(0)
                    idioma.nombre = row.Item(1)
                    idioma.descripcion = row.Item(2)
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return idioma
    End Function
End Class
