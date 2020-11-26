using System;
using System.Collections.Generic;
using System.Globalization;
using WebToEpubKindle.Core.Domain.EventArg;
namespace WebToEpubKindle.Core.Domain
{
    public class Epub : IDisposable
    {
        private List<Image> _images;
        private ChapterList _chapterList;
        private CultureInfo _cultureInfo;
        private readonly MetaInf _metaInf;
        private readonly MimeType _mimeType;
        private readonly string _title;
        private TableOfContent _tableOfContent;
        private Content _content;        
        public ChapterList ChapterList { get { return _chapterList; } }
        public string Title { get { return _title; }}

        private Epub(string title, CultureInfo culture)
        {
            _title = title;
            _chapterList = new ChapterList();
            _chapterList.ChapterAdded += OnChapterAdded;
            _content = new Content(_title, culture.TwoLetterISOLanguageName.ToLower());
            _cultureInfo = culture;
            _images = new List<Image>();
            _mimeType = new MimeType();
            _metaInf = new MetaInf();
            _tableOfContent = new TableOfContent(_title);
        }

        private void OnChapterAdded(object sender, ChapterEventArgs e)
        {
            _tableOfContent.ChapterIndexer(e.Chapter);
            _content.AddContentFromChapter(e.Chapter);
            Console.WriteLine($"Chapter added: { e.Chapter.Title}");
        }

        public static Epub Create(string title, CultureInfo culture)
        {
            return new Epub(title, culture);
        }

        public string GetMetaInfContent() => _metaInf.ToHtml();
        public string GetMimeTypeContent() => _mimeType.ToHtml();
        public string GetTableOfContent() => _tableOfContent.ToHtml();
        public string GetContent() => _content.ToHtml();

        public void Dispose()
        {
            _chapterList.ChapterAdded -= OnChapterAdded;
        }
    }
}
