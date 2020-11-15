using WebToEpubKindle.Core.Interfaces;
namespace WebToEpubKindle.Core.Domain
{
    public class MimeType : IHtmlConvertible
    {
        private const string _mime = @"application/epub+zip";

        public MimeType(){}

        public string ToHtml()
        {
            return _mime;
        }
    }
}