Imports Modelo
Imports Seguridad
Imports DAL

Public Class ImpuestoDao
    Inherits AbstractDao(Of Impuesto)

    Private Sub New()

    End Sub

    Private Shared _instancia As New ImpuestoDao

    Public Shared Function instancia() As ImpuestoDao
        Return _instancia
    End Function

    Public Overrides Function crear(oObject As Impuesto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As Impuesto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Impuesto) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Impuesto)
        Dim impuestos As New List(Of Impuesto)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Dim criterioActual As CriterioDeBusquedaDeImpuestos = oObject
        Try
            If Not oObject Is Nothing Then
                If criterioActual.esNotaDeCredito Then
                    query = ConstantesDeDatos.OBTENER_IMPUESTOS_POR_NOTA_DE_CREDITO

                    Dim param As New DbDto
                    param.esParametroDeSalida = False
                    param.parametro = "@nro_nota_de_credito"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    param.valor = oObject.criterioEntero
                    parametros.Add(param)

                    Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    Dim dataTable As DataTable = dataSet.Tables(0)
                    Dim oImpuesto As Impuesto
                    For Each row In dataTable.AsEnumerable
                        oImpuesto = New Impuesto()
                        oImpuesto.id = row.Item("impuesto_id")
                        oImpuesto.nombre = row.Item("nombre")
                        oImpuesto.porcentaje = row.Item("porcentaje")
                        impuestos.Add(oImpuesto)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return impuestos
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Impuesto
        Return Nothing
    End Function
End Class
