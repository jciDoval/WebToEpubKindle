using System.IO.Compression;
using System.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using WebToEpubKindle.Core.Domain;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Infrastructure
{
    public class EpubWriter
    {
        private const string _metaInf = @"<?xml version=""1.0""?><container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">   <rootfiles>     <rootfile full-path=""content.opf"" media-type=""application/oebps-package+xml""/>         </rootfiles></container>    ";
        private const string _mimetype = "application/epub+zip";
        private const string _chapterExtension  = ".xhtml";
        private const string _epubExtension = ".epub";
        private Epub _epub;

        private EpubWriter(Epub epub)
        {
            this._epub = epub;
        }

        public static EpubWriter Initialize(Epub epub)
        {
            return new EpubWriter(epub);
        }

        public void CreateEpub(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path)) 
                path = AppDomain.CurrentDomain.BaseDirectory;

            string completePath = $@"{path}\{fileName}{_epubExtension}";

            using (var memoryStream = new MemoryStream())
            {
                CreateEpubZipFile(memoryStream);
                WriteEpubToDisk(completePath, memoryStream);
            }
        }

        private void CreateEpubZipFile(MemoryStream memoryStream)
        {
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                CreateChapterFiles(archive);
                CreateTableOfContents(archive);
            }
        }

        private static void WriteEpubToDisk(string completePath, MemoryStream memoryStream)
        {
            using (var epub = new FileStream(completePath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(epub);
            }
        }

        private void CreateChapterFiles(ZipArchive archive)
        { 
            foreach (var chapter in _epub.Chapters)
            {
                var chapterFile = archive.CreateEntry(chapter.Identifier.ToString() + _chapterExtension);
                using (var chapterContent = chapterFile.Open())
                using (var writer = new StreamWriter(chapterContent))
                {
                    writer.Write(chapter.ToString());
                }
            }
        }

        private static void CreateTableOfContents(ZipArchive archive)
        {
           var toc = archive.CreateEntry("toc.xhtml");
           using(var tocContent = toc.Open())
           using(var writer = new StreamWriter(tocContent))
           {
               writer.Write("Hola");
           }
        }
    }

}


