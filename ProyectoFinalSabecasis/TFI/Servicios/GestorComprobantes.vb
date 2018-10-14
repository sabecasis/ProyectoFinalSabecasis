Imports Modelo
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class GestorComprobantes
    Private Sub New()

    End Sub

    Private Shared objeto As New GestorComprobantes

    Public Shared Function instancia() As GestorComprobantes
        Return objeto
    End Function

    Private Function crearDocumentoPdf(titulo As String, delegado As DelegadoDocumento) As Byte()
        Dim documento As New iTextSharp.text.Document(PageSize.A4_LANDSCAPE)
        documento.AddTitle(titulo)

        Dim stream As New System.IO.MemoryStream
        Dim escritor As PdfWriter = PdfWriter.GetInstance(documento, stream)
        escritor.ViewerPreferences = PdfWriter.PageLayoutSinglePage

        documento.Open()
        delegado.escribir(documento, escritor)
        documento.Close()

        Return stream.ToArray()
    End Function

    Public Function crearComprobanteFactura(oFactura As Factura) As Byte()
        Dim oDelegado As DelegadoDocumento = New DelegadoDocumentoFactura(oFactura)
        Return crearDocumentoPdf("Doo-Ba", oDelegado)
    End Function

    Public Function crearComprobanteSolicitud(oSolicitud As SolicitudDeStock) As Byte()
        Dim oDelegado As DelegadoDocumento = New DelegadoDocumentoSolicitudStock(oSolicitud)
        Return crearDocumentoPdf("Doo-Ba", oDelegado)
    End Function

    Public Function crerComprobanteDeIngresoDeStock(oIngreso As IngresoDeStock) As Byte()
        Dim oDelegado As DelegadoDocumento = New DelegadoDocumentoDeIngresoDeStock(oIngreso)
        Return crearDocumentoPdf("Doo-Ba", oDelegado)
    End Function

    Public Function crearComprobanteDeEgresoDeStock(oEgreso As EgresoDeStock) As Byte()
        Dim oDelegado As DelegadoDocumento = New DelegadoDocumentoDeEgresoDeStock(oEgreso)
        Return crearDocumentoPdf("Doo-Ba", oDelegado)
    End Function

    Public Function crearComprobanteDeNotaDeCredito(oNota As NotaDeCredito) As Byte()
        Dim oDelegado As DelegadoDocumento = New DelegadoDocumentoNotaDeCredito(oNota)
        Return crearDocumentoPdf("Doo-Ba", oDelegado)
    End Function
End Class
