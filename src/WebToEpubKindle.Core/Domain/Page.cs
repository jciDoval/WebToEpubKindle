using System.Text;
namespace WebToEpubKindle.Core.Domain
{
    public class Page
    {
        private const string _articleHtmlOpen = "<article>";
        private const string _articleHtmlClose = "</article>";
        private string _htmlBodyContent;

        public Page(string content) => _htmlBodyContent = content;

        public override string ToString()
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(_articleHtmlOpen);
            textBuilder.AppendLine(_htmlBodyContent);
            textBuilder.AppendLine(_articleHtmlClose);
            return textBuilder.ToString();
        }
    }
}
