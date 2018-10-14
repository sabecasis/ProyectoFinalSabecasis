Imports Modelo
Imports DAL
Imports Seguridad

Public Class DescuentoDao
    Inherits AbstractDao(Of Descuento)

    Private Sub New()

    End Sub

    Private Shared objeto As New DescuentoDao

    Public Shared Function instancia() As DescuentoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Descuento) As Boolean
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As Boolean = False
        Try
            query = ConstantesDeDatos.CREAR_DESCUENTO
            param = New DbDto
            param.valor = oObject.id
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.nombre
            param.parametro = "@nombre"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.descripcion
            param.parametro = "@descripcion"
            param.tamanio = 400
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.fechaInicio
            param.parametro = "@fecha_inicio"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            If Not oObject.fechaFin Is Nothing Then
                param.valor = oObject.fechaFin
            Else
                param.valor = DBNull.Value
            End If

            param.parametro = "@fecha_fin"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.monto.ToString("R").Replace(",", ".")
            param.parametro = "@monto"
            param.tamanio = 21
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.porcentaje.ToString("R").Replace(",", ".")
            param.parametro = "@porcentaje"
            param.tamanio = 21
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As Descuento) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As Descuento) As Boolean
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As Boolean = False
        Try
            query = ConstantesDeDatos.MODIFICAR_DESCUENTO
            param = New DbDto
            param.valor = oObject.id
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.nombre
            param.parametro = "@nombre"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.descripcion
            param.parametro = "@descripcion"
            param.tamanio = 400
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.fechaInicio
            param.parametro = "@fecha_inicio"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            If Not oObject.fechaFin Is Nothing Then
                param.valor = oObject.fechaFin
            Else
                param.valor = DBNull.Value
            End If

            param.parametro = "@fecha_fin"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.monto.ToString("R").Replace(",", ".")
            param.parametro = "@monto"
            param.tamanio = 21
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            param = New DbDto
            param.valor = oObject.porcentaje.ToString("R").Replace(",", ".")
            param.parametro = "@porcentaje"
            param.tamanio = 21
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)

            resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Descuento)
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim resultado As New List(Of Descuento)
        Dim descuento As Descuento
        Try
            If Not oObject Is Nothing Then
                If oObject.criterioBoolean = False Then
                    query = ConstantesDeDatos.OBTENER_DESCUENTOS_POR_FACTURA
                    param = New DbDto
                    param.valor = oObject.criterioEntero
                    param.parametro = "@detalle_id"
                    param.tamanio = 18
                    param.tipoDeDato = SqlDbType.BigInt
                    parametros.Add(param)
                End If

            Else
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_DESCUENTOS

            End If

            Dim dset As New DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                descuento = New Descuento
                descuento.id = row.Item("descuento_id")
                descuento.monto = row.Item("monto")
                descuento.porcentaje = row.Item("porcentaje")
                descuento.nombre = row.Item("nombre_descuento")
                descuento.descripcion = row.Item("descripcion_descuento")
                descuento.fechaInicio = row.Item("fecha_de_inicio")
                If Not IsDBNull(row.Item("fecha_de_fin")) Then
                    descuento.fechaFin = row.Item("fecha_de_fin")
                End If
                resultado.Add(descuento)
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_DESCUENTO)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Descuento
        Dim query As String = ""
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Dim descuento As Descuento = Nothing
        Try

            query = ConstantesDeDatos.OBTENER_DESCUENTO
            param = New DbDto
            If oObject.criterioEntero = 0 Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.criterioEntero
            End If
            param.parametro = "@id"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            parametros.Add(param)

            param = New DbDto
            If oObject.criterioString.Equals("") Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.criterioString
            End If

            param.parametro = "@nombre"
            param.tamanio = 200
            param.tipoDeDato = SqlDbType.VarChar
            parametros.Add(param)


            Dim dset As New DataSet
            Dim dtable As DataTable
            dset = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            dtable = dset.Tables(0)
            For Each row As DataRow In dtable.AsEnumerable
                descuento = New Descuento
                descuento.id = row.Item("descuento_id")
                descuento.monto = row.Item("monto")
                descuento.porcentaje = row.Item("porcentaje")
                descuento.nombre = row.Item("nombre_descuento")
                descuento.descripcion = row.Item("descripcion_descuento")
                descuento.fechaInicio = row.Item("fecha_de_inicio")
                If Not IsDBNull(row.Item("fecha_de_fin")) Then
                    descuento.fechaFin = row.Item("fecha_de_fin")
                End If
                Exit For
            Next
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return descuento
    End Function
End Class
