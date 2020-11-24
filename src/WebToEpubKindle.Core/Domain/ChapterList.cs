using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Domain.EventArg;

namespace WebToEpubKindle.Core.Domain
{
    public class ChapterList
    {
        private List<Chapter> _chapters;
        public List<Chapter> Chapters { get { return _chapters; } }
        public EventHandler<ChapterEventArgs> ChapterAdded;

        public ChapterList()
        {
            _chapters = new List<Chapter>();
        }
        public void AddChapter(Chapter chapter)
        {
            _chapters.Add(chapter);
            ChapterEventArgs args = new ChapterEventArgs(){ Chapter = chapter};
            OnChapterAdded(args);
        }

        protected virtual void OnChapterAdded(ChapterEventArgs e)
        {
            EventHandler<ChapterEventArgs> handler = ChapterAdded;
            if (handler!=null)
                handler(this,e);
        } 

    }
}