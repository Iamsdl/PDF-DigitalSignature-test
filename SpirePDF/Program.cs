using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Security;

namespace SpirePDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();
            doc.AppendPage();
            //doc.SaveToFile("../../testingC.pdf");
            //doc.LoadFromFile("../../testingC.pdf");
            var page = doc.Pages[0];
            
            String pfxPath = @"../../test.pfx";
            PdfCertificate cert = new PdfCertificate(pfxPath, "123456");
            PdfSignature signature = new PdfSignature(doc, page, cert, "signname")
            {
                ContactInfo = "contact",
                Certificated = true,
                DocumentPermissions = PdfCertificationFlags.AllowFormFill,
                Location=new System.Drawing.PointF(50,50),
                LocationInfo="center"

            };
            
            //Save pdf file.
            doc.SaveToFile(@"../../testingC signed.pdf");
            doc.Close();
        }
    }
}
