using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Chapter
    {
        private string title;

        public int NumberPages => Pages.Count;

        private string _title;
        public string Title { get => _title; set => title = value; }
        public List<Page> Pages { get; }
        public Chapter(string title, List<Page> pages)
        {
            _title = title;
            Pages = pages;
        }

        public void AddPage(Page page)
        {
            Pages.Add(page);
        }

        public void DeletePage(int pagePosition)
        {
            Pages.RemoveAt(pagePosition);
        }
    }
}
