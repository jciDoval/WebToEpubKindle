using System;
using System.Collections.Generic;
using System.Globalization;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Domain.EventArg;

namespace WebToEpubKindle.Core.Domain
{
    public abstract class Epub : IDisposable
    {
        private List<Image> _images;
        private ChapterList _chapterList;
        protected readonly MetaInf _metaInf;
        protected readonly MimeType _mimeType;
        protected readonly string _title;
        protected TableOfContent _tableOfContent;
        protected Content _content;        

        public Content Content { get => _content; }
        public ChapterList ChapterList { get => _chapterList; }
        public MimeType MimeType { get => _mimeType;  }
        public MetaInf MetaInf { get => _metaInf;  }
        public TableOfContent TableOfContent { get => _tableOfContent; }
        public string Title { get => _title; }

        public Epub(string title, CultureInfo culture)
        {
            _title = title;
            _chapterList = new ChapterList();
            _chapterList.ChapterAdded += OnChapterAdded;
            _content = new Content(_title, culture.TwoLetterISOLanguageName.ToLower());
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

        
        public virtual string GetContent() => _content.ToHtml();

        public void Dispose()
        {
            _chapterList.ChapterAdded -= OnChapterAdded;
        }
    }
}
