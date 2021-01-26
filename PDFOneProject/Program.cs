using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gnostice.PDFOne;

namespace PDFOneProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //need licence
            PDFDocument pDFDocument = new PDFDocument(/*insert licence here*/);
            PDFSignature pDFSignature = new PDFSignature("../../../test.pfx", "123456", "because", "here", "contactInfo", 1);
            pDFDocument.AddSignature(pDFSignature);
            pDFDocument.Save("../../testingC signed.pdf");
        }
    }
}
