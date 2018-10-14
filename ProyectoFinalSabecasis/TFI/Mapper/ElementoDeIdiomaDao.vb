Imports Modelo
Imports DAL
Imports Seguridad

Public Class ElementoDeIdiomaDao
    Inherits AbstractDao(Of ElementoDeIdioma)

    Private Shared objeto As New ElementoDeIdiomaDao

    Private Sub New()

    End Sub

    Public Shared Function instancia() As ElementoDeIdiomaDao
        Return objeto
    End Function

    Public Overrides Function crear(oObject As ElementoDeIdioma) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As ElementoDeIdioma) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As ElementoDeIdioma) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ElementoDeIdioma)
        Dim criterio As CriterioDeBusquedaElemento = oObject
        Dim resultado As New List(Of ElementoDeIdioma)
        Dim data As DataSet
        Dim tabla As DataTable
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not criterio Is Nothing Then

                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.Int
                parametro.parametro = "@idioma_id"
                parametro.valor = criterio.criterioEntero
                parametros.Add(parametro)

                query = ConstantesDeDatos.OBTENER_ELEMENTOS_DE_IDIOMA
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)

                tabla = data.Tables(0)
                Dim oElemento As ElementoDeIdioma
                For Each row In tabla.AsEnumerable
                    oElemento = New ElementoDeIdioma
                    oElemento.idioma = New Idioma
                    oElemento.idioma.id = row.Item("idioma_id")
                    oElemento.elemento = New Elemento
                    oElemento.elemento.id = row.Item("elemento_id")
                    oElemento.elemento.nombre = row.Item("nombre_elemento")
                    oElemento.texto = row.Item("texto")
                    resultado.Add(oElemento)
                Next
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return resultado
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ElementoDeIdioma
        Dim criterio As CriterioDeBusquedaElemento = oObject
        Dim oElemento As ElementoDeIdioma = Nothing
        Dim data As DataSet
        Dim tabla As DataTable
        Dim parametros As New List(Of DbDto)
        Dim query As String = ""
        Try
            If Not criterio Is Nothing Then

                Dim parametro As New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.VarChar
                parametro.parametro = "@elemento"
                If Not oObject.criterioString Is Nothing Then
                    parametro.valor = criterio.criterioString
                Else
                    parametro.valor = DBNull.Value
                End If

                parametros.Add(parametro)

                parametro = New DbDto
                parametro.esParametroDeSalida = False
                parametro.tipoDeDato = SqlDbType.BigInt
                parametro.parametro = "@idioma_id"
                parametro.valor = criterio.criterioEntero
                parametros.Add(parametro)

                query = ConstantesDeDatos.OBTENER_ELEMENTO_DE_IDIOMA
                data = ConnectionManager.obtenerInstancia().leerBD(parametros, query)
                If data.Tables.Count > 0 Then
                    tabla = data.Tables(0)

                    For Each row In tabla.AsEnumerable
                        oElemento = New ElementoDeIdioma
                        oElemento.idioma = New Idioma
                        oElemento.idioma.id = row.Item("idioma_id")
                        oElemento.elemento = New Elemento
                        oElemento.elemento.id = row.Item("elemento_id")
                        oElemento.elemento.nombre = row.Item("nombre_elemento")
                        oElemento.texto = row.Item("texto")
                        Exit For
                    Next
                End If
            End If

        Catch ex As Exception
            Throw New ExcepcionDeDatos(ex, query, parametros, Me.GetType.Name)
        End Try
        Return oElemento
    End Function
End Class
