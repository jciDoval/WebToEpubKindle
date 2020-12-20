using System.Text;
using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Validation;
using WebToEpubKindle.Core.Properties;
using WebToEpubKindle.Core.Interfaces;
using System.Linq;
using WebToEpubKindle.Core.Domain.EpubComponents.Events;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Chapter
    {
        private const string Abr = "chap";
        
        private string _abbreviation;
        private readonly List<IEvent> _events = new List<IEvent>();
        private readonly string _fileName;
        private List<Page> _pages = new List<Page>();
        private int? _secuential;
        private readonly string _title;


        public string Abbreviation => _abbreviation;
        public IReadOnlyCollection<IEvent> Events => _events;
        public string FileName => _fileName;
        public bool HasImages => Images.Count() > 0;
        public List<Image> Images => _pages.SelectMany(x => x.Images).ToList();
        public List<Page> Pages => _pages;
        public string Title => _title;


        private Chapter(string title)
        {
            var identifier = Guid.NewGuid();
            _title = title;
            _fileName = identifier.ToString() + ".xhtml";
            _abbreviation = Abr;
        }

        private Chapter(string title, List<Page> pages) : this(title)
        {
            _pages = pages;
        }

        public static Chapter Create(string title)
        {
            return new Chapter(title);
        }

        public static Chapter Create(string title, List<Page> pages)
        {
            return new Chapter(title, pages);
        }

        public void AddPage(Page page)
        {
            Ensure.Argument.NotNull(page, CoreStrings.NullPage);
            Ensure.That(page.ValidateImageContent(), CoreStrings.PageInvalidImageContent);
            _pages.Add(page);
            _events.Add(PageAdded.Create());
        }

        public void AssignSecuential(int secuential)
        {
            _secuential = secuential;
            _abbreviation = Abr + _secuential;
        }

        public void RemovePage(Page page)
        {
            Ensure.Contains(_pages, x=>x.Identifier == page.Identifier, CoreStrings.PageIdentifierNotExist(page.Identifier));
            _pages.Remove(page);
            _events.Add(PageRemoved.Create());
        }
    }
}