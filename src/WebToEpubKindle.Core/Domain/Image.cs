using System;

namespace WebToEpubKindle.Core.Domain
{
    public class Image
    {
        private byte[] _bytes;
        public byte[] Bytes { get { return _bytes; } }
        private Guid _guid;
        public string Id { get { return _guid.ToString(); } }
        private string _mediaType;
        public string MediaType { get { return _mediaType; } }
        private string _extension;
        public string Extension { get { return _extension; } }

        public Image(string mediaType, byte[] bytes, string extension)
        {
            _bytes = bytes;
            _guid = Guid.NewGuid();
            _mediaType = mediaType;
            _extension = extension;
        }
    }
}