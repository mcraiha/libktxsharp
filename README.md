# libktxsharp

C# library for handling [KTX File Format](https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/). Only Version 1 is supported, so Version 2 is **NOT** supported.

## Build status

![](https://github.com/mcraiha/libktxsharp/workflows/CIBuild/badge.svg)

## Nuget

[https://www.nuget.org/packages/LibKTX/](https://www.nuget.org/packages/LibKTX/)

## Why

Because KTX specs are public and I need something like this for my upcoming projects

## How to use
1. Get nuget, build .dll or include [lib folder](lib) in your project
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

Dotnet core 2.0 (or newer) environment

### Build .dll

Move to lib folder and run
```bash
dotnet build
```

### Build nuget

Move to lib folder and run
```bash
dotnet pack -o out --configuration Release --include-source --include-symbols
```

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

## What is partially in

* KTX write support

## What is missing

* More files for testing
* Benchmarks

## License

All code is released under *"Do whatever you want"* license aka [Unlicense](LICENSE)