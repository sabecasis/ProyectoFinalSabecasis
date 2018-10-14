Imports System.Web.Services
Imports Seguridad
Imports NegocioYServicios
Imports System.IO

Public Class Checkout
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Session("Sesion") Is Nothing Then
                Try
                    Dim reader As New StreamReader(Server.MapPath("/static/add_xml/add1_" & DirectCast(Session("sesion"), Sesion).usuario.id.ToString() & ".xml"))
                    Dim line As String
                    line = reader.ReadToEnd()
                    If line.Contains("Ad") Then
                        Add1.AdvertisementFile = "/static/add_xml/add1_" & DirectCast(Session("sesion"), Sesion).usuario.id.ToString() & ".xml"
                        Add1.Width = 400
                        Add1.Height = 60
                    End If
                    reader.Close()
                    reader.Dispose()
                Catch ex As Exception
                End Try

                aPerfil.InnerText = DirectCast(Session("sesion"), Sesion).usuario.nombre
                Dim oCheckout As Modelo.Checkout = DirectCast(Session("Sesion"), Sesion).checkout
                If Not oCheckout Is Nothing Then

                    If Me.IsPostBack Then
                        If Request.Form("accion").Equals(ConstantesDeEvento.ELIMINAR) Then
                            DirectCast(Session("Sesion"), Sesion).checkout = GestorOrdenes.instancia().eliminarProductoDeCheckout(oCheckout, Convert.ToInt32(Request.Form("idProd")))
                            oCheckout = DirectCast(Session("Sesion"), Sesion).checkout
                        End If
                        If Request.Form("accion").Equals(ConstantesDeEvento.COMPRAR) Then
                            Response.Redirect("/Cliente/MetodoDeEnvio.aspx?check=" & oCheckout.idSesion)
                        End If
                    End If
                    contenido.InnerHtml = GestorOrdenes.instancia().obtenerTablaDeProductosDeCheckout(oCheckout)
                    sugerencias.InnerHtml = GestorOrdenes.instancia().armarTablaSugerencias(GestorOrdenes.instancia().obtenerSugerenciasDeProductos(oCheckout))
                End If
            Else
                Try
                    Dim reader As New StreamReader(Server.MapPath("/static/add_xml/add_general.xml"))
                    Dim line As String
                    line = reader.ReadToEnd()
                    If line.Contains("Ad") Then
                        Add1.AdvertisementFile = "/static/add_xml/add_general.xml"
                        Add1.Width = 400
                        Add1.Height = 60
                    End If
                    reader.Close()
                    reader.Dispose()
                Catch ex As Exception
                End Try
            End If
        Catch ex As ExcepcionDeValidacion
            LblMensaje.InnerText = ex.Message
            LblMensaje.DataBind()
        Catch exes As ExcepcionDelSistema
            LblMensaje.InnerText = exes.mensaje
            LblMensaje.DataBind()
        Catch exe As Exception
            LblMensaje.InnerText = exe.Message
            LblMensaje.DataBind()
        End Try
        breadcrums.InnerHtml = Session("cadenabreadcrums")
    End Sub

End Class