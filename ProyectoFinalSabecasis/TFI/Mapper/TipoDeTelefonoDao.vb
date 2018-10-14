Imports Modelo
Imports DAL
Imports Seguridad

Public Class TipoDeTelefonoDao
    Inherits AbstractDao(Of TipoDeTelefono)

    Private Shared objeto As New TipoDeTelefonoDao
    Private Sub New()

    End Sub

    Public Shared Function instancia() As TipoDeTelefonoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As TipoDeTelefono) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As TipoDeTelefono) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As TipoDeTelefono) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of TipoDeTelefono)
        Dim tiposDeTelefono As New List(Of TipoDeTelefono)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_TIPOS_DE_TELEFONO
                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim tabla As DataTable = dataSet.Tables(0)
                Dim oTipoTel As TipoDeTelefono
                For Each row In tabla.AsEnumerable
                    oTipoTel = New TipoDeTelefono
                    oTipoTel.id = row.Item(0)
                    oTipoTel.tipo = row.Item(1)
                    tiposDeTelefono.Add(oTipoTel)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return tiposDeTelefono
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As TipoDeTelefono
        Return Nothing
    End Function
End Class
