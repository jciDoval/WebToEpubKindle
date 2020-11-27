using System.Text;
using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Validation;
using WebToEpubKindle.Core.Properties;
using WebToEpubKindle.Core.Interfaces;
using System.Linq;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Chapter
    {
        private const string _abr = "chap";
        private const string _extension = ".xhtml";
        private string _abbreviation;
        private int? _secuential;
        private List<Page> _pages;
        private readonly Guid _identifier;
        private string _fileName;
        private readonly string _title;

        public string Abbreviation { get { return _abbreviation;} }
        public string FileName { get => _fileName; }        
        public String IdentifierFormatted { get => _identifier.ToString().Replace("-", "_"); }        
        public string Title { get => _title; }
        public bool HasImages { get => Images.Count() > 0; }
        public List<Image> Images { get => _pages.SelectMany(x => x.Images).ToList(); }
        public List<Page> Pages { get => _pages; } 

        public Chapter(string title, List<Page> pages)
        {
            _identifier = Guid.NewGuid();
            _title = title;
            _pages = pages;
            _fileName = IdentifierFormatted + _extension;
            _abbreviation  = _abr;
        }

        public void AddPage(Page page)
        {
            Ensure.Argument.NotNull(page, CoreStrings.NullPage);
            _pages.Add(page);
        }
        public void AssignSecuential(int secuential)
        {
            _secuential = secuential;
            _abbreviation = _abr + _secuential; 
        }

        public void DeletePage(int pagePosition) => _pages.RemoveAt(pagePosition);

       
    }
}