Imports Modelo

Public MustInherit Class AbstractDao(Of T)

    Public MustOverride Function crear(oObject As T) As Boolean
    Public MustOverride Function modificar(oObject As T) As Boolean
    Public MustOverride Function obtenerUno(oObject As CriterioDeBusqueda) As T
    Public MustOverride Function obtenerMuchos(oObject As CriterioDeBusqueda) As List(Of T)

    Public MustOverride Function eliminar(oObject As T) As Boolean

    Public MustOverride Function obtenerProximoId() As Integer

End Class
