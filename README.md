# libktxsharp

Managed .NET 10/.NET 8 library for handling [KTX File Format](https://registry.khronos.org/KTX/specs/1.0/ktxspec.v1.html) and [KTX 2.0 file format](https://github.khronos.org/KTX-Specification/ktxspec.v2.html).

## Build status

![](https://github.com/mcraiha/libktxsharp/workflows/CIBuild/badge.svg)

## Nuget

[https://www.nuget.org/packages/LibKTX/](https://www.nuget.org/packages/LibKTX/)

## Why

Because KTX specs are public and I needed something like this for my canceled project

## How to use

1. Get nuget, build .dll or include [lib folder](lib) in your project
2. Use following code examples

**KTX**
```csharp
using KtxSharp;

byte[] ktxBytes = File.ReadAllBytes("myImage.ktx");

KtxStructure ktxStructure = null;
using (MemoryStream ms = new MemoryStream(ktxBytes))
{
	ktxStructure = KtxLoader.LoadInput(ms);
}

Console.WriteLine(ktxStructure.header.pixelWidth);
```

**KTX2** (standalone classes)
```csharp
using KtxSharp;

byte[] ktx2Bytes = File.ReadAllBytes("myImage.ktx2");

Ktx2Structure ktx2Structure = null;
using (MemoryStream ms = new MemoryStream(ktx2Bytes))
{
	ktx2Structure = Ktx2Loader.LoadInput(ms);
}

Console.WriteLine(ktx2Structure.ktx2Header.pixelWidth);
```

**KTX2** (view over existing memory)
```csharp
using KtxSharp;

byte[] ktx2Bytes = File.ReadAllBytes("myImage.ktx2");

Ktx2View ktx2View = new Ktx2View(ktx2Bytes);

Console.WriteLine(ktx2View.GetPixelWidth());
```

## How do I build this

### Requirements

.NET 8 (or newer) environment

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