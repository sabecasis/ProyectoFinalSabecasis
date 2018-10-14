Imports Modelo
Imports DAL
Imports Seguridad

Public Class ProvinciaDao
    Inherits AbstractDao(Of Provincia)

    Private Shared objeto As New ProvinciaDao()
    Private Sub New()

    End Sub

    Public Shared Function instancia() As ProvinciaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Provincia) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Provincia) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Provincia) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Provincia)
        Dim provincias As New List(Of Provincia)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            If Not oObject Is Nothing Then
                If oObject.criterioEntero <> 0 Then
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@id_pais"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_PROVINCIAS_POR_PAIS
                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim dataTable As DataTable = dataSet.Tables(0)
                    Dim oProvincia As Provincia
                    For Each row In dataTable.AsEnumerable
                        oProvincia = New Provincia()
                        oProvincia.id = row.Item(0)
                        oProvincia.provincia = row.Item(1)
                        Dim criterio As New CriterioDeBusqueda
                        criterio.criterioEntero = oProvincia.id
                        'oProvincia.localidades = LocalidadDao.instancia().obtenerMuchos(criterio)
                        provincias.Add(oProvincia)
                    Next
                End If
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return provincias
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Provincia
        Dim oProvincia As Provincia = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try

            If Not oObject Is Nothing Then
                If oObject.criterioEntero <> 0 Then
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@id"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_PROVINCIA_POR_ID
                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim dataTable As DataTable = dataSet.Tables(0)

                    For Each row In dataTable.AsEnumerable
                        oProvincia = New Provincia()
                        oProvincia.id = row.Item("provincia_id")
                        oProvincia.provincia = row.Item("provincia")
                        oProvincia.pais = New Pais
                        oProvincia.pais.id = row.Item("pais_id")
                        oProvincia.pais.pais = row.Item("pais")

                        Exit For
                    Next
                End If
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oProvincia
    End Function
End Class
