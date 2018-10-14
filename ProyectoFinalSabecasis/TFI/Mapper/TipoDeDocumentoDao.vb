Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeDocumentoDao
    Inherits AbstractDao(Of TipoDeDocumento)

    Private Shared objeto As New TipoDeDocumentoDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As TipoDeDocumentoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As TipoDeDocumento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As TipoDeDocumento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As TipoDeDocumento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeDocumento)
        Dim tiposDoc As New List(Of TipoDeDocumento)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_TIPOS_DE_DOCUMENTO

                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim tabla As DataTable = dataSet.Tables(0)
                Dim oTipoDoc As TipoDeDocumento
                For Each row In tabla.AsEnumerable
                    oTipoDoc = New TipoDeDocumento
                    oTipoDoc.id = row.Item(0)
                    oTipoDoc.tipo = row.Item(1)
                    tiposDoc.Add(oTipoDoc)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return tiposDoc
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeDocumento
        Return Nothing
    End Function
End Class
