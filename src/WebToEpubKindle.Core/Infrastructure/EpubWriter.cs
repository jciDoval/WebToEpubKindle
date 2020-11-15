using System.IO.Compression;
using System;
using System.IO;
using WebToEpubKindle.Core.Domain;

namespace WebToEpubKindle.Core.Infrastructure
{
    public class EpubWriter
    {
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
                WriteChapterFiles(archive);
                WriteMimeTypeFile(archive);
                WriteTableOfContents(archive);
                WriteMetaInfFile(archive);
                WriteContentFile(archive);
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

        private void WriteChapterFiles(ZipArchive archive)
        { 
            foreach (var chapter in _epub.ChapterList.Chapters)
            {
                string fullFileName = chapter.Identifier.ToString() + _chapterExtension;
                AddFileToZip(archive, fullFileName, chapter.ToHtml());
            }
        }

        private void WriteTableOfContents(ZipArchive archive)
        {
            AddFileToZip(archive, "toc.ncx", _epub.GetTableOfContent());
        }

        private void WriteMimeTypeFile(ZipArchive archive)
        {
            AddFileToZip(archive, "mimetype", _epub.GetMimeTypeContent());
        }

        private void WriteMetaInfFile(ZipArchive archive)
        {
            AddFileToZip(archive, "META_INF/container.xml", _epub.GetMetaInfContent());
        }

        private void WriteContentFile(ZipArchive archive)
        {

        }

        private static void AddFileToZip(ZipArchive archive, string fileName, string contentFile)
        {
            var file = archive.CreateEntry(fileName);
            using (var fileContent = file.Open())
            using (var writer = new StreamWriter(fileContent))
            {
                writer.Write(contentFile);
            }
        }
    }

}


