using System.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using WebToEpubKindle.Core.Domain;

namespace WebToEpubKindle.Core.Infrastructure
{

    public class EpubWriter
    {
        private const string _metaInf = @"<?xml version=""1.0""?><container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">   <rootfiles>     <rootfile full-path=""content.opf"" media-type=""application/oebps-package+xml""/>         </rootfiles></container>    ";
        private const string _mimetype = "application/epub+zip";

        private Epub _epub;
        private string _path;

        private EpubWriter(Epub epub)
        {
            this._epub = epub;
        }

        public static EpubWriter Create(Epub epub)
        {
            return new EpubWriter(epub);
        }

        public void WriteToDisk(string path, string fileName)
        {
            using (var file = new FileStream(AppDomain.CurrentDomain.BaseDirectory + fileName,FileMode.Create, FileAccess.Write))
            {
                var fileBytes = BuildEpubToFile();
                file.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        private byte[] BuildEpubToFile()
        {
            var pages = _epub.Chapters.SelectMany(x=>x.Pages).ToList();
            StringBuilder contentAllPages = new StringBuilder();
            pages.ForEach(x=>contentAllPages.AppendLine(x.HtmlBodyContent));
            return Encoding.UTF8.GetBytes(contentAllPages.ToString());
        }

        private static byte[] BuildTableOfContents()
        {
            return null;
        }
    }

}


