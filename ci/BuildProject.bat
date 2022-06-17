cd ..\SoftwareSystemDesign
nuget install Newtonsoft.Json -OutputDirectory .\packages
nuget install ini-parser -OutputDirectory .\packages
nuget install CsvHelper -OutputDirectory .\packages
nuget install DocumentFormat.Open.Xml -OutputDirectory .\packages
nuget install NUnit -OutputDirectory .\packages
nuget install DocumentFormat.OpenXml -OutputDirectory .\packages
nuget install Microsoft.CodeAnalysis.Analyzers -OutputDirectory .\packages
nuget install MSTest.TestAdapter -Version 2.2.7 -OutputDirectory .\packages

dotnet build
