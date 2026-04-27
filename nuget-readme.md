# libktxsharp

C# library for handling [KTX File Format](https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/).

## How to use

1. Get nuget, build .dll or include [lib folder](lib) in your project
2. Use following code example
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