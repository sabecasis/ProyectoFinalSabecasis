Imports Modelo
Imports DAL
Imports Seguridad

Public Class ElementoDao
    Inherits AbstractDao(Of Elemento)

    Private Shared objeto As New ElementoDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As ElementoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As Elemento) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@elemento_id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@nombre"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.nombre
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@leyenda"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.leyendaPorDefecto
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.CREAR_ELEMENTO
                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function eliminar(oObject As Elemento) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@elemento_id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)
                query = ConstantesDeDatos.ELIMINAR_ELEMENTO
                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function modificar(oObject As Elemento) As Boolean
        Dim resultado As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim dto As New DbDto
                dto.parametro = "@elemento_id"
                dto.valor = oObject.id
                dto.tipoDeDato = SqlDbType.BigInt
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@elemento_nombre"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.nombre
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                dto = New DbDto
                dto.parametro = "@leyenda"
                dto.tipoDeDato = SqlDbType.VarChar
                dto.valor = oObject.leyendaPorDefecto
                dto.esParametroDeSalida = False
                parametros.Add(dto)

                query = ConstantesDeDatos.MODIFICAR_ELEMENTO
                resultado = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)

            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of Elemento)
        Dim resultado As New List(Of Elemento)
        Dim criterio As CriterioDeBusquedaElemento = oObject
        Dim data As DataSet
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If criterio Is Nothing Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_ELEMENTOS
                data = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)

            ElseIf criterio.flagTraducible <> 0 And criterio.hasta = 0 Then
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_ELEMENTOS_POR_ESTADO
                data = ConnectionManager.obtenerInstancia().leerBD(Nothing, query)
            Else

                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@desde"
                parametro.tipoDeDato = SqlDbType.Int
                parametro.valor = criterio.desde
                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.parametro = "@hasta"
                parametro.tipoDeDato = SqlDbType.Int
                parametro.valor = criterio.hasta
                parametros.Add(parametro)
                query = ConstantesDeDatos.OBTENER_TODOS_LOS_ELEMENTOS_PAGINADOS
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
            End If
            Dim tabla As DataTable = data.Tables(0)
            Dim oElemento As Elemento
            If Not data Is Nothing Then
                For Each fila As DataRow In tabla.AsEnumerable
                    oElemento = New Elemento
                    oElemento.id = fila.Item("elemento_id")
                    If Not IsDBNull(fila.Item("nombre_elemento")) Then
                        oElemento.nombre = fila.Item("nombre_elemento")
                    Else
                        oElemento.nombre = ""
                    End If

                    If Not IsDBNull(fila.Item("leyenda_por_defecto")) Then
                        oElemento.leyendaPorDefecto = fila.Item("leyenda_por_defecto")
                    Else
                        oElemento.leyendaPorDefecto = ""
                    End If


                    resultado.Add(oElemento)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As Elemento
        Dim resultado As Elemento = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then

                Dim data As DataSet
                If oObject.criterioEntero <> 0 Then

                    Dim dto As New DbDto
                    dto.parametro = "@elemento_id"
                    dto.valor = oObject.criterioEntero
                    dto.tipoDeDato = SqlDbType.BigInt
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)
                    query = ConstantesDeDatos.BUSCAR_ELEMENTO_POR_ID
                    data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Else
                    Dim dto As New DbDto
                    dto.parametro = "@elemento"
                    dto.valor = oObject.criterioString
                    dto.tipoDeDato = SqlDbType.VarChar
                    dto.esParametroDeSalida = False
                    parametros.Add(dto)
                    query = ConstantesDeDatos.BUSCAR_ELEMENTO_POR_NOMBRE
                    data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                End If

                Dim table As DataTable = data.Tables(0)
                For Each row As DataRow In table.AsEnumerable
                    resultado = New Elemento
                    resultado.id = row.Item("elemento_id")
                    If Not IsDBNull(row.Item("nombre_elemento")) Then
                        resultado.nombre = row.Item("nombre_elemento")
                    Else
                        resultado.nombre = ""
                    End If

                    If Not IsDBNull(row.Item("leyenda_por_defecto")) Then
                        resultado.leyendaPorDefecto = row.Item("leyenda_por_defecto")
                    Else
                        resultado.leyendaPorDefecto = ""
                    End If

                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try

        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_ELEMENTO)
    End Function
End Class
