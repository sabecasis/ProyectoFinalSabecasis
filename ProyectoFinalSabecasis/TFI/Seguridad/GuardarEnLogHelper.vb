Imports DAL
Imports Modelo

Public Class GuardarEnLogHelper

    Private Shared objeto As New GuardarEnLogHelper

    Public Shared Function instancia() As GuardarEnLogHelper
        Return objeto
    End Function


    Public Sub guardarEnLog(excepcion As String, clase As String, parameters As String, sqlQuery As String, tipoDeExcepcion As TipoDeExcepcion)

        Dim parametros As New List(Of DbDto)
        Dim param As New DbDto
        param.esParametroDeSalida = False
        param.parametro = "@excepcion"
        param.tamanio = -1
        param.tipoDeDato = SqlDbType.VarChar
        param.valor = excepcion
        parametros.Add(param)

        param = New DbDto
        param.valor = clase
        param.tipoDeDato = SqlDbType.VarChar
        param.parametro = "@clase"
        param.tamanio = 300
        param.esParametroDeSalida = False
        parametros.Add(param)

        param = New DbDto
        param.valor = sqlQuery
        param.tipoDeDato = SqlDbType.VarChar
        param.tamanio = 300
        param.parametro = "@query"
        param.esParametroDeSalida = False
        parametros.Add(param)

        param = New DbDto
        param.valor = tipoDeExcepcion.id
        param.tipoDeDato = SqlDbType.BigInt
        param.tamanio = 18
        param.parametro = "@tipo_de_excepcion"
        param.esParametroDeSalida = False
        parametros.Add(param)

        ConnectionManager.obtenerInstancia().ejecutarEnDB(parametros, ConstantesDeDatos.GUARDAR_EN_LOG)

    End Sub
End Class
