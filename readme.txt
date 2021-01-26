ABCPdf - last updated 2018
	From what i read on the internet it can add multiple digital signatures and it can add 
	written signatures from an images (electronic signatures) however it needs a bought licence 
	in order to work. The price starts at 329$.	https://www.websupergoo.com/purchase_abcpdf.htm
	I have tried using the trial licence but it did not work with any of the provided functions
	(InstallLicense, InstallSystemLicense, InstallRedistributionLicense, InstallTrialLicense).
	Judging by the functions intellisense gave me, ABCPdf makes a digital signature from a .pfx file,
	but i have found out that windows certificates can be exported to .pfx files and viceversa so that
	should not be an issue (see ITextSharp test application).

iTextSharp - last updated 2018
	It can add electronic signatures and digital signatures from windows certificates
	It is an open source library on github
	https://github.com/itext/itextsharp
	It is licensed under the GNU AFFERO GENERAL PUBLIC LICENSE Version 3
	https://www.gnu.org/licenses/agpl-3.0.en.html
	https://itextpdf.com/AGPL
	"https://tldrlegal.com/license/gnu-lesser-general-public-license-v3-(lgpl-3)"
	Apparently it can be used commercially but the application must be open source
	https://stackoverflow.com/questions/11324687/itext-itextsharp-for-commercial-purposes-not-recommended

pdfone - last updated 2014
	It can add electronic signatures and digital signatures from .pfx files... or at least that's 
	what it's supposed to do. I tried to digitally sign a pdf using pdfone trial and it created the
	pdf but without the digital signature. Now it throws an exception saying that the trial expired.
	The pricing starts at 600$.

PDFSharp - last updated 2013
	Can only add electronic signatures, cannot digitally sign.

Sautinsoft.document - last updated 2018
	It can add only one digital signature from a .pfx file. I tried adding a second signature, but that removed 
	the first one. Tested the trial version successfully.
	Pricing starts at 890$
	https://www.sautinsoft.com/products/document/order.php

spirePDF - last updated 2018
	It can add multiple digital signatures from .pfx files, but it should be used with caution as one signature
	might invalidate another. Tested the trial version successfully
	It should be noted that the way it adds digital signatures is different. SpirePDF does not show the signature
	directly on the page, you can only see it in the signature panel (in adobe).
	Pricing starts at 600$
	https://www.e-iceblue.com/Introduce/pdf-for-net-introduce.html