Imports Modelo
Imports DAL
Imports Seguridad

Public Class LocalidadDao
    Inherits AbstractDao(Of Localidad)

    Private Shared objeto As New LocalidadDao
    Private Sub New()

    End Sub

    Public Shared Function instancia() As LocalidadDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Localidad) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Localidad) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Localidad) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Localidad)
        Dim localidades As New List(Of Localidad)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try

            If Not oObject Is Nothing Then
                If oObject.criterioEntero <> 0 Then
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@id_provincia"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_LOCALIDADES_POR_PROVINCIA
                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim dataTable As DataTable = dataSet.Tables(0)
                    Dim oLocalidad As Localidad
                    For Each row In dataTable.AsEnumerable
                        oLocalidad = New Localidad()
                        oLocalidad.id = row.Item(0)
                        oLocalidad.localidad = row.Item(1)
                        oLocalidad.cp = row.Item(2)
                        localidades.Add(oLocalidad)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return localidades
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Localidad
        Dim oLocalidad As Localidad = Nothing
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Try

            If Not oObject Is Nothing Then
                If oObject.criterioEntero <> 0 Then
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@id_localidad"
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.valor = oObject.criterioEntero
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_LOCALIDAD_POR_ID

                Else
                    Dim dto As New DbDto
                    dto.esParametroDeSalida = False
                    dto.parametro = "@cp"
                    dto.tipoDeDato = SqlDbType.VarChar
                    dto.valor = oObject.criterioString
                    parametros.Add(dto)

                    query = ConstantesDeDatos.OBTENER_LOCALIDAD_POR_CP
                End If

                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)

                For Each row In dataTable.AsEnumerable
                    oLocalidad = New Localidad()
                    oLocalidad.id = row.Item("localidad_id")
                    oLocalidad.localidad = row.Item("localidad")
                    oLocalidad.cp = row.Item("cp")
                    oLocalidad.provincia = New Provincia()
                    oLocalidad.provincia.id = row.Item("provincia_id")
                    Exit For
                Next

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oLocalidad
    End Function
End Class
