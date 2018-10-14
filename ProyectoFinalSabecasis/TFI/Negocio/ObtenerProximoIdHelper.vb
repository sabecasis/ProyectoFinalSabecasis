Imports Mapper
Imports Modelo
Imports Seguridad
Imports Datos

Public Class ObtenerProximoIdHelper

    Private Shared objeto As New ObtenerProximoIdHelper

    Private Sub New()
    End Sub

    Public Shared Function instancia() As ObtenerProximoIdHelper
        Return objeto
    End Function

    Public Function obtenerProximoIdPreguntaencuesta() As Integer
        Return PreguntaDeEncuestaDao.instancia().obtenerProximoId()
    End Function

    Public Function obtenerProximoIdEncuesta() As Integer
        Return EncuestaDao.instancia().obtenerProximoId()
    End Function
    Public Function obtenerProximoIdSucursal() As Integer
        Return SucursalDao.instancia().obtenerProximoId()
    End Function
    Public Function obtenerProximoIdProducto() As Integer
        Dim dao As AbstractDao(Of Producto) = ProductoDao.instancia()
        Return dao.obtenerProximoId()
    End Function

    Public Function obtenerProximoIdElemento() As Integer
        Try
            Dim dao As AbstractDao(Of Elemento) = ElementoDao.instancia()
            Return dao.obtenerProximoId()
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerProximoIdPermiso() As Integer
        Try
            Dim dao As AbstractDao(Of Permiso) = PermisoDao.instancia()
            Return dao.obtenerProximoId()
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerProximoIdRol() As Integer
        Try
            Dim dao As AbstractDao(Of Rol) = RolDao.instancia()
            Return dao.obtenerProximoId()
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerProximoIdUsuario() As Integer
        Try
            Dim dao As AbstractDao(Of Usuario) = UsuarioDao.instancia()
            Return dao.obtenerProximoId()
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerProximoIdSolicitudStock() As Integer
        Return SolicitudDeStockDao.instancia().obtenerProximoId()
    End Function
    Public Function obtenerProximoIdFamiliaDeProducto() As Integer
        Try
            Dim dao As AbstractDao(Of FamiliaDeProducto) = FamiliaDeProductoDao.instancia()
            Return dao.obtenerProximoId()
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

    Public Function obtenerProximoIdEgresoDeStock() As Integer
        Return EgresoDeStockDao.instancia().obtenerProximoId()
    End Function

    Public Function obtenerProximoIdIngresoStock() As Integer
        Return IngresoDeStockDao.instancia().obtenerProximoId()
    End Function

    Public Function obtenerProximoIdTipoDeGarantia() As Integer
        Try
            Dim dao As AbstractDao(Of TipoDeGarantia) = TipoDeGarantiaDao.instancia()
            Return dao.obtenerProximoId()
        Catch exe As ExcepcionDeDatos
            Throw exe
        Catch ex As Exception
            Throw New ExcepcionDeNegocios(ex, Me.GetType.Name)
        End Try
    End Function

End Class
