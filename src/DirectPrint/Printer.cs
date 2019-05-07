using System;
using System.IO;

namespace DirectPrint
{
    public class Printer : NativePrinter
    {
        public Printer(string name) : base(name)
        {
            var defaults = new PRINTER_DEFAULTS
            {
                DesiredPrinterAccess = PRINTER_ACCESS_MASK.PRINTER_ACCESS_USE
            };
            OpenPrinter(ref defaults);
        }

        /// <summary>
        /// Print the job to the printer.
        /// </summary>
        /// <param name="printJob"></param>
        public void Print(PrintJob printJob)
        {
            uint id = StartDocPrinter(printJob.DocInfo1);

            if (StartPagePrinter())
            {
                WritePrinter(printJob.Bytes);
                EndPagePrinter();
            }
            EndDocPrinter();
        }


        /// <summary>
        /// Write bytes to the print spooler.
        /// </summary>
        /// <param name="bytes"></param>
        private void WritePrinter(byte[] bytes)
        {
            WritePrinter(bytes, bytes.Length);
        }

        /// <summary>
        /// Converts stream to bytes and write them to the print spooler.
        /// </summary>
        /// <param name="stream"></param>
        private void WritePrinter(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            const int bufferSize = 1048576;
            var buffer = new byte[bufferSize];

            int read;
            while ((read = stream.Read(buffer, 0, bufferSize)) != 0)
            {
                WritePrinter(buffer, read);
            }
        }

    }
}
