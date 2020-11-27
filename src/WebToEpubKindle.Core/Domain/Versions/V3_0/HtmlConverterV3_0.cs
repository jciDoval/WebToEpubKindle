using System;
using System.Text;
using WebToEpubKindle.Core.Domain.EpubComponents;

namespace WebToEpubKindle.Core.Domain.Versions.V3_0
{
    public class HtmlConverterV3_0 : HtmlConverter
    {
        public override string GetContent(Content content)
        {
            StringBuilder spineRefs = new StringBuilder();
            StringBuilder textBuilder = new StringBuilder();

            textBuilder.AppendLine(@"<?xml version=""1.0""  encoding=""UTF-8""?>");
            textBuilder.AppendLine(@"<package xmlns=""http://www.idpf.org/2007/opf"" xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:dcterms=""http://purl.org/dc/terms/"" unique-identifier=""pub-identifier"" version=""3.0"">");
            textBuilder.AppendLine(@"<metadata>");
            textBuilder.AppendLine($@"<dc:identifier id=""pub-identifier"">{Guid.NewGuid().ToString()}</dc:identifier>");
            textBuilder.AppendLine($@"<dc:title id=""pub-title"">{content.Title}</dc:title>");
            textBuilder.AppendLine($@"<dc:creator>{content.Creator}</dc:creator>");
            textBuilder.AppendLine($@"<dc:date>{DateTime.Now.ToString("yyyy-MM-dd")}</dc:date>");
            textBuilder.AppendLine($@"<dc:language>{content.Language}</dc:language>");
            textBuilder.AppendLine($@"<meta property=""dcterms:modified"">{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ")}</meta>");
            textBuilder.AppendLine("</metadata>");
            textBuilder.AppendLine("<manifest>");
            
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

        public override string GetTableOfContent(TableOfContent tableOfContent)
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
                textBuilder.AppendLine($@"<navPoint id=""{node.Key.ToString()}"" playOrder=""{index.ToString()}"">");
                textBuilder.AppendLine("<navLabel>");
                textBuilder.AppendLine($"<text>{node.Value}</text>");
                textBuilder.AppendLine("</navLabel>");
                textBuilder.AppendLine($@"<content src=""{node.Key.ToString() }.xhtml""/>");
                textBuilder.AppendLine("</navPoint>");
                index++;
            }
            textBuilder.AppendLine("</navMap>");
            textBuilder.AppendLine("</ncx>");
            return textBuilder.ToString();
        }

        public override string GetChapter(Chapter chapter)
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
            chapter.Pages.ForEach(page => _textBuilder.AppendLine(this.GetPage(page)));            
            _textBuilder.AppendLine("</section>");
            _textBuilder.AppendLine("</body>");
            _textBuilder.AppendLine("</html>");
            return _textBuilder.ToString();
        }

        
    }
}
