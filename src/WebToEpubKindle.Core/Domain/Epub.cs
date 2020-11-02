using System;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Epub
    {
        private const string _metaInf = @"<?xml version=""1.0""?><container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">   <rootfiles>     <rootfile full-path=""content.opf"" media-type=""application/oebps-package+xml""/>         </rootfiles></container>    ";
        private const string mimetype = "application/epub+zip";
        private Dictionary<Guid,string> _tableOfContent;
        private Dictionary<Guid,Chapter> _chapters;

        private Epub(List<Chapter> chapters)
        {
            //_chapters = chapters;
        }

        public static Epub Create(List<Chapter> chapters)
        {
            Epub epub = new Epub(chapters);
            return epub;
        }
        
        public static void AddChapter(Chapter chapter)
        {

        }
    }

}
