using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;

namespace iTextSharpProject
{
    class Program
    {



        public string Path { get; private set; }
        public string Password { get; private set; }
        public object Akp { get; private set; }
        public Org.BouncyCastle.X509.X509Certificate[] Chain { get; private set; }

        static void Main(string[] args)
        {
            Document pdfDoc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream("../../testingC.pdf", FileMode.OpenOrCreate));

            pdfDoc.Open();
            pdfDoc.Add(new Paragraph("Some data"));
            PdfContentByte cb = writer.DirectContent;
            cb.MoveTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height / 2);
            cb.LineTo(pdfDoc.PageSize.Width / 2, pdfDoc.PageSize.Height);
            cb.Stroke();

            PdfSignature sig = new PdfSignature(new PdfName("testname"), new PdfName("subfiltertest"));
            pdfDoc.Close();

            PdfReader reader = new PdfReader("../../testingC.pdf");
            using (FileStream fout = new FileStream("../../testingC signed.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                // appearance
                PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0', "test", false);
                PdfSignatureAppearance appearance = stamper.SignatureAppearance;
                appearance.Reason = "testing";
                appearance.Location = "here";
                appearance.SignDate = DateTime.Now.Date;
                appearance.SetVisibleSignature(new Rectangle(10, 10, 10 + 200, 10 + 100), 1, null);

                // Custom text and background image
                appearance.Image = Image.GetInstance("../../../asd.png");
                appearance.ImageScale = 0.6f;
                appearance.Image.Alignment = 300;
                appearance.Acro6Layers = true;

                StringBuilder buf = new StringBuilder();
                buf.Append("Digitally Signed by ");
                String name = "SDL";

                buf.Append(name).Append('\n');
                buf.Append("Date: ").Append(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss zzz"));

                string text = buf.ToString();

                appearance.Layer2Text = text;


                //get store
                var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);

                //get store certificates by thumbprint (collection of 1 certificate)
                string thumbprint = "dd 8d 50 24 c3 e2 c9 ce 53 40 81 18 16 ca de 57 f5 44 a7 77";
                thumbprint = thumbprint.Replace(" ", "");
                thumbprint = thumbprint.ToUpper();

                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                //get certificate raw data to parse to a bouncycastle certificate
                var bytecert = certs[0].RawData;
                //parse certificate
                var parser = new Org.BouncyCastle.X509.X509CertificateParser();
                var cert = parser.ReadCertificate(bytecert);
                //create collection for SignDetached method
                List<Org.BouncyCastle.X509.X509Certificate> chain = new List<Org.BouncyCastle.X509.X509Certificate>
                {
                    cert
                };
                //get password from certificate in bouncycastle format
                var rsa = (RSACryptoServiceProvider)(certs[0].PrivateKey);
                var es = new PrivateKeySignature(DotNetUtilities.GetRsaKeyPair(rsa).Private, "SHA-256");
                //sign the document
                MakeSignature.SignDetached(appearance, es, chain, null, null, null, 0, CryptoStandard.CMS);

                stamper.Close();







                var x = certs[0].Export(X509ContentType.Pfx,"123456");
                File.WriteAllBytes("../../test.pfx", x);
                

            }
            
        }
        public static AsymmetricKeyParameter TransformRSAPrivateKey(AsymmetricAlgorithm privateKey)
        {
            RSACryptoServiceProvider prov = privateKey as RSACryptoServiceProvider;
            RSAParameters parameters = prov.ExportParameters(true);

            return new RsaPrivateCrtKeyParameters(
                new BigInteger(1, parameters.Modulus),
                new BigInteger(1, parameters.Exponent),
                new BigInteger(1, parameters.D),
                new BigInteger(1, parameters.P),
                new BigInteger(1, parameters.Q),
                new BigInteger(1, parameters.DP),
                new BigInteger(1, parameters.DQ),
                new BigInteger(1, parameters.InverseQ));
        }
    }
}

