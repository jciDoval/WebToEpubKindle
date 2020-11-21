using WebToEpubKindle.Core.Interfaces;
namespace WebToEpubKindle.Core.Domain
{
public class MetaInf : IHtmlConvertible
    {
        private const string _metaInf = @"<?xml version=""1.0""?>
                                            <container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">
                                                <rootfiles>
                                                    <rootfile full-path=""content.opf"" media-type=""application/oebps-package+xml""/>
                                                </rootfiles>
                                            </container>";

        public string ToHtml()
        {
            return _metaInf;
        }
    }
}