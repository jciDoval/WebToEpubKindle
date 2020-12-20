using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Domain.EventArg;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class ChapterList
    {
        private List<Chapter> _chapters;
        public List<Chapter> Chapters => _chapters;
        public EventHandler<ChapterEventArgs> ChapterAdded;

        public ChapterList()
        {
            _chapters = new List<Chapter>();
        }
        public void AddChapter(Chapter chapter)
        {
            chapter.AssignSecuential(_chapters.Count + 1);
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