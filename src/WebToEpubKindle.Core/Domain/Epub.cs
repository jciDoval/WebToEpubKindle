using System;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Epub
    {
        private List<Chapter> _chapters;
        private MimeType _mimeType;
        private string _title;
        private TableOfContent _tableOfContent;
        
        public List<Chapter> Chapters { get { return _chapters; } }
        public MimeType MimeType {get {return _mimeType;}}
        public TableOfContent TableOfContent { get {return _tableOfContent;}}
        public string Title { get { return _title; }}

        private Epub(string title, List<Chapter> chapters)
        {
            _title = title;
            _chapters = chapters;
            _mimeType = new MimeType();
            _tableOfContent = new TableOfContent(_title);
            _tableOfContent.IndexChapter(_chapters);
        }

        public static Epub Create(string title, List<Chapter> chapters)
        {
            return new Epub(title, chapters);
        }

        public void AddChapter(Chapter chapter)
        {
            _chapters.Add(chapter);
            _tableOfContent.IndexChapter(chapter);
        }
    }
}
