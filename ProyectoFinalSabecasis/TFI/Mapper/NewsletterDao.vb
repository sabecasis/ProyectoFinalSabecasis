Imports Modelo
Imports DAL
Imports Seguridad

Public Class NewsletterDao
    Inherits AbstractDao(Of Newsletter)
    Private Sub New()

    End Sub

    Private Shared objeto As New NewsletterDao

    Public Shared Function instancia() As NewsletterDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Newsletter) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_NEWSLETTER_DE_USUARIO
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.usuario.id
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@newsletter_id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.id
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function eliminar(oObject As Newsletter) As Boolean
        Dim query As String = ConstantesDeDatos.ELIMINAR_TODOS_LOS_NEWSLETTER_DE_USUARIO
        Dim esExitoso As Boolean = False
        Dim parametros As New List(Of DbDto)
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.usuario.id
            parametros.Add(param)

            esExitoso = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return esExitoso
    End Function

    Public Overrides Function modificar(oObject As Newsletter) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Newsletter)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As New List(Of Newsletter)
        Dim descuento As Newsletter
        Dim dset As New DataSet
        Dim dtable As DataTable
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_NEWSLETTER
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New Newsletter
                    descuento.id = row.Item("newsletter_id")
                    descuento.descripcion = row.Item("descripcion")
                    resultado.Add(descuento)
                Next
            Else
                If oObject.criterioBoolean = True Then
                    query = ConstantesDeDatos.OBTENER_NEWSLETTERS_POR_ID
                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.valor = oObject.criterioEntero
                    param.tipoDeDato = SqlDbType.BigInt
                    param.tamanio = 18
                    param.parametro = "@id"
                    parametros.Add(param)
                Else

                    query = ConstantesDeDatos.OBTENER_NEWSLETTERS_DE_USUARIO
                    param = New DbDto
                    param.esParametroDeSalida = False
                    param.valor = oObject.criterioEntero
                    param.tipoDeDato = SqlDbType.BigInt
                    param.tamanio = 18
                    param.parametro = "@id"
                    parametros.Add(param)
                End If
              

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    descuento = New Newsletter
                    descuento.id = row.Item("id_newsletter")
                    descuento.usuario = New Usuario
                    descuento.usuario.id = row.Item("id_usuario")
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

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Newsletter
        Return Nothing
    End Function
End Class
