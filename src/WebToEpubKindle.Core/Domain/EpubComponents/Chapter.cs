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
        private const string _abr = "chap";
        private const string _extension = ".xhtml";
        private string _abbreviation;
        private readonly List<IEvent> _events = new List<IEvent>();
        private readonly Guid _identifier;
        private readonly string _fileName;
        private List<Page> _pages = new List<Page>();
        private int? _secuential;
        private readonly string _title;



        public string Abbreviation { get => _abbreviation; }
        public IReadOnlyCollection<IEvent> Events => _events;
        public string FileName { get => _fileName; }
        public bool HasImages { get => Images.Count() > 0; }
        public List<Image> Images { get => _pages.SelectMany(x => x.Images).ToList(); }
        public List<Page> Pages { get => _pages; }
        public string Title { get => _title; }


        public Chapter(string title)
        {
            _identifier = Guid.NewGuid();
            _title = title;
            _fileName = _identifier.ToString() + _extension;
            _abbreviation = _abr;
        }

        public Chapter(string title, List<Page> pages) : this(title)
        {
            _pages = pages;
        }

        public void AddPage(Page page)
        {
            Ensure.Argument.NotNull(page, CoreStrings.NullPage);
            _pages.Add(page);
            _events.Add(PageAdded.Create());
        }

        public void AssignSecuential(int secuential)
        {
            _secuential = secuential;
            _abbreviation = _abr + _secuential;
        }

        public void DeletePage(int pagePosition)
        {
            _pages.RemoveAt(pagePosition);
            _events.Add(PageRemoved.Create());
        }


    }
}