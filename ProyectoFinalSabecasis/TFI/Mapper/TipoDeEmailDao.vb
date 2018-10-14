Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeEmailDao
    Inherits AbstractDao(Of TipoDeEmail)

    Private Sub New()

    End Sub

    Private Shared _instancia As New TipoDeEmailDao

    Public Shared Function instancia() As TipoDeEmailDao
        Return _instancia
    End Function

    Public Overrides Function crear(oObject As TipoDeEmail) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As TipoDeEmail) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As TipoDeEmail) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeEmail)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeEmail
        Dim oLocalidad As TipoDeEmail = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try

            If Not oObject Is Nothing Then
                If oObject.criterioEntero <> 0 Then
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@id"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_TIPO_DE_EMAIL_POR_ID

                End If


                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)

                For Each row In dataTable.AsEnumerable
                    oLocalidad = New TipoDeEmail()
                    oLocalidad.id = row.Item("tipo_de_email_id")
                    oLocalidad.template = row.Item("template")
                    oLocalidad.tipo = row.Item("tipo")
                    Exit For
                Next

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oLocalidad
    End Function
End Class
