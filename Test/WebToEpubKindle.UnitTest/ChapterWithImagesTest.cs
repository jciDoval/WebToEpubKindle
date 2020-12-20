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
        private string _title = "TEST Chapter with images";
        private string _pageContent = "Lorem ipsum dolor sit amet, consectetur <img src=\"kindle.jpg\" /> adipiscing elit";
        private Chapter _chapter;
        private Page _page;

        public ChapterWithImagesTest()
        {
            Image image = Image.LoadImage("kindle.jpg", AppDomain.CurrentDomain.BaseDirectory + @"resources\kindle.jpg").Result;
            _chapter = Chapter.Create(_title);
            _page = Page.Create(_pageContent, new List<Image>(){image});
        }

        [Fact]
        public void allow_add_page_to_the_chapter()
        {
            _chapter.AddPage(_page);
            _chapter.Events.Last().Should().BeOfType<PageAdded>();            
        }

        [Fact]
        public void not_allow_remove_non_existing_page()
        {
            Action result = () => _chapter.RemovePage(_page);
            result.Should().Throw<Exception>().WithMessage(CoreStrings.PageIdentifierNotExist(_page.Identifier));
        }

        [Fact]
        public void allow_remove_existing_page()
        {
            _chapter.AddPage(_page);
            _chapter.RemovePage(_page);
            _chapter.Events.Last().Should().BeOfType<PageRemoved>();
        }
    }
}
