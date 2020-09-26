using System;
using System.IO;

namespace KtxSharp
{
    /// <summary>
    /// Write Ktx output static class
    /// </summary>
    public static class KtxWriter
    {
        /// <summary>
        /// Write KtxStructure to output stream
        /// </summary>
        /// <param name="structure">KtxStructure</param>
        /// <param name="output">Output stream</param>
        public static void WriteTo(KtxStructure structure, Stream output)
        {
            if (structure == null)
            {
                throw new NullReferenceException("Structure cannot be null");
            }

            if (output == null)
            {
                throw new NullReferenceException("Output stream cannot be null");
            }
            else if (!output.CanWrite)
            {
                throw new ArgumentException("Output stream must be writable");
            }

            output.Write(Common.onlyValidIdentifier, 0, Common.onlyValidIdentifier.Length);
            structure.header.WriteTo(output);
            structure.textureData.WriteTo(output);
        }
    }
}