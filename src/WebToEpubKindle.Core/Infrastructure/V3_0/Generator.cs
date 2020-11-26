using System;
using System.IO;
using System.IO.Compression;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure.V3_0
{
    public class Generator : IEpubGenerator
    {
        private const string _epubExtension = ".epub";
        private const string _contentFileName = "content.opf";
        private const string _metaInfFile = "META-INF/container.xml";
        private const string _tocFile = "toc.ncx";
        private const string _mimetype = "mimetype";
        private const string _oebps = "OEBPS/";
        private Epub _epub;
        public Generator(Epub epub)
        {
            _epub = epub;
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
                AddFileToZip(archive, _oebps + chapter.FileName, chapter.ToHtml());
            }
        }

        private void WriteTableOfContents(ZipArchive archive)
        {
            AddFileToZip(archive, _oebps + _tocFile, _epub.GetTableOfContent());
        }

        private void WriteMimeTypeFile(ZipArchive archive)
        {
            AddFileToZip(archive, _mimetype, _epub.GetMimeTypeContent());
        }

        private void WriteMetaInfFile(ZipArchive archive)
        {
            AddFileToZip(archive, _metaInfFile, _epub.GetMetaInfContent());
        }

        private void WriteContentFile(ZipArchive archive)
        {
            AddFileToZip(archive, _oebps + _contentFileName, _epub.GetContent());
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
