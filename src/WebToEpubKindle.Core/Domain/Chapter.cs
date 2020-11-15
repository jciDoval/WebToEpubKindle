using System.Text;
using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Validation;
using WebToEpubKindle.Core.Properties;
using WebToEpubKindle.Core.Interfaces;
using System.Linq;

namespace WebToEpubKindle.Core.Domain
{
    public class Chapter : IHtmlConvertible
    {
        private List<Page> _pages;
        private readonly Guid _identifier;
        public Guid Identifier { get => _identifier; }
        private readonly string _title;
        public string Title { get => _title; }
        public bool HasImages { get => Images.Count() > 0; }
        public List<Image> Images { get => _pages.SelectMany(x => x.Images).ToList(); }

        public Chapter(string title, List<Page> pages)
        {
            _identifier = Guid.NewGuid();
            _title = title;
            _pages = pages;
        }

        public void AddPage(Page page)
        {
            Ensure.Argument.NotNull(page, CoreStrings.NullPage);
            _pages.Add(page);
        }



        public void DeletePage(int pagePosition) => _pages.RemoveAt(pagePosition);

        public List<Image> GetImages() => _pages.SelectMany(x => x.Images).ToList();

        public string ToHtml()
        {
            StringBuilder _textBuilder = new StringBuilder();
            _textBuilder.Clear();
            _textBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>");
            _textBuilder.AppendLine("<!DOCTYPE html>");
            _textBuilder.AppendLine(@"<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:epub=""http://www.idpf.org/2007/ops"">");
            _textBuilder.AppendLine("<head>");
            _textBuilder.AppendFormat("<title>{0}</title>", _title);
            _textBuilder.AppendLine();
            //_textBuilder.AppendLine("<link rel=""stylesheet"" href=""css/style.css"" type=""text/css""/>");
            _textBuilder.AppendLine(@"<meta charset=""utf-8""/>");
            _textBuilder.AppendLine("</head>");
            _textBuilder.AppendLine("<body>");
            _textBuilder.AppendLine("<section>");
            _textBuilder.AppendLine("<header>");
            _textBuilder.AppendFormat("<h1>{0}</h1>", _title);
            _textBuilder.AppendLine();
            _textBuilder.AppendLine("</header>");
            _pages.ForEach(page => _textBuilder.AppendLine(page.ToString()));
            _textBuilder.AppendLine("</section>");
            _textBuilder.AppendLine("</body>");
            _textBuilder.AppendLine("</html>");
            return _textBuilder.ToString();
        }
    }
}