using System.Text;
using WebToEpubKindle.Core.Domain.EpubComponents;

namespace WebToEpubKindle.Core.Domain
{
    public abstract class HtmlConverter
    {
        public abstract string GetContent(Content content);

        public abstract string GetChapter(Chapter chapter);

        public virtual string GetMetaInf(MetaInf metaInf)
        {
            return $@"<?xml version=""1.0""?>
                                            <container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">
                                                <rootfiles>
                                                    <rootfile full-path=""{metaInf.FullPath}"" media-type=""{metaInf.MediaType}""/>
                                                </rootfiles>
                                            </container>";
        }

        public virtual string  GetMimeType(MimeType mimeType)
        {
            return mimeType.Format;
        }

        public abstract string GetTableOfContent(TableOfContent tableOfContent);

        public virtual string GetPage(Page page)
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine("<article>");
            textBuilder.AppendLine(page.Content);
            textBuilder.AppendLine("</article>");
            return textBuilder.ToString();
        }
        
    }
}
