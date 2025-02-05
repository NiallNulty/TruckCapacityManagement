# Truck Capacity Management
A WinForm application that resolves an issue with too many customer orders to fit in the delivery truck. The orders are reduced fairly and evenly for each customer. The orders are read in from a CSV file and two new CSV files are output.

# Target Framework
.NET Framework 4.7.2
# Output Type
Windows Application
# Languages
C#
# How to build the project
1. Download a zip or clone the project from the repository
2. Open the solution in Visual Studio
3. Expand the "Build" dropdown at the top of Visual Studio and select "Build Solution"
4. If you made a debug build, the exe should now be in the directory "TruckCapacityManagement\TruckCapacityManagement\bin\Debug"
5. If you made a release build, the exe should now be in the directory "TruckCapacityManagement\TruckCapacityManagement\bin\Release"
# How to download the latest release
- Download the latest release found in the [releases](https://github.com/NiallNulty/TruckCapacityManagement/releases) section.
- Extract the folder and run TruckCapacityManagement.exe
- A number of example CSV files are available in the downloaded folder too. 
# CSV File Naming Convention
```
orders-YYYYMMDD.csv
```
# Example CSV File Format
```
Customer,OrderNumber,ProductCode,OrderQty
101,123,A234,100
101,123,B567,50
212,456,A789,20
```
# Additional Information
The application also does the following contains error handling. When possible, error messages are displayed to the user and certain features are locked until all prerequisites are met. A success message is displayed when the new order files are created. This message shows the directory the files were created in and the the file names.