using System.Collections.Generic;
using System.Text;
using WebToEpubKindle.Core.Interfaces;
namespace WebToEpubKindle.Core.Domain
{
    public class Page  : IHtmlConvertible
    {
        private const string _articleHtmlOpen = "<article>";
        private const string _articleHtmlClose = "</article>";
        private string _htmlBodyContent;
        private List<Image> _images;
        public List<Image> Images { get { return _images; }}
        

        private Page(string content, List<Image> images) 
        {
            _htmlBodyContent = content;
            _images = images;
        } 

        public static Page Create(string content, List<Image> images)
        {
            return new Page(content, images);
        }

        public string ToHtml()
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(_articleHtmlOpen);
            textBuilder.AppendLine(_htmlBodyContent);
            textBuilder.AppendLine(_articleHtmlClose);
            return textBuilder.ToString();
        }
    }
}
