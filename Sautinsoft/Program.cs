using System.IO;
using System.Linq;
using SautinSoft.Document;
using SautinSoft.Document.Drawing;

namespace Sautinsoft
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitalSignature();
        }

        public static void DigitalSignature()
        {
            // Load an existing document (*.docx, *.rtf, *.pdf, *.html, *.txt, *.pdf) and save it in a PDF document with the digital signature.
            DocumentCore dc = new DocumentCore();
            Section section = new Section(dc);

            dc.Content.End.Insert("\nThis is a first line in 2nd paragraph.", new CharacterFormat() { Size = 25, FontColor = Color.Blue, Bold = true });
            SpecialCharacter lBr = new SpecialCharacter(dc, SpecialCharacterType.LineBreak);
            dc.Content.End.Insert(lBr.Content);
            dc.Content.End.Insert("This is a second line.", new CharacterFormat() { Size = 20, FontColor = Color.DarkGreen, UnderlineStyle = UnderlineType.Single });
            dc.Save("../../testingC.pdf", new PdfSaveOptions());

            // Path to a loadable document.
            string loadPath = @"../../testingC.pdf";

            DocumentCore pdfdoc = DocumentCore.Load(loadPath);
            loadPath = @"../../testingC signed.pdf";

            // Signature line added with MS Word -> Insert tab -> Signature Line button by default has description 'Microsoft Office Signature Line...'.
            ShapeBase signatureLine = dc.GetChildElements(true).OfType<ShapeBase>().FirstOrDefault();

            // This picture symbolizes a handwritten signature
            Picture signature = new Picture(dc, @"../../../asd.png");

            // Signature in this document will be 4.5 cm right of TopLeft position of signature line
            // and 4.5 cm below of TopLeft position of signature line.
            signature.Layout = Layout.Floating(
               new HorizontalPosition(4.5, LengthUnit.Centimeter, HorizontalPositionAnchor.Page),
               new VerticalPosition(-4.5, LengthUnit.Centimeter, VerticalPositionAnchor.Page),
               signature.Layout.Size);

            //signature.Layout = Layout.Inline(signature.Layout.Size);
            PdfSaveOptions options = new PdfSaveOptions();

            // Path to the certificate (*.pfx).
            options.DigitalSignature.CertificatePath = @"../../../test.pfx";

            // Password of the certificate.
            options.DigitalSignature.CertificatePassword = "123456";

            // Additional information about the certificate.
            options.DigitalSignature.Location = "here";
            options.DigitalSignature.Reason = "testing";
            options.DigitalSignature.ContactInfo = "asd@asd.asd";

            // Placeholder where signature should be visualized.
            options.DigitalSignature.SignatureLine = signatureLine;

            // Visual representation of digital signature.
            options.DigitalSignature.Signature = signature;

            string savePath = Path.ChangeExtension(loadPath, ".pdf");
            dc.Save(savePath, options);

            pdfdoc = DocumentCore.Load(loadPath);
            loadPath= @"../../testingC signed2.pdf";
            signature = new Picture(dc, @"../../../asd.png");

            // Signature in this document will be 4.5 cm right of TopLeft position of signature line
            // and 4.5 cm below of TopLeft position of signature line.
            signature.Layout = Layout.Floating(
               new HorizontalPosition(-4.5, LengthUnit.Centimeter, HorizontalPositionAnchor.Page),
               new VerticalPosition(-8.5, LengthUnit.Centimeter, VerticalPositionAnchor.Page),
               signature.Layout.Size);

            //signature.Layout = Layout.Inline(signature.Layout.Size);
            options = new PdfSaveOptions();

            // Path to the certificate (*.pfx).
            options.DigitalSignature.CertificatePath = @"../../../test.pfx";

            // Password of the certificate.
            options.DigitalSignature.CertificatePassword = "123456";

            // Additional information about the certificate.
            options.DigitalSignature.Location = "here";
            options.DigitalSignature.Reason = "testing";
            options.DigitalSignature.ContactInfo = "asd@asd.asd";

            // Placeholder where signature should be visualized.
            options.DigitalSignature.SignatureLine = signatureLine;

            // Visual representation of digital signature.
            options.DigitalSignature.Signature = signature;

            savePath = Path.ChangeExtension(loadPath, ".pdf");
            dc.Save(savePath, options);
        }
    }
}


