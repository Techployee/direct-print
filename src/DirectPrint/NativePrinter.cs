using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DirectPrint
{
    public class NativePrinter : IDisposable
    {
        private readonly string _printerName;
        private IntPtr _printerHandle;

        public NativePrinter(string printerName)
        {
            _printerName = printerName;
        }

        /// <summary>
        /// Opens the printer and stores the handle.
        /// </summary>
        /// <param name="defaults"></param>
        /// <returns></returns>
        protected IntPtr OpenPrinter(ref PRINTER_DEFAULTS defaults)
        {
            if (NativeMethods.OpenPrinterW(_printerName, out var printerHandle, ref defaults) == 0)
            {
                throw new Win32Exception();
            }
            _printerHandle = printerHandle;
            return _printerHandle;
        }


        /// <summary>
        /// Notifies the print spooler that a document is to be spooled for printing.
        /// </summary>
        /// <param name="di1"></param>
        /// <returns></returns>
        protected uint StartDocPrinter(DOC_INFO_1 di1)
        {
            var id = NativeMethods.StartDocPrinterW(_printerHandle, 1, ref di1);
            if (id == 0)
            {
                if (Marshal.GetLastWin32Error() == 1804)
                {
                    throw new Exception("The specified datatype is invalid, try setting " +
                                        "'Enable advanced printing features' in printer properties.", new Win32Exception());
                }
                throw new Win32Exception();
            }

            return id;
        }

        /// <summary>
        /// Notifies the spooler that a page is about to be printed on the specified printer.
        /// </summary>
        /// <returns></returns>
        protected bool StartPagePrinter()
        {
            return NativeMethods.StartPagePrinter(_printerHandle);
        }

        /// <summary>
        /// Notifies the print spooler that data should be written to the specified printer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        protected void WritePrinter(byte[] buffer, int size)
        {
            int written = 0;
            if (NativeMethods.WritePrinter(_printerHandle, buffer, size, ref written) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Ends a print job for the specified printer.
        /// </summary>
        protected void EndDocPrinter()
        {
            NativeMethods.EndDocPrinter(_printerHandle);
        }

        /// <summary>
        /// Notifies the print spooler that the application is at the end of a page in a print job.
        /// </summary>
        /// <returns></returns>
        protected bool EndPagePrinter()
        {
            return NativeMethods.EndPagePrinter(_printerHandle);
        }       

        /// <summary>
        /// Disposes of the printer and handle.
        /// </summary>
        public void Dispose()
        {
            NativeMethods.ClosePrinter(_printerHandle);
        }
    }
}
