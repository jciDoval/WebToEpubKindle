
using WebToEpubKindle.Core.Domain.EpubComponents;

namespace WebToEpubKindle.Core.Interfaces
{
    public interface IHtmlConverter
    {
        string GenerateContent(Content content);
        string GenerateChapter(Chapter chapter);
        string GenerateMetaInf(MetaInf metaInf);
        string GenerateMimeType(MimeType mimeType);
        string GeneratePage(Page page);
        string GenerateTableOfContent(TableOfContent tableOfContent);
        string GenerateTableOfContentXHTML(TableOfContent tableOfContent);

    }
}