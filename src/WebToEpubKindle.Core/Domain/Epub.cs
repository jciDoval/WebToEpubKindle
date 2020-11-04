using System;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Epub
    {
        private Dictionary<Guid,string> _tableOfContent;
        private List<Chapter> _chapters;
        public List<Chapter> Chapters {get { return _chapters;}}

        private Epub(List<Chapter> chapters)
        {
            _chapters = chapters;
            _tableOfContent = new Dictionary<Guid, string>();
            LoadTableOfContents();
        }

        private void LoadTableOfContents()
        {
            foreach (var chapter in _chapters)
            {
                _tableOfContent.Add(chapter.Identifier, chapter.Title);
            }
        }

        public static Epub Create(List<Chapter> chapters)
        {
           return new Epub(chapters);
        }
        
        public void AddChapter(Chapter chapter)
        {
            _chapters.Add(chapter);
            _tableOfContent.Add(chapter.Identifier, chapter.Title);
        }
    }
}
