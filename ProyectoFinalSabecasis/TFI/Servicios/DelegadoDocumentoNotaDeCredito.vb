Imports iTextSharp.text.pdf
Imports Modelo
Imports iTextSharp.text
Imports System.IO

Public Class DelegadoDocumentoNotaDeCredito
    Implements DelegadoDocumento

    Property nota As NotaDeCredito
    Public Sub New(oNota As NotaDeCredito)
        Me.nota = oNota
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
        Dim invoiceCell As PdfPCell = New PdfPCell(New Phrase("Nota de crédito", titleFont))
        invoiceCell.HorizontalAlignment = 2
        invoiceCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(invoiceCell)
        Dim noCell As PdfPCell = New PdfPCell(New Phrase("Número de nota de crédito :", bodyFont))
        noCell.HorizontalAlignment = 2
        noCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(noCell)
        headertable.AddCell(New Phrase(nota.nroNotaDeCredito.ToString(), bodyFont))
        Dim dateCell As PdfPCell = New PdfPCell(New Phrase("Fecha :", bodyFont))
        dateCell.HorizontalAlignment = 2
        dateCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(dateCell)
        headertable.AddCell(New Phrase(nota.fechaEmision.ToString("dd/MM/yyyy"), bodyFont))
        Dim billCell = New PdfPCell(New Phrase("Modifica factura número :", bodyFont))
        billCell.HorizontalAlignment = 2
        billCell.Border = Rectangle.NO_BORDER
        headertable.AddCell(billCell)
       
        headertable.AddCell(New Phrase(nota.factura.nroFactura, bodyFont))
        documento.Add(headertable)

        Dim itemTable As PdfPTable = New PdfPTable(4)
        itemTable.HorizontalAlignment = 0
        itemTable.WidthPercentage = 95
        itemTable.SetWidths(New Integer() {10, 10, 40, 20})  ' then set the column's __relative__ widths
        itemTable.SpacingAfter = 40
        itemTable.DefaultCell.Border = Rectangle.BOX
        Dim cell1 As PdfPCell = New PdfPCell(New Phrase("NÚMERO ITEM", boldTableFont))
        cell1.HorizontalAlignment = 1
        itemTable.AddCell(cell1)
        Dim cell2 As PdfPCell = New PdfPCell(New Phrase("CANTIDAD", boldTableFont))
        cell2.HorizontalAlignment = 1
        itemTable.AddCell(cell2)
        Dim cell3 As PdfPCell = New PdfPCell(New Phrase("DESCRIPCIÓN", boldTableFont))
        cell3.HorizontalAlignment = 1
        itemTable.AddCell(cell3)
        Dim cell4 As PdfPCell = New PdfPCell(New Phrase("PRECIO UNITARIO", boldTableFont))
        cell4.HorizontalAlignment = 1
        itemTable.AddCell(cell4)

        Dim numero As Integer = 1

        'y Luego los detalles de la factura
        For Each oDetalle As DetalleDeFactura In nota.factura.detallesDeFactura
            Dim numberCell As PdfPCell = New PdfPCell(New Phrase(numero.ToString(), bodyFont))
            numberCell.HorizontalAlignment = 0
            numberCell.PaddingLeft = 10.0F
            numberCell.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell)

            Dim numberCell1 As PdfPCell = New PdfPCell(New Phrase(oDetalle.cantidad.ToString(), bodyFont))
            numberCell1.HorizontalAlignment = 0
            numberCell1.PaddingLeft = 10.0F
            numberCell1.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell1)

            Dim numberCell2 As PdfPCell = New PdfPCell(New Phrase(oDetalle.producto.nombre, bodyFont))
            numberCell2.HorizontalAlignment = 0
            numberCell2.PaddingLeft = 10.0F
            numberCell2.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell2)

            Dim numberCell3 As PdfPCell = New PdfPCell(New Phrase(oDetalle.precioUnitario.ToString("C"), bodyFont))
            numberCell3.HorizontalAlignment = 0
            numberCell3.PaddingLeft = 10.0F
            numberCell3.Border = Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
            itemTable.AddCell(numberCell3)

            numero += 1
        Next

        'Agregamos el total al pie
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
        Dim empty6 As PdfPCell = New PdfPCell(New Phrase(" "))
        empty6.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(empty6)
        Dim ivaAmtCell As PdfPCell = New PdfPCell(New Phrase("21 %", boldTableFont))
        ivaAmtCell.HorizontalAlignment = 1
        ivaAmtCell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(ivaAmtCell)
        Dim iva As Double = nota.monto * 0.21D
        Dim ivamontoCell As PdfPCell = New PdfPCell(New Phrase(iva.ToString("C"), boldTableFont))
        ivamontoCell.HorizontalAlignment = 1
        ivamontoCell.Border = Rectangle.LEFT_BORDER Or Rectangle.TOP_BORDER Or Rectangle.RIGHT_BORDER Or Rectangle.BOTTOM_BORDER
        itemTable.AddCell(ivamontoCell)
        Dim totalAmtCell As PdfPCell = New PdfPCell(New Phrase(nota.monto.ToString("C"), boldTableFont))
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

        Dim venc As String = Date.Now.AddYears(3).ToString("dd/MM/yyyy")
        If Not String.IsNullOrEmpty(nota.fechaCaducidad) Then
            venc = nota.fechaCaducidad
        End If
        Dim fechavenc As PdfPCell = New PdfPCell(New Phrase("Fecha de vencimiento: " & venc, boldTableFont))
        fechavenc.Border = Rectangle.NO_BORDER
        fechavenc.HorizontalAlignment = 1
        bottomtable.AddCell(fechavenc)
        documento.Add(bottomtable)


        escritor.CloseStream = False
        documento.Close()

    End Sub
End Class
