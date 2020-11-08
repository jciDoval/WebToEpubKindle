using System;
using System.Collections.Generic;
using System.Text;

namespace WebToEpubKindle.Core.Domain
{
    public class TableOfContent
    {
        private string _epubTitle;
        private Dictionary<Guid, string> _nodes;

        public TableOfContent(string epubTitle)
        {
            _epubTitle = epubTitle;
            _nodes = new Dictionary<Guid, string>();
        }

        public void IndexChapter(Chapter chapter) => _nodes.Add(chapter.Identifier, chapter.Title);

        public void IndexChapter(List<Chapter> chapters)
        {
            chapters.ForEach(chapter => this.IndexChapter(chapter));
        }

        public override string ToString()
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            textBuilder.AppendLine(@"<ncx xmlns=""http://www.daisy.org/z3986/2005/ncx/"" version=""2005-1"">");
            textBuilder.AppendLine(@"<head>");
            textBuilder.AppendLine(@"<meta name=""cover"" content=""cover""/>");
            textBuilder.AppendLine(@"</head>");
            textBuilder.AppendLine(@"<docTitle>");
            textBuilder.AppendLine($@"<text>{_epubTitle}</text>");
            textBuilder.AppendLine(@"</docTitle>");
            textBuilder.AppendLine(@"<navMap>");
            int index = 1;
            foreach (var node in _nodes)
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


    }
}