# ssd2122kisk11shevchuk18
The developed software reads a file of the appropriate format or, in its absence, takes a formula and generates such a file, parses and generates a sequence, saving it in a .csv file.
In addition, 5 console commands are implemented, which in turn:
-sf (specifies the location of the initial file),
-s (sets sequence formula),
-n (says how many elements of the sequence should be generated),
-h (opens a documentation window to help the user),
-v (returns the software version).

To run current project you need:
1) Donwload 'Build Tools for Visual Studio' from site: https://visualstudio.microsoft.com/downloads/
3) Download 'Download .NET SDK x64' from https://dotnet.microsoft.com/en-us/download
4) Download NuGet.exe from https://www.nuget.org/downloads
5) Install PS from previous steps: Build Tools for Visual Studio - install befault packages plus .NET Framework 4.8 SDK and .NET Framework 4.8 targeting pack
6) Add nuget.exe to the folder with MSBuild.exe 
7) Add global enviromental variable path to folder from step 5
8) Run .bat file from 'ci' folder in next order: BuildProject.bat -> Start.bat

For current project you need to install next packages:

Install-Package ini-parser

Install-Package Newtonsoft.Json -Version 13.0.1

For this you can write string above at PM package console (https://www.nuget.org/packages/Newtonsoft.Json, https://github.com/rickyah/ini-parser).
Also for work with .xml extention you need to add references to next packages:

System.Xml

System.Xml.Linq

System.Xml.Serializations

For this you can add references at project by context menu and choose 'Assemblies' filter.
All those packages needed for correct work with supported extentions (.ini, .json, .xml).

If you want to use files created by yourself, all those files should have next structure:
.ini:

Sequence variable should be inside 'DataModel' block. If .ini file contains another data, it will be ignored.

Example:
```ini
[DataModel]
Sequence = (n*2+2/n*(1+5))/2
```

.Json:

Sequence variable should be inside curve brackets, as at default json format.

Example:
```json
{"Sequence":"(n*2+2/n*(1+5))/2"}
```
.xml:

Sequence variable should be inside 'DataModel' tag. If .xml file contains another data, it will be ignored.

Example:
```xml
<?xml version="1.0"?>
<DataModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Sequence>(n*2+2/n*(1+5))/2</Sequence>
</DataModel>
```
