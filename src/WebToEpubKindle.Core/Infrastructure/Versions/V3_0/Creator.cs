using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure.Versions.V3_0
{
    public class Creator : IFileEpubCreator
    {
        private const string _epubExtension = ".epub";
        private const string _contentFileName = "content.opf";
        private const string _metaInfFile = "META-INF/container.xml";
        private const string _tocFile = "toc.ncx";
        private const string _tocFileXHTML = "toc.xhtml";
        private const string _mimetype = "mimetype";
        private const string _pathOebps = "OEBPS/";
        private const string _pathImage = "images/";
        private Epub _epub;
        private IHtmlConverter _htmlConverter;

        public Creator(Epub epub, IHtmlConverter htmlConverter)
        {
            _epub = epub;
            _htmlConverter = htmlConverter;
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
                WriteMimeTypeFile(archive); //Epub needs mimetype file will be the first to be created.
                WriteChapterFiles(archive);
                WriteTableOfContents(archive);
                WriteTableOfContentsXHTML(archive);
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
                AddFileToZip(archive, _pathOebps + chapter.FileName, _htmlConverter.GenerateChapter(chapter));
            }

            foreach (var image in _epub.ChapterList.Images)
            {
                AddFileToZip(archive, _pathImage + image.EpubFileName, image.Bytes);
            }
        }

        private void WriteTableOfContents(ZipArchive archive)
        {
            AddFileToZip(archive, _pathOebps + _tocFile, _htmlConverter.GenerateTableOfContent(_epub.TableOfContent));
        }

        private void WriteTableOfContentsXHTML(ZipArchive archive)
        {
            AddFileToZip(archive, _pathOebps + _tocFileXHTML, _htmlConverter.GenerateTableOfContentXHTML(_epub.TableOfContent));
        }

        private void WriteMimeTypeFile(ZipArchive archive)
        {
            AddFileToZip(archive, _mimetype, _htmlConverter.GenerateMimeType(_epub.MimeType), CompressionLevel.NoCompression);
        }

        private void WriteMetaInfFile(ZipArchive archive)
        {
            AddFileToZip(archive, _metaInfFile, _htmlConverter.GenerateMetaInf(_epub.MetaInf));
        }

        private void WriteContentFile(ZipArchive archive)
        {
            AddFileToZip(archive, _pathOebps + _contentFileName, _htmlConverter.GenerateContent(_epub.Content));
        }

        private static void AddFileToZip(ZipArchive archive, string fileName, string contentFile, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            var file = archive.CreateEntry(fileName, compressionLevel);
            using (var fileContent = file.Open())
            using (var writer = new StreamWriter(fileContent))
            {
                writer.Write(contentFile);
            }
        }

        private static void AddFileToZip(ZipArchive archive, string fileName, byte[] contentFile,
            CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            var file = archive.CreateEntry(fileName, compressionLevel);
            using (var fileContent = file.Open())
            using (var writer = new BinaryWriter(fileContent))
            {
                writer.Write(contentFile);
            }
        }
    }
}
