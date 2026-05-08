# libktxsharp

Managed .NET 10/.NET 8 library for handling [KTX File Format](https://registry.khronos.org/KTX/specs/1.0/ktxspec.v1.html) and [KTX 2.0 file format](https://github.khronos.org/KTX-Specification/ktxspec.v2.html).

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