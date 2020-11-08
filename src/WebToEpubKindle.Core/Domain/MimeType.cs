namespace WebToEpubKindle.Core.Domain
{
    public class MimeType
    {
        private const string _mime = @"application/epub+zip";

        public MimeType(){}

        public override string ToString()
        {
            return _mime;
        }
    }
}