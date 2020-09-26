using System;
using System.IO;

namespace KtxSharp
{
    /// <summary>
    /// Load Ktx input static class
    /// </summary>
    public static class KtxLoader
    {
        
        /// <summary>
        /// Check if input is valid
        /// </summary>
        /// <param name="memoryStream">Memorystream to check</param>
        /// <returns>Tuple that tells if input is valid, and possible error message</returns>
        public static (bool isValid, string possibleError) CheckIfInputIsValid(MemoryStream memoryStream)
        {
            // Currently only header and metadata are validated properly, so texture data can still contain invalid values
            (bool isStreamValid, string possibleStreamError) = KtxValidators.GenericStreamValidation(memoryStream);
            if (!isStreamValid)
            {
                return (isValid: false, possibleError: possibleStreamError);
            }

            // We have to duplicate the data, since we have to both validate it and keep it for texture data validation step
            long memoryStreamPos = memoryStream.Position;

            (bool isHeaderValid, string possibleHeaderError) = KtxValidators.ValidateHeaderData(memoryStream);
            if (!isHeaderValid)
            {
                return (isValid: false, possibleError: possibleHeaderError);
            }

            memoryStream.Position = memoryStreamPos;
            KtxHeader tempHeader = new KtxHeader(memoryStream);

            (bool isTextureDataValid, string possibleTextureDataError) = KtxValidators.ValidateTextureData(memoryStream, tempHeader, (uint)(memoryStream.Length - memoryStream.Position));

            return (isValid: isTextureDataValid, possibleError: possibleTextureDataError);
        }

        /// <summary>
        /// Load KtxStructure from memorystream
        /// </summary>
        /// <param name="memoryStream">Memorystream to read</param>
        /// <returns>KtxStructure</returns>
        public static KtxStructure LoadInput(MemoryStream memoryStream)
        {
            // First we read the header
            KtxHeader header = new KtxHeader(memoryStream);
            // Then texture data
            KtxTextureData textureData = new KtxTextureData(header, memoryStream);
            // And combine those to one structure
            return new KtxStructure(header, textureData);
        }
    }
}
