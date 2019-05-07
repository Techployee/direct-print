using System.IO;
using System.Text;

namespace DirectPrint
{

    public enum DataType
    {
        RAW,
        XPS_PASS,
        TEXT
    }

    public class PrintJob
    {
        public DOC_INFO_1 DocInfo1 { get; set; }

        public byte[] Bytes { get; set; }


        /// <summary>
        /// Prints bytes to the printer.
        /// </summary>
        /// <param name="name">Print job name</param>
        /// <param name="dataType">Type of printing</param>
        /// <param name="bytes">bytes to print</param>
        public PrintJob(string name, DataType dataType, byte[] bytes)
        {
            DocInfo1 = new DOC_INFO_1()
            {
                pDocName = name,
                pDataType = dataType.ToString()
            };
            Bytes = bytes;
        }

        /// <summary>
        /// Prints a document to the printer.
        /// </summary>
        /// <param name="name">Print job name</param>
        /// <param name="dataType">Type of printing</param>
        /// <param name="fileInfo">File to print</param>
        public PrintJob(string name, DataType dataType, FileInfo fileInfo) :
            this(name, dataType, File.ReadAllBytes(fileInfo.FullName))
        {
        }

        /// <summary>
        /// Prints a document to the printer.
        /// </summary>
        /// <param name="name">Print job name</param>
        /// <param name="dataType">Type of printing</param>
        /// <param name="text">Text to print</param>
        public PrintJob(string name, DataType dataType, string text) :
            this(name, dataType, Encoding.ASCII.GetBytes(text))
        {

        }
    }
}
