using System;
using System.Text;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Domain.Versions.V3_0
{
    public class HtmlConverterV3_0 : IHtmlConverter
    {
        public string GenerateContent(Content content)
        {
            StringBuilder spineRefs = new StringBuilder();
            StringBuilder textBuilder = new StringBuilder();

            textBuilder.AppendLine(@"<?xml version=""1.0""  encoding=""UTF-8""?>");
            textBuilder.AppendLine(@"<package xmlns=""http://www.idpf.org/2007/opf"" xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:dcterms=""http://purl.org/dc/terms/"" unique-identifier=""pub-identifier"" version=""3.0"">");
            textBuilder.AppendLine(@"<metadata>");
            textBuilder.AppendLine($@"<dc:identifier id=""pub-identifier"">{Guid.NewGuid().ToString()}</dc:identifier>");
            textBuilder.AppendLine($@"<dc:title id=""pub-title"">{content.Title}</dc:title>");
            textBuilder.AppendLine($@"<dc:creator>{content.Creator}</dc:creator>");
            textBuilder.AppendLine($@"<dc:date>{DateTime.Now:yyyy-MM-dd}</dc:date>");
            textBuilder.AppendLine($@"<dc:language>{content.Language}</dc:language>");
            textBuilder.AppendLine($@"<meta property=""dcterms:modified"">{DateTime.Now:yyyy-MM-ddTHH:mm:ssZ}</meta>");
            textBuilder.AppendLine("</metadata>");
            textBuilder.AppendLine("<manifest>");
            textBuilder.AppendLine(@"<item id=""nav"" href=""toc.xhtml"" media-type=""application/xhtml+xml""  properties=""nav"" />");

            foreach (var imageItem in content.ImageItems)
                textBuilder.AppendLine($@"<item href=""{imageItem.Href}.xhtml"" id=""{imageItem.Id}"" media-type=""{imageItem.MediaType}"" />");

            foreach (var fileItem in content.FileItems)
            {
                spineRefs.AppendLine($@"<itemref idref=""{fileItem.Id}"" />");
                textBuilder.AppendLine($@"<item href=""{fileItem.Href}"" id=""{fileItem.Id}"" media-type=""{fileItem.MediaType}"" />");
            }
            
            textBuilder.AppendLine(@"<item href=""toc.ncx"" id=""ncx"" media-type=""application/x-dtbncx+xml""/>");
            textBuilder.AppendLine("</manifest>");
            textBuilder.AppendLine(@"<spine toc=""ncx"">");
            textBuilder.Append(spineRefs.ToString());
            textBuilder.AppendLine("</spine>");
            textBuilder.AppendLine("</package>");
            return textBuilder.ToString();
        }



        public string GenerateChapter(Chapter chapter)
        {
            StringBuilder _textBuilder = new StringBuilder();
            _textBuilder.Clear();
            _textBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>");
            _textBuilder.AppendLine("<!DOCTYPE html>");
            _textBuilder.AppendLine(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:epub=""http://www.idpf.org/2007/ops"">");
            _textBuilder.AppendLine("<head>");
            _textBuilder.AppendFormat("<title>{0}</title>", chapter.Title);
            _textBuilder.AppendLine();
            _textBuilder.AppendLine(@"<meta charset=""utf-8""/>");
            _textBuilder.AppendLine("</head>");
            _textBuilder.AppendLine("<body>");
            _textBuilder.AppendLine("<section>");
            _textBuilder.AppendLine("<header>");
            _textBuilder.AppendFormat("<h1>{0}</h1>", chapter.Title);
            _textBuilder.AppendLine();
            _textBuilder.AppendLine("</header>");
            chapter.Pages.ForEach(page => _textBuilder.AppendLine(this.GeneratePage(page)));            
            _textBuilder.AppendLine("</section>");
            _textBuilder.AppendLine("</body>");
            _textBuilder.AppendLine("</html>");
            return _textBuilder.ToString();
        }

        public string GenerateMetaInf(MetaInf metaInf)
        {
            return $@"<?xml version=""1.0""?>
                                            <container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">
                                                <rootfiles>
                                                    <rootfile full-path=""{metaInf.FullPath}"" media-type=""{metaInf.MediaType}""/>
                                                </rootfiles>
                                            </container>";
        }

        public string GenerateMimeType(MimeType mimeType)
        {
            return mimeType.Format;
        }

        public string GeneratePage(Page page)
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine("<article>");
            textBuilder.AppendLine(page.Content);
            textBuilder.AppendLine("</article>");
            return textBuilder.ToString();
        }

        public string GenerateTableOfContent(TableOfContent tableOfContent)
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            textBuilder.AppendLine(@"<ncx xmlns=""http://www.daisy.org/z3986/2005/ncx/"" version=""2005-1"">");
            textBuilder.AppendLine(@"<head>");
            textBuilder.AppendLine(@"<meta name=""cover"" content=""cover""/>");
            textBuilder.AppendLine(@"</head>");
            textBuilder.AppendLine(@"<docTitle>");
            textBuilder.AppendLine($@"<text>{tableOfContent.Title}</text>");
            textBuilder.AppendLine(@"</docTitle>");
            textBuilder.AppendLine(@"<navMap>");
            int index = 1;
            foreach (var node in tableOfContent.Nodes)
            {
                textBuilder.AppendLine($@"<navPoint id=""{node.Abbreviation}"" playOrder=""{index}"">");
                textBuilder.AppendLine("<navLabel>");
                textBuilder.AppendLine($"<text>{node.Title}</text>");
                textBuilder.AppendLine("</navLabel>");
                textBuilder.AppendLine($@"<content src=""{node.Src}""/>");
                textBuilder.AppendLine("</navPoint>");
                index++;
            }
            textBuilder.AppendLine("</navMap>");
            textBuilder.AppendLine("</ncx>");
            return textBuilder.ToString();
        }

        public string GenerateTableOfContentXHTML(TableOfContent tableOfContent)
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""  standalone=""no""?>");
            textBuilder.AppendLine(@"<!DOCTYPE html>");
            textBuilder.AppendLine(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:epub=""http://www.idpf.org/2007/ops"">");
            textBuilder.AppendLine(@"<head>");
            textBuilder.AppendLine(@"<title>Table of Contents</title>");
            textBuilder.AppendLine(@"</head>");
            textBuilder.AppendLine(@"<body>");
            textBuilder.AppendLine(@"<section>");
            textBuilder.AppendLine(@"<header><h1>Table of Contents</h1></header>");
            textBuilder.AppendLine(@"<article>");
            textBuilder.AppendLine(@"<nav epub:type=""toc"" id=""toc"">");
            textBuilder.AppendLine(@"<ol>");

            foreach (var node in tableOfContent.Nodes)
            {
                textBuilder.AppendLine($@"<li>");
                textBuilder.AppendLine($@"<a href=""{node.Src}"">{node.Title}</a>");
                textBuilder.AppendLine($"</li>");                
            }

            textBuilder.AppendLine("</ol>");
            textBuilder.AppendLine("</nav>");
            textBuilder.AppendLine("</article>");
            textBuilder.AppendLine("</section>");
            textBuilder.AppendLine("</body>");
            textBuilder.AppendLine("</html>");
            return textBuilder.ToString();
        }
    }
}
