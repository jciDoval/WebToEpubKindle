using FluentAssertions;
using System.Linq;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Domain.EpubComponents.Events;
using Xunit;

namespace WebToEpubKindle.UnitTest
{

    public class ChapterTest
    {
        [Fact]
        public void Chapter_AddPage_PageAdded()
        {
            const string title = "TEST";
            const string content = "This is the content of the page";
            Page page = Page.Create(content,null);
            Chapter chapter = new Chapter(title);

            chapter.AddPage(page);

            chapter.Events.Last().Should().BeOfType<PageAdded>();
            
        }
    }
}
