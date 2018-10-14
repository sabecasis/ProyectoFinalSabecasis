Imports Modelo

Public Class ErrorLogDao
    Inherits AbstractDao(Of ErrorLog)

    Private Sub New()

    End Sub

    Private Shared _instancia As New ErrorLogDao

    Public Shared Function instancia() As ErrorLogDao
        Return _instancia
    End Function

    Public Overrides Function crear(oObject As ErrorLog) As Boolean
        Return Nothing
    End Function

    Public Overrides Function eliminar(oObject As ErrorLog) As Boolean
        Return Nothing
    End Function

    Public Overrides Function modificar(oObject As ErrorLog) As Boolean
        Return Nothing
    End Function

    Public Overrides Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of ErrorLog)
        Return Nothing
    End Function

    Public Overrides Function obtenerProximoId() As Integer
        Return Nothing
    End Function

    Public Overrides Function obtenerUno(oObject As CriterioDeBusqueda) As ErrorLog
        Return Nothing
    End Function
End Class
