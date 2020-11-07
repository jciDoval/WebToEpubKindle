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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_articleHtmlOpen);
            sb.AppendLine(_htmlBodyContent);
            sb.AppendLine(_articleHtmlClose);
            return sb.ToString();
        }
    }
}
