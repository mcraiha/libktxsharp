# libktxsharp
C# library for handling [KTX File Format](https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/)

## Build status
![](https://github.com/mcraiha/libktxsharp/workflows/CIBuild/badge.svg)

## Why
Because KTX specs are public and I need something like this for my upcoming projects

## How to use
1. Build .dll or include [lib folder](lib) in your project
2. Use following code example
```csharp
byte[] ktxBytes = File.ReadAllBytes("myImage.ktx");

KtxStructure ktxStructure = null;
using (MemoryStream ms = new MemoryStream(ktxBytes))
{
	ktxStructure = KtxLoader.LoadInput(ms);
}

Console.WriteLine(ktxStructure.header.pixelWidth);
```

## How do I build this
### Requirements
Dotnet core 2.0 environment

### Build .dll
Move to lib folder and run
```bash
dotnet build
```

### Build nuget
TBA

## Testing
### Requirements 
* nunit
* NUnit3TestAdapter
* Microsoft.NET.Test.Sdk

All requirements are restored when you run
```bash
dotnet restore
```

### Run tests
Just call
```bash
dotnet test
```

## What is in
* Basic KTX read functionality
* Some test cases

## What is missing
* KTX write support
* CI
* Nuget
* More files for testing
* Benchmarks

## License
All code is released under *"Do whatever you want"* license aka [Unlicense](LICENSE)