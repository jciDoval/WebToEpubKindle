using System;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Chapter
    {
        private readonly Guid _identifier;
        public Guid Identifier 
        {
            get => _identifier;
        }
        public int NumberPages => Pages.Count;

        private readonly string _title;
        public string Title { get => _title;}
        public List<Page> Pages { get; }
        public Chapter(string title, List<Page> pages)
        {
            _identifier = Guid.NewGuid();
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
