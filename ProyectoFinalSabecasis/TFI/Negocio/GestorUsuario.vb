Imports Modelo
Imports Datos
Imports Seguridad

Public Class GestorUsuario

    Private Sub New()

    End Sub


    Private Shared objeto As New GestorUsuario

    Public Shared Function instancia() As GestorUsuario
        Return objeto
    End Function

    Public Function crearSesionDeChat(sesion As SesionDeChatEnCola) As Boolean
        Dim result As Boolean = True
        result = result And SesionDeChatDao.instancia().crear(sesion.sesion)
        result = result And SesionDeChatEnColaDao.instancia().crear(sesion)
        Return result
    End Function

    Public Function obtenerPreguntasDeEncuestaPorTipo(idTipo As Integer) As List(Of PreguntaDeEncuesta)
        Dim crit As New CriterioDeBusqueda
        crit.criterioEntero = idTipo
        crit.criterioBoolean = False
        Return PreguntaDeEncuestaDao.instancia().obtenerMuchos(crit)
    End Function

    Public Function obtenerTodasLasOfertas() As List(Of OfertaPersonal)
        Return OfertaPersonalDao.instancia().obtenerMuchos(Nothing)
    End Function

    Public Function obtenerTodasLasOfertasDeUsuario(idUsuario As Integer) As List(Of OfertaPersonal)
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = idUsuario
        Return OfertaPersonalDao.instancia().obtenerMuchos(criterio)
    End Function

    Public Function eliminarOfertaPersonal(idOferta As Integer) As String
        Dim oferta As New OfertaPersonal
        oferta.id = idOferta
        Dim resultado As Boolean = OfertaPersonalDao.instancia().eliminar(oferta)
        If resultado Then
            Return ConstantesDeMensaje.ELIMINADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_AL_ELIMINAR
        End If
    End Function

    Public Function guardarOfertaPersonal(oferta As OfertaPersonal) As String
        Dim resultado As Boolean = False
        resultado = OfertaPersonalDao.instancia().crear(oferta)
        If resultado Then
            Return ConstantesDeMensaje.GUARDADO_CON_EXITO
        Else
            Return ConstantesDeMensaje.ERROR_DE_GUARDADO
        End If
    End Function

    Public Function obtenerEstadoDecuenta(idUsuario As Integer) As EstadoDeCuenta
        Dim oEstado As New EstadoDeCuenta
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = idUsuario
        oEstado.notasDeCredito = NotaDeCreditoDao.instancia().obtenerMuchos(criterio)
        oEstado.totalEnCredito = 0
        oEstado.totalEnDebito = 0
        For Each oNotaDeCredito As NotaDeCredito In oEstado.notasDeCredito
            oEstado.totalEnCredito += oNotaDeCredito.monto
        Next
        Dim facturas As List(Of Factura) = FacturaDao.instancia().obtenerMuchos(criterio)
        oEstado.totalFacturado = 0
        For Each oFactura As Factura In facturas
            oEstado.totalFacturado += oFactura.montoDeCobro
        Next
        Return oEstado
    End Function
    Public Function buscarSesionDeChat(idSesionChat As Integer) As SesionDeChatEnCola
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = idSesionChat
        Return SesionDeChatEnColaDao.instancia().obtenerUno(criterio)
    End Function

    Public Function obtenerUltimoMensaje(idSesionChat As Integer) As ComentarioDeChat
        Dim criterio As New CriterioDeBusqueda
        criterio.criterioEntero = idSesionChat
        Return ComentarioDeChatDao.instancia().obtenerUno(criterio)
    End Function

    Public Function finalizarSesionDeChat(idSesion As Integer, idUsuario As Integer) As Boolean
        Dim sesion As New SesionDeChat
        sesion.id = idSesion
        sesion.usuario = New Usuario
        sesion.usuario.id = idUsuario
        sesion.estado = New Estado
        sesion.estado.id = 3
        Return SesionDeChatDao.instancia().modificar(sesion)
    End Function

    Public Function buscarSesionDeChatInactiva(idUsuario As Integer) As SesionDeChatEnCola
        Dim sesion As SesionDeChatEnCola = SesionDeChatEnColaDao.instancia().obtenerUno(Nothing)
        If Not sesion Is Nothing Then
            sesion.sesion.estado = New Estado
            sesion.sesion.estado.id = 2
            sesion.sesion.asesor = New Usuario
            sesion.sesion.asesor.id = idUsuario
            SesionDeChatDao.instancia().modificar(sesion.sesion)
            sesion.estado = New Estado
            sesion.estado.id = 2
            SesionDeChatEnColaDao.instancia().modificar(sesion)
        End If
        Return sesion
    End Function

    Public Function agregarComentarioDeChat(idUsuario As Integer, comentario As String, idSesion As Integer) As Boolean
        Dim oComentario As New ComentarioDeChat
        oComentario.comentario = comentario
        oComentario.usuario = New Usuario
        oComentario.usuario.id = idUsuario
        oComentario.sesion = New SesionDeChat
        oComentario.sesion.id = idSesion
        Return ComentarioDeChatDao.instancia().crear(oComentario)
    End Function
End Class
