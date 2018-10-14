Imports Modelo
Imports DAL
Imports Seguridad

Public Class ContactoDao
    Inherits AbstractDao(Of Contacto)

    Private Sub New()

    End Sub

    Private Shared objeto As New ContactoDao

    Public Shared Function instancia() As ContactoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Contacto) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_CONTACTO_DE_ENVIO
        Dim parametros As New List(Of DbDto)
        Dim parametro As DbDto
        Dim result As Boolean = False
        Try
            Dim param As New DbDto
            param.esParametroDeSalida = True
            param.parametro = "@contacto_id"
            param.tipoDeDato = SqlDbType.BigInt
            param.tamanio = 18
            param.valor = 0
            parametros.Add(param)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@calle"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.tamanio = 100
            If oObject.calle Is Nothing Then
                parametro.valor = DBNull.Value
            Else
                parametro.valor = oObject.calle
            End If

            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@nro_puerta"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.tamanio = 10
            parametro.valor = oObject.numero.ToString()
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@piso"
            parametro.tipoDeDato = SqlDbType.Int
            parametro.valor = oObject.piso
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@departamento"
            parametro.tipoDeDato = SqlDbType.VarChar
            parametro.tamanio = 4
            If Not oObject.departamento Is Nothing Then
                parametro.valor = oObject.departamento
            Else
                parametro.valor = DBNull.Value
            End If
            parametros.Add(parametro)

            parametro = New DbDto
            parametro.esParametroDeSalida = False
            parametro.parametro = "@localidad_id"
            parametro.tipoDeDato = SqlDbType.BigInt
            parametro.valor = oObject.localidad.id
            parametro.tamanio = 18
            parametros.Add(parametro)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            If result Then
                oObject.id = param.valor
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As Contacto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Contacto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Contacto)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Contacto
        Return Nothing
    End Function
End Class
