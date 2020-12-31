using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Domain.EpubComponents.Events;
using WebToEpubKindle.Core.Properties;
using Xunit;

namespace WebToEpubKindle.UnitTest
{

    public class ChapterWithImagesTest
    {
        private const string _title = "TEST Chapter with images";
        private string _pageContent = "Lorem ipsum dolor sit amet, consectetur <img src=\"../images/kindle.jpg\" /> adipiscing elit";
        private readonly Chapter _chapter;
        private Page _page;
        private readonly Image _image;

        public ChapterWithImagesTest()
        {
            _image = Image.LoadImage("kindle.jpg", ".jpg", new byte[1]);
            _chapter = Chapter.Create(_title);
        }

        [Fact]
        public void add_page_to_the_chapter_with_srcimage_inside_pagecontent_should_be_added()
        {
            _page = Page.Create(_pageContent, new List<Image>(){_image});
            _chapter.AddPage(_page);
            
            _chapter.Events.Last().Should().BeOfType<PageAdded>();
        }
        
        [Fact]
        public void add_page_to_the_chapter_without_srcimage_inside_pagecontent_should_throw_exception()
        {
            _pageContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            _page = Page.Create(_pageContent, new List<Image>(){_image});
            Action result = () => _chapter.AddPage(_page);
            
            result.Should().Throw<Exception>().WithMessage(CoreStrings.PageInvalidImageContent);
        }

        [Fact]
        public void remove_non_existing_page_should_throw_exception()
        {
            _page = Page.Create(_pageContent, new List<Image>(){_image});
            Action result = () => _chapter.RemovePage(_page);
            
            result.Should().Throw<Exception>().WithMessage(CoreStrings.PageIdentifierNotExist(_page.Identifier));
        }

        [Fact]
        public void remove_existing_page_should_be_removed()
        {
            _page = Page.Create(_pageContent, new List<Image>(){_image});
            _chapter.AddPage(_page);
            _chapter.RemovePage(_page);
            
            _chapter.Events.Last().Should().BeOfType<PageRemoved>();
        }
    }
}
