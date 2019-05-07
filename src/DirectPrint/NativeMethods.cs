using System;
using System.Runtime.InteropServices;

namespace DirectPrint
{
    [Flags]
    public enum PRINTER_ACCESS_MASK : uint
    {
        PRINTER_ACCESS_ADMINISTER = 0x00000004,
        PRINTER_ACCESS_USE = 0x00000008,
        PRINTER_ACCESS_MANAGE_LIMITED = 0x00000040,
        PRINTER_ALL_ACCESS = 0x000F000C,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct PRINTER_DEFAULTS
    {
        public string pDatatype;

        private IntPtr pDevMode;

        public PRINTER_ACCESS_MASK DesiredPrinterAccess;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DOC_INFO_1
    {
        public string pDocName;

        public string pOutputFile;

        public string pDataType;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct DRIVER_INFO_3
    {
        public uint cVersion;
        public string pName;
        public string pEnvironment;
        public string pDriverPath;
        public string pDataFile;
        public string pConfigFile;
        public string pHelpFile;
        public IntPtr pDependentFiles;
        public string pMonitorName;
        public string pDefaultDataType;
    }

    public class NativeMethods
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/openprinter
        /// </summary>
        /// <param name="pPrinterName"></param>
        /// <param name="phPrinter"></param>
        /// <param name="pDefault"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int OpenPrinterW(string pPrinterName, out IntPtr phPrinter, ref PRINTER_DEFAULTS pDefault);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/closeprinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", SetLastError = true)]
        public static extern int ClosePrinter(IntPtr hPrinter);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/startdocprinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <param name="level"></param>
        /// <param name="di1"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint StartDocPrinterW(IntPtr hPrinter, uint level, [In] ref DOC_INFO_1 di1);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/enddocprinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int EndDocPrinter(IntPtr hPrinter);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/startpageprinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/endpageprinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/printdocs/writeprinter
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <param name="pBuf"></param>
        /// <param name="cbBuf"></param>
        /// <param name="pcWritten"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int WritePrinter(IntPtr hPrinter, byte[] pBuf, int cbBuf, ref int pcWritten);

      

    }
}
