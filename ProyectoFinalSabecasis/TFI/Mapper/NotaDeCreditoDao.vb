Imports Modelo
Imports Seguridad
Imports DAL

Public Class NotaDeCreditoDao
    Inherits AbstractDao(Of NotaDeCredito)
    Private Sub New()

    End Sub

    Private Shared objeto As New NotaDeCreditoDao

    Public Shared Function instancia() As NotaDeCreditoDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As NotaDeCredito) As Boolean
        Dim query As String = ConstantesDeDatos.CREAR_NOTA_DE_CREDITO
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            Dim param1 = New DbDto
            param1.esParametroDeSalida = True
            param1.parametro = "@nro_nota_de_credito"
            param1.tamanio = 18
            param1.tipoDeDato = SqlDbType.BigInt
            param1.valor = DBNull.Value
            parametros.Add(param1)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@descripcion"
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.descripcion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@monto"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.monto.ToString("R").Replace(",", ".")
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@nro_de_factura"
            param.tamanio = 18
            param.tipoDeDato = SqlDbType.BigInt
            param.valor = oObject.factura.nroFactura
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@comprobante"
            param.tipoDeDato = SqlDbType.Binary
            param.tamanio = -1
            If oObject.comprobante Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.comprobante
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha_emision"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            If oObject.fechaEmision <= Date.Now Then
                param.valor = Date.Now.ToString("MM-dd-yyyy")
            Else
                param.valor = oObject.fechaEmision.ToString("MM-dd-yyyy")
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha_caducidad"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            If oObject.fechaCaducidad <= Date.Now Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.fechaCaducidad.ToString("MM-dd-yyyy")
            End If
            parametros.Add(param)


            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)


            oObject.nroNotaDeCredito = param1.valor


        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result
    End Function

    Public Overrides Function eliminar(oObject As NotaDeCredito) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As NotaDeCredito) As Boolean
        Dim query As String = ConstantesDeDatos.MODIFICAR_NOTA_DE_CREDITO
        Dim result As Boolean = False
        Dim parametros As New List(Of DbDto)
        Dim param As DbDto
        Try
            Dim param1 = New DbDto
            param1.esParametroDeSalida = False
            param1.parametro = "@nro_nota_de_credito"
            param1.tamanio = 18
            param1.tipoDeDato = SqlDbType.BigInt
            param1.valor = DBNull.Value
            parametros.Add(param1)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@descripcion"
            param.tamanio = -1
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.descripcion
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@monto"
            param.tamanio = 20
            param.tipoDeDato = SqlDbType.VarChar
            param.valor = oObject.monto.ToString("R").Replace(",", ".")
            parametros.Add(param)


            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@fecha_caducidad"
            param.tipoDeDato = SqlDbType.VarChar
            param.tamanio = 20
            If oObject.fechaCaducidad < Date.Now Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.fechaCaducidad.ToString("MM-dd-yyyy")
            End If
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@esta_activa"
            param.tipoDeDato = SqlDbType.Bit
            param.valor = oObject.estaActiva
            parametros.Add(param)

            param = New DbDto
            param.esParametroDeSalida = False
            param.parametro = "@comprobante"
            param.tipoDeDato = SqlDbType.Binary
            param.tamanio = -1
            If oObject.comprobante Is Nothing Then
                param.valor = DBNull.Value
            Else
                param.valor = oObject.comprobante
            End If
            parametros.Add(param)

            result = ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, query)
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return result

    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of NotaDeCredito)
        Dim notas As New List(Of NotaDeCredito)
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_NOTAS_DE_CREDITO_ACTIVAS_DE_USUARIO

                Dim param As New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@usuario_id"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                parametros.Add(param)

                param = New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@esta_activa"
                param.tipoDeDato = SqlDbType.Bit
                param.valor = oObject.criterioBoolean
                parametros.Add(param)

                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)
                Dim oNota As NotaDeCredito
                For Each row In dataTable.AsEnumerable
                    oNota = New NotaDeCredito()
                    If Not IsDBNull(row.Item("comprobante")) Then
                        oNota.comprobante = row.Item("comprobante")
                    End If
                    oNota.descripcion = row.Item("descripcion")
                    oNota.factura = New Factura
                    oNota.factura.nroFactura = row.Item("nro_de_factura")
                    oNota.factura.tipoDeFactura = New TipoDeFactura
                    oNota.factura.tipoDeFactura.id = row.Item("tipo_de_factura_id")
                    oNota.monto = row.Item("monto")
                    oNota.nroNotaDeCredito = row.Item("nro_nota_de_credito")
                    If IsDBNull(row.Item("fecha_caducidad")) Then
                        oNota.fechaCaducidad = New Date()
                    Else
                        oNota.fechaCaducidad = row.Item("fecha_caducidad")
                    End If

                    oNota.fechaEmision = row.Item("fecha_emision")
                    oNota.sucursal = New Sucursal
                    oNota.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                    oNota.estaActiva = row.Item("esta_activa")
                    Dim criterio As New CriterioDeBusquedaDeImpuestos
                    criterio.criterioEntero = oNota.nroNotaDeCredito
                    criterio.esNotaDeCredito = True
                    oNota.impuestos = ImpuestoDao.instancia().obtenerMuchos(criterio)
                    notas.Add(oNota)
                Next
            Else
                query = ConstantesDeDatos.OBTENER_NOTAS_DE_CREDITO



                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)
                Dim oNota As NotaDeCredito
                For Each row In dataTable.AsEnumerable
                    oNota = New NotaDeCredito()
                    If Not IsDBNull(row.Item("comprobante")) Then
                        oNota.comprobante = row.Item("comprobante")
                    End If
                    oNota.descripcion = row.Item("descripcion")
                    oNota.factura = New Factura
                    oNota.factura.nroFactura = row.Item("nro_de_factura")
                    oNota.factura.tipoDeFactura = New TipoDeFactura
                    oNota.factura.tipoDeFactura.id = row.Item("tipo_de_factura_id")
                    oNota.monto = row.Item("monto")
                    oNota.nroNotaDeCredito = row.Item("nro_nota_de_credito")
                    If IsDBNull(row.Item("fecha_caducidad")) Then
                        oNota.fechaCaducidad = New Date()
                    Else
                        oNota.fechaCaducidad = row.Item("fecha_caducidad")
                    End If

                    oNota.fechaEmision = row.Item("fecha_emision")
                    oNota.sucursal = New Sucursal
                    oNota.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                    oNota.estaActiva = row.Item("esta_activa")
                    Dim criterio As New CriterioDeBusquedaDeImpuestos
                    criterio.criterioEntero = oNota.nroNotaDeCredito
                    criterio.esNotaDeCredito = True
                    oNota.impuestos = ImpuestoDao.instancia().obtenerMuchos(criterio)
                    notas.Add(oNota)
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return notas
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return ConnectionManager.obtenerInstancia().obtenerProximoId(ConstantesDeDatos.TABLA_NOTA_DE_CREDITO)
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As NotaDeCredito
        Dim oNota As NotaDeCredito = Nothing
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not oObject Is Nothing Then
                query = ConstantesDeDatos.OBTENER_NOTA_DE_CREDITO_POR_ID
                Dim param As New DbDto
                param.esParametroDeSalida = False
                param.parametro = "@id"
                param.tamanio = 18
                param.tipoDeDato = SqlDbType.BigInt
                param.valor = oObject.criterioEntero
                parametros.Add(param)

                Dim dataSet As DataSet = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                Dim dataTable As DataTable = dataSet.Tables(0)

                For Each row In dataTable.AsEnumerable
                    oNota = New NotaDeCredito()
                    If Not IsDBNull(row.Item("comprobante")) Then
                        oNota.comprobante = row.Item("comprobante")
                    End If
                    oNota.descripcion = row.Item("descripcion")
                    oNota.factura = New Factura
                    oNota.factura.nroFactura = row.Item("nro_de_factura")
                    oNota.factura.tipoDeFactura = New TipoDeFactura
                    oNota.factura.tipoDeFactura.id = row.Item("tipo_de_factura_id")
                    oNota.monto = row.Item("monto")
                    oNota.nroNotaDeCredito = row.Item("nro_nota_de_credito")
                    If IsDBNull(row.Item("fecha_caducidad")) Then
                        oNota.fechaCaducidad = New Date()
                    Else
                        oNota.fechaCaducidad = row.Item("fecha_caducidad")
                    End If
                    oNota.fechaEmision = row.Item("fecha_emision")
                    oNota.sucursal = New Sucursal
                    oNota.sucursal.nroSucursal = row.Item("nro_de_sucursal")
                    oNota.estaActiva = row.Item("esta_activa")
                    Dim criterio As New CriterioDeBusquedaDeImpuestos
                    criterio.esNotaDeCredito = True
                    criterio.criterioEntero = oObject.criterioEntero
                    oNota.impuestos = ImpuestoDao.instancia().obtenerMuchos(criterio)
                    Exit For
                Next
            End If
        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oNota
    End Function
End Class
