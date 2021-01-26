using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDFSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            //it seems that pdfsharp cannot digitally sign pdfs. However it can create or edit them so i left it here.
            //
            PdfDocument doc = new PdfDocument();
            var page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);
            doc.Save("../../testingC.pdf");
        }
    }
}
