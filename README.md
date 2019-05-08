<img src="https://raw.githubusercontent.com/Techployee/direct-print/master/images/directprint.png" alt="logo" width="200"/>

<img src="https://dev.azure.com/techployee/GitHub%20Pipeline/_apis/build/status/DirectPrint" alt="pipeline" />

Send raw data directly to a printer. 

## Usage

Creating a printer object.

Local Printer
```
Printer printer = new Printer("HP Color LaserJet 1600");
```

Network Printer
```
Printer printer = new Printer("\\192.168.1.200\Brother MFC-7362N Printer");
```

### Print Job
A Print job can be created with three different constructors.
```
PrintJob(JobName, DataType, string)
PrintJob(JobName, DataType, byte[])
PrintJob(JobName, DataType, FileInfo)
```

Three data types:
- DataType.RAW
- DataType.Text
- DataType.XPS_PASS


### Printing
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
