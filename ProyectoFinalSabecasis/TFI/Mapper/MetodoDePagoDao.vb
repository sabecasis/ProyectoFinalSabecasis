Imports Modelo
Imports DAL
Imports Seguridad

Public Class MetodoDePagoDao
    Inherits AbstractDao(Of MetodoDePago)

    Private Sub New()

    End Sub

    Private Shared objeto As New MetodoDePagoDao

    Public Shared Function instancia() As MetodoDePagoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As MetodoDePago) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As MetodoDePago) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As MetodoDePago) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of MetodoDePago)
        Dim query As String = ConstantesDeDatos.OBTENER_TODOS_LOS_METODOS_DE_PAGO
        Dim resultado As New List(Of MetodoDePago)
        Try
            Dim dset As DataSet
            Dim dtable As DataTable
           

            dset = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
                dtable = dset.Tables(0)
                Dim res As MetodoDePago
                For Each row As DataRow In dtable.AsEnumerable
                    res = New MetodoDePago
                    res.id = row.Item("metodo_de_pago_id")
                    res.metodo = row.Item("metodo_de_pago")
                    resultado.Add(res)
                Next
            Catch ex As Exception
                Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
            End Try
            Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As MetodoDePago
        If oObject.criterioEntero <> 0 Then
            Dim query As String = ConstantesDeDatos.OBTENER_REGEX_POR_METODO_DE_PAGO
            Dim resultado As New MetodoDePago
            Try
                Dim dset As DataSet
                Dim dtable As DataTable

                Dim parametros As New List(Of DbDto)
                Dim param As New DbDto
                param.esParametroDeSalida = True
                param.parametro = "@metodo_de_pago_id"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                parametros.Add(param)

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                Dim res As MetodoDePago = Nothing
                For Each row As DataRow In dtable.AsEnumerable
                    res = New MetodoDePago
                    res.id = oObject.criterioEntero
                    If IsDBNull(row.Item("regex")) Then
                        res.regex = Nothing
                    Else
                        res.regex = row.Item("regex")
                    End If

                    Exit For
                Next

                If Not res Is Nothing Then
                    query = ConstantesDeDatos.OBTENER_CUOTAS
                    dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                    dtable = dset.Tables(0)
                    res.cuotas = New List(Of Cuota)
                    For Each row As DataRow In dtable.AsEnumerable
                        Dim cuota As New Cuota
                        cuota.cantidadDeCuotas = row.Item("cantidad_de_cuotas")
                        cuota.porcentajeDeRecargo = row.Item("porcentaje_recargo")
                        res.cuotas.Add(cuota)
                    Next
                End If

                Return res
            Catch ex As Exception
                Throw New ExcepcionDeDatos(ex, query, Nothing, Me.GetType.Name)
            End Try
        End If
        Return Nothing
    End Function
End Class
