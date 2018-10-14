Imports Modelo
Imports Seguridad
Imports Datos

Public Class GestorIdioma

    Private Shared objeto As New GestorIdioma
    Private Sub New()

    End Sub

    Public Shared Function instancia() As GestorIdioma
        Return objeto
    End Function

    Public Function obtenerElementosTraducidos(leyendas As List(Of String), idioma As Integer) As List(Of ElementoDeIdioma)
        Try
            Dim criterio As New CriterioDeBusquedaElemento
            Dim elemento As ElementoDeIdioma = Nothing
            Dim resultado As New List(Of ElementoDeIdioma)
            For Each id As String In leyendas
                criterio.criterioString = id
                criterio.criterioEntero = idioma
                elemento = ElementoDeIdiomaDao.instancia().obtenerUno(criterio)
                If Not elemento Is Nothing Then
                    resultado.Add(elemento)
                End If
            Next
            Return resultado
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function
End Class
