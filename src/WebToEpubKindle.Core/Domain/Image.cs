namespace WebToEpubKindle.Core.Domain
{
    public class Image
    {
        private byte[] _bytes;
        private string _fileName;
        private string _mediaType;
        private string _relativePath;

        public Image(string relativePath, string mediaType, byte[] bytes, string fileName)
        {
            _bytes = bytes;
            _fileName = fileName;
            _mediaType = mediaType;
            _relativePath  = relativePath;
            
        }
    }
}