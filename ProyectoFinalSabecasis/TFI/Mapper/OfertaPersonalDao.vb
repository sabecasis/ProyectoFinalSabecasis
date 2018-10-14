Imports Modelo
Imports Seguridad
Imports DAL

Public Class OfertaPersonalDao
    Inherits AbstractDao(Of OfertaPersonal)
    Private Sub New()

    End Sub

    Private Shared objeto As New OfertaPersonalDao

    Public Shared Function instancia() As OfertaPersonalDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As OfertaPersonal) As Boolean
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim query As String = ""
        Try
            If Not oObject.usuario Is Nothing Then
                query = ConstantesDeDatos.GUARDAR_OFERTA_DE_USUARIO
                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.parametro = "@usuario_id"
                parametro.tamanio = 18
                parametro.valor = oObject.usuario.id
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.parametro = "@oferta_id"
                parametro.tamanio = 18
                parametro.valor = oObject.id
                parametros.Add(parametro)

                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Else
                query = ConstantesDeDatos.GUARDAR_OFERTA_PERSONAL
                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.tamanio = 200
                parametro.parametro = "@oferta"
                parametro.valor = oObject.oferta
                parametros.Add(parametro)

                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As OfertaPersonal) As Boolean
        Dim parametros As New List(Of DbDto)
        Dim resultado As Boolean = False
        Dim query As String = ""
        Try
            If Not oObject.usuario Is Nothing Then
                query = ConstantesDeDatos.ELIMINAR_OFERTAS_DE_USUARIO
                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.parametro = "@usuario_id"
                parametro.valor = oObject.usuario.id
                parametros.Add(parametro)

                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            Else
                query = ConstantesDeDatos.ELIMINAR_OFERTAS_PERSONAL
                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.parametro = "@oferta_id"
                parametro.valor = oObject.id
                parametros.Add(parametro)

                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As OfertaPersonal) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of OfertaPersonal)
        Dim resultado As New List(Of OfertaPersonal)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim data As DataSet
        Dim tabla As DataTable
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODAS_LAS_OFERTAS
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                tabla = data.Tables(0)
                Dim oElemento As OfertaPersonal
                For Each row In tabla.AsEnumerable
                    oElemento = New OfertaPersonal
                    oElemento.id = row.Item("oferta_id")
                    oElemento.oferta = row.Item("oferta")
                    resultado.Add(oElemento)
                Next
            Else
                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.parametro = "@usuario_id"
                parametro.valor = oObject.criterioEntero
                parametros.Add(parametro)

                query = ConstantesDeDatos.OBTENER_TODAS_LAS_OFERTAS_POR_USUARIO
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                tabla = data.Tables(0)
                Dim oElemento As OfertaPersonal
                For Each row In tabla.AsEnumerable
                    oElemento = New OfertaPersonal
                    oElemento.id = row.Item("oferta_id")
                    oElemento.oferta = row.Item("oferta")
                    resultado.Add(oElemento)
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As OfertaPersonal
        Return Nothing
    End Function
End Class
