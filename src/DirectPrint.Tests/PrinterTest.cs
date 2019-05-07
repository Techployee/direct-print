using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace DirectPrint.Tests
{
    public class PrinterTest
    {
        private const string MicrosoftToPdf = "Microsoft Print to PDF";
        private const string LocalPrinter = "HP Color LaserJet 1600 Class Driver";
        private const string NetworkPrinter = @"\\172.16.17.205\Brother MFC-7362N Printer";
        private string _testMessage;

        public PrinterTest()
        {
            _testMessage = $"{_testMessage} - {DateTime.Now}";
        }

        [Test]
        public void Print_StringUsingRawToLocalPrinter_Success()
        {
            using (Printer printer = new Printer(LocalPrinter))
            {
                printer.Print(new PrintJob("PrintStringTest", DataType.RAW, _testMessage));
            }
        }


        [Test]
        public void Print_StringUsingTextToNetworkPrinter_Success()
        {
            using (Printer printer = new Printer(NetworkPrinter))
            {
                printer.Print(new PrintJob("PrintStringTest", DataType.TEXT, _testMessage));
            }
        }

        [Test]
        public void Print_PdfUsingXpsToMicrosoft_Success()
        {
            using (Printer printer = new Printer(MicrosoftToPdf))
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 20, XFontStyle.Regular);
                gfx.DrawString(_testMessage, font, XBrushes.Black,
                    new XRect(0, 0, page.Width, page.Height),
                    XStringFormats.TopLeft);

                MemoryStream stream = new MemoryStream();
                document.Save(stream, false);

                printer.Print(new PrintJob("PrintByteTest", DataType.XPS_PASS, stream.ToArray()));
            }
        }

       
    }
}
