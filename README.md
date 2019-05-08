# Direct Print
![Pipeline](https://dev.azure.com/techployee/GitHub%20Pipeline/_apis/build/status/DirectPrint)

Send raw data directly to a printer. 

## Usage

Creating a printer object.

Local
```
Printer printer = new Printer("HP Color LaserJet 1600");
```

Network
```
Printer printer = new Printer("\\192.168.1.200\Brother MFC-7362N Printer");
```

A Print job can be created with three different constructors.
```
PrintJob(JobName, DataType, string)
PrintJob(JobName, DataType, byte[])
PrintJob(JobName, DataType, FileInfo)
```

Three data types
- DataType.RAW
- DataType.Text
- DataType.XPS_PASS


Printing a string.
```
printer.Print(new PrintJob("StringTest", DataType.RAW, "Hello World!"));
```

Printing a byte array.
```
printer.Print(new PrintJob("ByteTest", DataType.RAW, Encoding.ASCII.GetBytes("Hello World!")));
```

Printing a file.
```
printer.Print(new PrintJob("ByteTest", DataType.RAW, "path\to\file.txt");
```
