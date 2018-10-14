Imports Modelo
Imports DAL
Imports Seguridad

Public Class EstadisticaDeVentaDao

    Private Sub New()

    End Sub

    Private Shared objeto As New EstadisticaDeVentaDao

    Public Shared Function instancia() As EstadisticaDeVentaDao
        Return objeto
    End Function

    Public Function obtenerEstadisticasDiarias(anio As String, mes As Integer, nroSucursal As Integer) As List(Of EstadisticaDeVenta)
        Dim estadisticas As New List(Of EstadisticaDeVenta)
        Dim estadisticaDeVenta As EstadisticaDeVenta
        Dim dset As DataSet
        Dim dtable As DataTable
        Dim query As String = ConstantesDeDatos.OBTENER_TOTALES_FACTURADOS_POR_DIA
        Dim param As DbDto
        Dim parametros As New List(Of DbDto)
        Try
            Dim param1 = New DbDto
            param1.esParametroDeSalida = False
            param1.parametro = "@nro_sucursal"
            param1.tipoDeDato = SqlDbType.BigInt
            param1.tamanio = 18
            param1.valor = nroSucursal
            parametros.Add(param1)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@anio"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 4
            param.valor = anio
            parametros.Add(param)

            Dim param2 = New DbDto
            param2.esParametroDeSalida = False
            param2.parametro = "@mes"
            param2.tipoDeDato = SqlDbType.Int
            param2.valor = mes
            parametros.Add(param2)

            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                estadisticaDeVenta = New EstadisticaDeVenta
                estadisticaDeVenta.concepto = row.Item("dia")
                If Not IsDBNull(row.Item("total")) Then
                    estadisticaDeVenta.total = row.Item("total")
                Else
                    estadisticaDeVenta.total = 0
                End If
                estadisticas.Add(estadisticaDeVenta)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return estadisticas
    End Function
    Public Function obtenerEstadisticasDeVentaMensual(anio As String, nroSucursal As Integer) As List(Of EstadisticaDeVenta)
        Dim estadisticas As New List(Of EstadisticaDeVenta)
        Dim estadisticaDeVenta As EstadisticaDeVenta
        Dim dset As DataSet
        Dim dtable As DataTable
        Dim query As String = ConstantesDeDatos.OBTENER_TOTAL_FACTURADO_MENSUAL
        Dim param As DbDto
        Dim parametros As New List(Of DbDto)
        Try
            Dim param1 = New DbDto
            param1.esParametroDeSalida = False
            param1.parametro = "@nro_sucursal"
            param1.tipoDeDato = SqlDbType.BigInt
            param1.tamanio = 18
            param1.valor = nroSucursal
            parametros.Add(param1)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@anio"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 4
            param.valor = anio
            parametros.Add(param)

            Dim param2 = New DbDto
            param2.esParametroDeSalida = False
            param2.parametro = "@mes"
            param2.tipoDeDato = SqlDbType.VarChar
            param2.tamanio = 2
            parametros.Add(param2)

            For i As Integer = 1 To 12
                param2.valor = i.ToString()
                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable

                    estadisticaDeVenta = New EstadisticaDeVenta
                    estadisticaDeVenta.concepto = i.ToString
                    If Not IsDBNull(row.Item("total")) Then
                        estadisticaDeVenta.total = row.Item("total")
                    Else
                        estadisticaDeVenta.total = 0
                    End If
                    estadisticas.Add(estadisticaDeVenta)

                    Exit For
                Next
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return estadisticas
    End Function

    Public Function obtenerEstadisticasDeVentaAnual(nroSucursal As Integer) As List(Of EstadisticaDeVenta)
        Dim estadisticas As New List(Of EstadisticaDeVenta)
        Dim estadisticaDeVenta As EstadisticaDeVenta
        Dim dset As DataSet
        Dim dtable As DataTable
        Dim query As String = ConstantesDeDatos.OBTENER_TOTAL_FACTURADO_ANUAL
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            Dim param2 = New DbDto
            param2.esParametroDeSalida = False
            param2.parametro = "@nro_sucursal"
            param2.tipoDeDato = SqlDbType.BigInt
            param2.tamanio = 18
            param2.valor = nroSucursal
            parametros.Add(param2)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@anio"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 4
            parametros.Add(param)
            For i As Integer = 2015 To Date.Now.Year
                param.valor = i.ToString

                dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                dtable = dset.Tables(0)
                For Each row As DataRow In dtable.AsEnumerable
                    estadisticaDeVenta = New EstadisticaDeVenta
                    estadisticaDeVenta.concepto = i
                    If Not IsDBNull(row.Item("total")) Then
                        estadisticaDeVenta.total = row.Item("total")
                    Else
                        estadisticaDeVenta.total = 0
                    End If
                    estadisticas.Add(estadisticaDeVenta)
                    Exit For
                Next

            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return estadisticas
    End Function
End Class
