Imports iTextSharp.text.pdf
Imports Modelo

Public Class DelegadoDocumentoFactura
    Implements DelegadoDocumento

    Property factura As Factura
    Public Sub New(oFactura As Factura)
        Me.factura = oFactura
    End Sub

    Public Sub escribir(documento As iTextSharp.text.Document, escritor As iTextSharp.text.pdf.PdfWriter) Implements DelegadoDocumento.escribir
        Dim cb As PdfContentByte = escritor.DirectContent
        Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1257, BaseFont.NOT_EMBEDDED)
        cb.SetFontAndSize(bf, 12)
        cb.BeginText()

        cb.SetTextMatrix(50, 780)
        cb.ShowText("Factura Tipo " + factura.tipoDeFactura.tipo)
        cb.SetTextMatrix(50, 768)
        cb.ShowText("Doo-Ba")

        cb.SetTextMatrix(50, 750)
        Dim positiionCounter = 750
        cb.ShowText("Número de factura: " + factura.nroFactura.ToString)
        positiionCounter -= 12
        cb.SetTextMatrix(50, positiionCounter)
        cb.ShowText("Sucursal Número: " & factura.sucursal.nroSucursal)
        positiionCounter -= 12
        cb.SetTextMatrix(50, positiionCounter)
        cb.ShowText("Conceptos: ")
        positiionCounter -= 20
        cb.SetTextMatrix(50, positiionCounter)
        For Each detalle As DetalleDeFactura In factura.detallesDeFactura
            cb.ShowText(detalle.producto.nroDeProducto & "  " & detalle.producto.nombre & "........................" & detalle.cantidad & "   $" & detalle.precioUnitario)
            positiionCounter -= 12
            cb.SetTextMatrix(50, positiionCounter)
            If Not detalle.descuentos Is Nothing Then
                If detalle.descuentos.Count > 0 Then
                    cb.ShowText("Descuentos: ")
                    positiionCounter -= 12
                    cb.SetTextMatrix(50, positiionCounter)

                    For Each oDescuento As Descuento In detalle.descuentos
                        If oDescuento.monto > 0 Then
                            cb.ShowText(oDescuento.descripcion & " ........................" & oDescuento.monto)
                        ElseIf oDescuento.porcentaje > 0 Then
                            cb.ShowText(oDescuento.descripcion & " ........................%" & oDescuento.porcentaje)
                        End If
                        positiionCounter -= 12
                        cb.SetTextMatrix(50, positiionCounter)
                    Next
                End If
            End If

        Next
       
        cb.ShowText("IVA:........................%" + ConstantesDeCargasImpositivas.IVA.ToString())
        positiionCounter -= 12
        cb.SetTextMatrix(50, positiionCounter)
        cb.ShowText("Total:............................................................" & factura.montoDeCobro)

        cb.EndText()
    End Sub
End Class
