Imports iTextSharp.text.pdf
Imports Modelo
Imports iTextSharp.text
Imports System.IO

Public Class DelegadoDocumentoFactura
    Implements DelegadoDocumento

    Property factura As Factura
    Public Sub New(oFactura As Factura)
        Me.factura = oFactura
    End Sub

    Public Sub escribir(documento As iTextSharp.text.Document, escritor As iTextSharp.text.pdf.PdfWriter) Implements DelegadoDocumento.escribir
        Dim titleFont = FontFactory.GetFont("Arial", 14, Font.BOLD)
        Dim boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD)
        Dim bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL)
        Dim pageSize As Rectangle = escritor.PageSize

        documento.Open()

        'Creamos la cabecera de la factura
        Dim headertable As PdfPTable = New PdfPTable(3)
        headertable.HorizontalAlignment = 0
        headertable.WidthPercentage = 100
        headertable.SetWidths(New Integer() {4, 2, 4})  ' then set the column's __relative__ widths
        headertable.DefaultCell.Border = Rectangle.NO_BORDER

        headertable.SpacingAfter = 30
        Dim nested As PdfPTable = New PdfPTable(1)
        nested.DefaultCell.Border = Rectangle.BOX
        Dim nextPostCell1 As PdfPCell = New PdfPCell(New Phrase("Doo-Ba S.R.L", bodyFont))

        nextPostCell1.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        nested.AddCell(nextPostCell1)
        Dim nextPostCell2 As PdfPCell = New PdfPCell(New Phrase("Av. Montes De Oca 336, Flores", bodyFont))
        nextPostCell2.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        nested.AddCell(nextPostCell2)
        Dim nextPostCell3 As PdfPCell = New PdfPCell(New Phrase("Cidad Autónoma de Buenos Aires, 1406", bodyFont))
        nextPostCell3.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        nested.AddCell(nextPostCell3)
        Dim nextPostCell4 As PdfPCell = New PdfPCell(New Phrase("C.U.I.T.: 27-16321016-4", bodyFont))
        nextPostCell4.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        nested.AddCell(nextPostCell4)
        Dim nextPostCell5 As PdfPCell = New PdfPCell(New Phrase("Teléfono: 0800-111-1111", bodyFont))
        nextPostCell5.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        nested.AddCell(nextPostCell5)
        Dim nesthousing As PdfPCell = New PdfPCell(nested)
        nesthousing.Rowspan = 6
        nesthousing.Padding = 0.0F
        headertable.AddCell(nesthousing)


        headertable.AddCell("")
        Dim invoiceCell As PdfPCell = New PdfPCell(New Phrase("Factura tipo B", titleFont))
        invoiceCell.HorizontalAlignment = 2
        invoiceCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(invoiceCell)
        Dim noCell As PdfPCell = New PdfPCell(New Phrase("Número de factura :", bodyFont))
        noCell.HorizontalAlignment = 2
        noCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(noCell)
        headertable.AddCell(New Phrase(Me.factura.nroFactura.ToString(), bodyFont))
        Dim dateCell As PdfPCell = New PdfPCell(New Phrase("Fecha :", bodyFont))
        dateCell.HorizontalAlignment = 2
        dateCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(dateCell)
        headertable.AddCell(New Phrase(Date.Now.ToString("dd/MM/yyyy"), bodyFont))
        Dim billCell = New PdfPCell(New Phrase("Cliente :", bodyFont))
        billCell.HorizontalAlignment = 2
        billCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(billCell)
        Dim direccionCompleta As String = ""
        direccionCompleta = direccionCompleta & factura.usuario.persona.contacto.calle & " "
        direccionCompleta = direccionCompleta & factura.usuario.persona.contacto.numero & " "
        If factura.usuario.persona.contacto.piso <> 0 Then
            direccionCompleta = direccionCompleta & factura.usuario.persona.contacto.piso & " " & direccionCompleta & factura.usuario.persona.contacto.departamento
        End If
        factura.usuario.persona.contacto.localidad = GestorABM.instancia().obtenerLocalidadPorId(factura.usuario.persona.contacto.localidad.id)
        If Not factura.usuario.persona.contacto.localidad Is Nothing Then
            direccionCompleta = ", " & direccionCompleta & factura.usuario.persona.contacto.localidad.localidad & " " & factura.usuario.persona.contacto.localidad.cp
            factura.usuario.persona.contacto.localidad.provincia = GestorABM.instancia().obtenerProvinciaPorId(factura.usuario.persona.contacto.localidad.id)
            If Not factura.usuario.persona.contacto.localidad.provincia Is Nothing Then
                direccionCompleta = direccionCompleta & ", " & factura.usuario.persona.contacto.localidad.provincia.provincia & ", " & factura.usuario.persona.contacto.localidad.provincia.pais.pais
            End If
        End If

        headertable.AddCell(New Phrase(factura.usuario.persona.nombre & " " & factura.usuario.persona.apellido & " - " & direccionCompleta, bodyFont))
        documento.Add(headertable)

        Dim itemTable As PdfPTable = New PdfPTable(6)
        itemTable.HorizontalAlignment = 0
        itemTable.WidthPercentage = 95
        itemTable.SetWidths(New Integer() {10, 40, 10, 20, 20, 20})  ' then set the column's __relative__ widths
        itemTable.SpacingAfter = 40
        itemTable.DefaultCell.Border = Rectangle.BOX
        Dim cell1 As PdfPCell = New PdfPCell(New Phrase("NÚMERO ITEM", boldTableFont))
        cell1.HorizontalAlignment = 1
        itemTable.AddCell(cell1)
        Dim cell2 As PdfPCell = New PdfPCell(New Phrase("DETALLE", boldTableFont))
        cell2.HorizontalAlignment = 1
        itemTable.AddCell(cell2)
        Dim cell3 As PdfPCell = New PdfPCell(New Phrase("CANTIDAD", boldTableFont))
        cell3.HorizontalAlignment = 1
        itemTable.AddCell(cell3)
        Dim cell4 As PdfPCell = New PdfPCell(New Phrase("PRECIO UNITARIO", boldTableFont))
        cell4.HorizontalAlignment = 1
        itemTable.AddCell(cell4)
        Dim cell5 As PdfPCell = New PdfPCell(New Phrase("DESCUENTO", boldTableFont))
        cell5.HorizontalAlignment = 1
        itemTable.AddCell(cell5)
        Dim cell6 As PdfPCell = New PdfPCell(New Phrase("TOTAL", boldTableFont))
        cell6.HorizontalAlignment = 1
        itemTable.AddCell(cell6)

        Dim numero As Integer = 1

        'y Luego los detalles de la factura
        For Each oDetalle As DetalleDeFactura In factura.detallesDeFactura
            Dim numberCell As PdfPCell = New PdfPCell(New Phrase(numero.ToString(), bodyFont))
            numberCell.HorizontalAlignment = 0
            numberCell.PaddingLeft = 10.0F
            numberCell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell)

            Dim numberCell1 As PdfPCell = New PdfPCell(New Phrase(oDetalle.producto.nombre, bodyFont))
            numberCell1.HorizontalAlignment = 0
            numberCell1.PaddingLeft = 10.0F
            numberCell1.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell1)

            Dim numberCell2 As PdfPCell = New PdfPCell(New Phrase(oDetalle.cantidad.ToString(), bodyFont))
            numberCell2.HorizontalAlignment = 0
            numberCell2.PaddingLeft = 10.0F
            numberCell2.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell2)

            Dim numberCell3 As PdfPCell = New PdfPCell(New Phrase(oDetalle.producto.precioVenta.ToString("C"), bodyFont))
            numberCell3.HorizontalAlignment = 0
            numberCell3.PaddingLeft = 10.0F
            numberCell3.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell3)

            Dim totalDescuento As Double = 0
            If Not oDetalle.producto.descuentos Is Nothing Then
                For Each oDescuento As Descuento In oDetalle.producto.descuentos
                    If oDescuento.monto <> 0 Then
                        totalDescuento += oDescuento.monto * oDetalle.cantidad
                    End If
                    If oDescuento.porcentaje <> 0 Then
                        totalDescuento += (oDescuento.porcentaje * ((oDetalle.precioUnitario * oDetalle.cantidad) - totalDescuento)) / 100
                    End If
                Next
            End If
            totalDescuento = totalDescuento

            Dim numberCell4 As PdfPCell = New PdfPCell(New Phrase(totalDescuento.ToString("C"), bodyFont))
            numberCell4.HorizontalAlignment = 0
            numberCell4.PaddingLeft = 10.0F
            numberCell4.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell4)

            Dim totalLineaFactura As Double = (oDetalle.precioUnitario * oDetalle.cantidad) - totalDescuento

            Dim numberCell5 As PdfPCell = New PdfPCell(New Phrase(totalLineaFactura.ToString("C"), bodyFont))
            numberCell5.HorizontalAlignment = 0
            numberCell5.PaddingLeft = 10.0F
            numberCell5.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell5)

            numero += 1
        Next

        Dim numberCell6 As PdfPCell = New PdfPCell(New Phrase(numero.ToString(), bodyFont))
        numberCell6.HorizontalAlignment = 0
        numberCell6.PaddingLeft = 10.0F
        numberCell6.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        itemTable.AddCell(numberCell6)

        Dim numberCell7 As PdfPCell = New PdfPCell(New Phrase("Recargo por tarjeta de crédito", bodyFont))
        numberCell7.HorizontalAlignment = 0
        numberCell7.PaddingLeft = 10.0F
        numberCell7.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        itemTable.AddCell(numberCell7)

        Dim numberCell8 As PdfPCell = New PdfPCell(New Phrase("1", bodyFont))
        numberCell8.HorizontalAlignment = 0
        numberCell8.PaddingLeft = 10.0F
        numberCell8.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        itemTable.AddCell(numberCell8)

        Dim numberCell9 As PdfPCell = New PdfPCell(New Phrase(factura.recargoPorTarjeta.ToString("C"), bodyFont))
        numberCell9.HorizontalAlignment = 0
        numberCell9.PaddingLeft = 10.0F
        numberCell9.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        itemTable.AddCell(numberCell9)

        Dim numberCell10 As PdfPCell = New PdfPCell(New Phrase((Convert.ToDouble("0")).ToString("C"), bodyFont))
        numberCell10.HorizontalAlignment = 0
        numberCell10.PaddingLeft = 10.0F
        numberCell10.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        itemTable.AddCell(numberCell10)

        Dim numberCell11 As PdfPCell = New PdfPCell(New Phrase(factura.recargoPorTarjeta.ToString("C"), bodyFont))
        numberCell11.HorizontalAlignment = 0
        numberCell11.PaddingLeft = 10.0F
        numberCell11.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
        itemTable.AddCell(numberCell11)

        numero += 1

        'gastos por envío
        If factura.orden.envio.monto <> 0 Then
            Dim numberCell12 As PdfPCell = New PdfPCell(New Phrase(numero.ToString(), bodyFont))
            numberCell12.HorizontalAlignment = 0
            numberCell12.PaddingLeft = 10.0F
            numberCell12.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
            itemTable.AddCell(numberCell12)

            Dim numberCell13 As PdfPCell = New PdfPCell(New Phrase("Gastos de envío", bodyFont))
            numberCell13.HorizontalAlignment = 0
            numberCell13.PaddingLeft = 10.0F
            numberCell13.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
            itemTable.AddCell(numberCell13)

            Dim numberCell14 As PdfPCell = New PdfPCell(New Phrase("1", bodyFont))
            numberCell14.HorizontalAlignment = 0
            numberCell14.PaddingLeft = 10.0F
            numberCell14.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
            itemTable.AddCell(numberCell14)

            Dim numberCell15 As PdfPCell = New PdfPCell(New Phrase(factura.orden.envio.monto.ToString("C"), bodyFont))
            numberCell15.HorizontalAlignment = 0
            numberCell15.PaddingLeft = 10.0F
            numberCell15.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
            itemTable.AddCell(numberCell15)

            Dim numberCell16 As PdfPCell = New PdfPCell(New Phrase((Convert.ToDouble("0")).ToString("C"), bodyFont))
            numberCell16.HorizontalAlignment = 0
            numberCell16.PaddingLeft = 10.0F
            numberCell16.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
            itemTable.AddCell(numberCell16)

            Dim numberCell17 As PdfPCell = New PdfPCell(New Phrase(factura.orden.envio.monto.ToString("C"), bodyFont))
            numberCell17.HorizontalAlignment = 0
            numberCell17.PaddingLeft = 10.0F
            numberCell17.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
            itemTable.AddCell(numberCell17)
        End If

        'Agregamos el total al pie
        Dim empty1 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty1.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty1)
        Dim empty2 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty2.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty2)
        Dim empty3 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty3.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty3)
        Dim totalAmtCell1 As PdfPCell = New PdfPCell(New Phrase("IVA %"))
        totalAmtCell1.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(totalAmtCell1)
        Dim totalAmtCell2 As PdfPCell = New PdfPCell(New Phrase("IVA"))
        totalAmtCell2.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(totalAmtCell2)
        Dim totalAmtStrCell As PdfPCell = New PdfPCell(New Phrase("Total en pesos (AR)", boldTableFont))
        totalAmtStrCell.Border = Rectangle.TOP_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        totalAmtStrCell.HorizontalAlignment = 1
        itemTable.AddCell(totalAmtStrCell)
        Dim empty4 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty4.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty4)
        Dim empty5 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty5.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty5)
        Dim empty6 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty6.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty6)
        Dim ivaAmtCell As PdfPCell = New PdfPCell(New Phrase("21 %", boldTableFont))
        ivaAmtCell.HorizontalAlignment = 1
        ivaAmtCell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(ivaAmtCell)
        Dim iva As Double = factura.montoDeVenta * 0.21D
        Dim ivamontoCell As PdfPCell = New PdfPCell(New Phrase(iva.ToString("C"), boldTableFont))
        ivamontoCell.HorizontalAlignment = 1
        ivamontoCell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(ivamontoCell)
        Dim totalAmtCell As PdfPCell = New PdfPCell(New Phrase(factura.montoDeCobro.ToString("C"), boldTableFont))
        totalAmtCell.HorizontalAlignment = 1
        totalAmtCell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(totalAmtCell)

        documento.Add(itemTable)

        'agregar CAI y fecha de vencimiento
        Dim bottomtable As PdfPTable = New PdfPTable(2)
        bottomtable.HorizontalAlignment = 0
        bottomtable.WidthPercentage = 100
        bottomtable.SetWidths(New Integer() {20, 20})  ' then set the column's __relative__ widths
        bottomtable.DefaultCell.Border = Rectangle.NO_BORDER

        Dim cai As PdfPCell = New PdfPCell(New Phrase("CAI : 0002254894384", boldTableFont))
        cai.Border = Rectangle.NO_BORDER
        cai.HorizontalAlignment = 1
        bottomtable.AddCell(cai)

        Dim fechavenc As PdfPCell = New PdfPCell(New Phrase("Fecha de vencimiento: " & Date.Now.AddYears(3).ToString("dd/MM/yyyy"), boldTableFont))
        fechavenc.Border = Rectangle.NO_BORDER
        fechavenc.HorizontalAlignment = 1
        bottomtable.AddCell(fechavenc)
        documento.Add(bottomtable)


        escritor.CloseStream = False
        documento.Close()

    End Sub
End Class
