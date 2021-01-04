using System.Globalization;
using System.IO;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Interfaces;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Infrastructure;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebToEpubKindle.Core.Common;

namespace WebToEpubKindle.IntegrationTest
{
    public class EpubIntegrationTest
    {
        private readonly string _path = AppDomain.CurrentDomain.BaseDirectory;
        private const string _filename = "EpubTest";
        private readonly string _fullPath;
        private readonly Epub _epub;
        private readonly Page _page;
        private readonly Chapter _chapter;
        private readonly IFileEpubCreator _fileCreator;

        public EpubIntegrationTest()
        {
            _fullPath =  _path + "EpubTest.epub";
            _page = Page.Create("This is a test.", null);
            _chapter = Chapter.Create("Test");
            _epub = EpubFactory.Initialize(EpubVersion.V3_0, "Test Epub", new CultureInfo("en-EN")).BuildInstance();
            _fileCreator = FileEpubFactory.Initialize(EpubVersion.V3_0, _epub).BuildCreator();
        }

        [Fact]
        public void validate_epub_file_without_images_through_idpf_validator()
        {
            _chapter.AddPage(_page);
            _epub.ChapterList.AddChapter(_chapter);
            _fileCreator.Create(_path, _filename);

            bool result = new IdpfValidator(_fullPath).Validate();

            File.Delete(_fullPath);

            Assert.True(result);
        }

        [Fact]
        public void validate_epub_file_with_images_through_idpf_validator()
        {
            var page2 = Page.Create("this is a image page test. <img src=\"../images/kindle.jpg\" />",
                new List<Image>()
                {
                    Image.LoadImage(_path + @$"resources{Basics.DirectorySeparator}kindle.jpg").Result
                });
            _chapter.AddPage(page2);
            _epub.ChapterList.AddChapter(_chapter);
            _fileCreator.Create(_path, _filename);

            bool result = new IdpfValidator(_fullPath).Validate();
            File.Delete(_fullPath);
            
            Assert.True(result);
        }
    }
}
