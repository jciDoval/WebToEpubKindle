namespace WebToEpubKindle.Core.Domain
{
    public class Page
    {
        private string _htmlBodyContent;
        public string HtmlBodyContent
        {
            get { return _htmlBodyContent; }
        }

        public Page(string content) => _htmlBodyContent = content;
    }
}
