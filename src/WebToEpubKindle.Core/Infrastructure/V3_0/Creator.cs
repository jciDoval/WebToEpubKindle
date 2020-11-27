using System;
using System.IO;
using System.IO.Compression;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Versions.V3_0;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure.V3_0
{
    public class Creator : IFileCreator
    {
        private const string _epubExtension = ".epub";
        private const string _contentFileName = "content.opf";
        private const string _metaInfFile = "META-INF/container.xml";
        private const string _tocFile = "toc.ncx";
        private const string _mimetype = "mimetype";
        private const string _oebps = "OEBPS/";
        private Epub _epub;
        private HtmlConverter _htmlConverter;

        public Creator(Epub epub)
        {
            _epub = epub;
            _htmlConverter = new HtmlConverterV3_0();
        }

        public void Create(string path, string fileName)
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
                AddFileToZip(archive, _oebps + chapter.FileName, _htmlConverter.GetChapter(chapter));
            }
        }

        private void WriteTableOfContents(ZipArchive archive)
        {
            AddFileToZip(archive, _oebps + _tocFile, _htmlConverter.GetTableOfContent(_epub.TableOfContent));
        }

        private void WriteMimeTypeFile(ZipArchive archive)
        {
            AddFileToZip(archive, _mimetype, _htmlConverter.GetMimeType(_epub.MimeType));
        }

        private void WriteMetaInfFile(ZipArchive archive)
        {
            AddFileToZip(archive, _metaInfFile, _htmlConverter.GetMetaInf(_epub.MetaInf));
        }

        private void WriteContentFile(ZipArchive archive)
        {
            AddFileToZip(archive, _oebps + _contentFileName, _htmlConverter.GetContent(_epub.Content));
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
