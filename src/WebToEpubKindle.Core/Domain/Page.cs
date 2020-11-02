namespace WebToEpubKindle.Core.Domain
{
    public class Page
    {
        private string _content;
        public string Content
        {
            get { return _content; }

            private set
            {
                _content = value;
            }
        }

        public Page(string content) => Content = content;

    }
}
