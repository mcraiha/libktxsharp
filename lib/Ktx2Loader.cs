using System;
using System.IO;

namespace KtxSharp;

/// <summary>
/// Load Ktx2 (.ktx2 files) input static class
/// </summary>
public static class Ktx2Loader
{
	/// <summary>
	/// Check if input is valid KTX2
	/// </summary>
	/// <param name="stream">Stream to check (must be seekable stream)</param>
	/// <returns>Tuple that tells if input is valid, and possible error message</returns>
	public static (bool isValid, string possibleError) CheckIfInputIsValid(Stream stream)
	{
		// Currently only header and metadata are validated properly, so texture data can still contain invalid values
		(bool isStreamValid, string possibleStreamError) = KtxValidators.GenericStreamValidation(stream);
		if (!isStreamValid)
		{
			return (isValid: false, possibleError: possibleStreamError);
		}

		// We have to duplicate the data, since we have to both validate it and keep it for texture data validation step
		long streamPos = stream.Position;

		/*(bool isIdentifierValid, string possibleIdentifierError) = KtxValidators.ValidateIdentifier(stream);
		if (!isIdentifierValid)
		{
			return (isValid: false, possibleError: possibleIdentifierError);
		}

		(bool isHeaderValid, string possibleHeaderError) = KtxValidators.ValidateKtx2HeaderData(stream);
		if (!isHeaderValid)
		{
			return (isValid: false, possibleError: possibleHeaderError);
		}*/

		stream.Position = streamPos;
		

		return (isValid: true, possibleError: "");
	}

	/// <summary>
	/// Load Ktx2Structure from stream
	/// </summary>
	/// <param name="stream">Stream to read (must be seekable stream)</param>
	/// <param name="seekFromCurrent">Seek from current position (use this if your stream is not a single .ktx file)</param>
	/// <returns><see cref="Ktx2Structure"/></returns>
	public static Ktx2Structure LoadInput(Stream stream, bool seekFromCurrent = false)
	{
		// First we read the header
		Ktx2Header header = new Ktx2Header(stream, seekFromCurrent);

		// Then Supercompression data
		Ktx2Supercompression supercompression = new Ktx2Supercompression(stream, header.sgdByteLength);

		// Finally texture data
		Ktx2TextureData textureData = new Ktx2TextureData(stream, header.levelIndexes);

		// And combine those to one structure
		return new Ktx2Structure(header, supercompression, textureData);
	}
}
