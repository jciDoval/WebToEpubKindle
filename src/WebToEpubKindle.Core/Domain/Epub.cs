using System;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Epub
    {
        private const string _metaInf = @"<?xml version=""1.0""?><container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">   <rootfiles>     <rootfile full-path=""content.opf"" media-type=""application/oebps-package+xml""/>         </rootfiles></container>    ";
        private const string mimetype = "application/epub+zip";
        private Dictionary<Guid,string> _tableOfContent;
        private List<Chapter> _chapters;

        private Epub(List<Chapter> chapters)
        {
            _chapters = chapters;
            _tableOfContent = new Dictionary<Guid, string>();
            LoadTableOfContents();
        }

        private void LoadTableOfContents()
        {
            foreach (var chapter in _chapters)
            {
                _tableOfContent.Add(chapter.Identifier, chapter.Title);
            }
        }

        public static Epub Create(List<Chapter> chapters)
        {
           return new Epub(chapters);
        }
        
        public void AddChapter(Chapter chapter)
        {
            _chapters.Add(chapter);
            _tableOfContent.Add(chapter.Identifier, chapter.Title);
        }
    }
}
