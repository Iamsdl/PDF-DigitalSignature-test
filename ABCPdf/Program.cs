using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSupergoo.ABCpdf11;
using WebSupergoo.ABCpdf11.Objects;

namespace ABCPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            //not working
            
            XSettings.Register();
            XSettings.InstallLicense                ("cd9b5c07db69df2bf57c0a04d9bca58b10c44889c9fb197984e592f49addfce5ec5fe85d7b9205bc");
            XSettings.InstallSystemLicense          ("cd9b5c07db69df2bf57c0a04d9bca58b10c44889c9fb197984e592f49addfce5ec5fe85d7b9205bc");
            XSettings.InstallRedistributionLicense  ("cd9b5c07db69df2bf57c0a04d9bca58b10c44889c9fb197984e592f49addfce5ec5fe85d7b9205bc");
            XSettings.InstallTrialLicense           ("cd9b5c07db69df2bf57c0a04d9bca58b10c44889c9fb197984e592f49addfce5ec5fe85d7b9205bc");
            XSettings.Register();
            Doc theDoc = new Doc();
            theDoc.FontSize = 72;
            theDoc.AddTextStyled("<b>Gallia</b> est omnis divisa in partes tres, quarum unam incolunt <b>Belgae</b>, aliam <b>Aquitani</b>, tertiam qui ipsorum lingua <b>Celtae</b>, nostra <b>Galli</b> appellantur.");
            theDoc.Save("../../testingC.pdf"); //need licence
            theDoc.Clear();

            Signature theSig = (Signature)theDoc.Form["Signature"];
            theSig.Location = "here";
            theSig.Reason = "test";
            //pfx + password
            theSig.Sign("../../../test.pfx", "123456");
            theDoc.Save("../../testingC signed.pdf");
        }
    }
}
