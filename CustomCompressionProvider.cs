using Microsoft.AspNetCore.ResponseCompression;
using System.IO;

namespace ResponseCompression 
{
    public class CustomCompressionProvider : ICompressionProvider
    {
        public string EncodingName => "customCompression";
        public bool SupportsFlush => true;
        public Stream CreateStream(Stream outputStream)
        {
            return outputStream;
        }
    }

}