Imports Modelo
Imports DAL
Imports Seguridad

Public Class PaisDao
    Inherits AbstractDao(Of Pais)

    Private Shared objeto As New PaisDao

    Private Sub New()

    End Sub


    Public Shared Function instancia() As PaisDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Pais) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Pais) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Pais) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Pais)
        Dim paises As New List(Of Pais)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            If oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_PAISES
                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)
                Dim oPais As Pais
                For Each row In dataTable.AsEnumerable
                    oPais = New Pais()
                    oPais.id = row.Item(0)
                    oPais.pais = row.Item(1)
                    Dim criterio As New CriterioDeBusqueda
                    criterio.criterioEntero = oPais.id
                    'oPais.provincias = ProvinciaDao.instancia().obtenerMuchos(criterio)
                    paises.Add(oPais)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return paises
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Pais
        Return Nothing
    End Function
End Class
