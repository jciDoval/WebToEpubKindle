using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using WebToEpubKindle.Core.Domain.EventArg;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class ChapterList
    {
        private int _numberOfImages = 1;
        private readonly List<Chapter> _chapters;
        public List<Chapter> Chapters => _chapters;
        
        private List<Image> _images;
        public List<Image> Images => _images;
        public EventHandler<ChapterEventArgs> ChapterAdded;

        public ChapterList()
        {
            _chapters = new List<Chapter>();
            _images = new List<Image>();
        }
        
        public void AddChapter(Chapter chapter)
        {
            ManageEpubImages(chapter);
            chapter.AssignSecuential(_chapters.Count + 1);
            _chapters.Add(chapter);
            ChapterEventArgs args = new ChapterEventArgs(){ Chapter = chapter};
            OnChapterAdded(args);
        }

        protected virtual void OnChapterAdded(ChapterEventArgs e)
        {
            EventHandler<ChapterEventArgs> handler = ChapterAdded;
            handler?.Invoke(this,e);
        } 
        
        private void ManageEpubImages(Chapter chapter)
        {
            if (!chapter.HasImages) return;
            
            foreach (var img in chapter.Images)
            {
                if (!_images.Contains(img))
                {
                    img.AssingEpubFileNameNumbered(_numberOfImages);
                    _images.Add(img);
                    _numberOfImages++;
                }

                chapter.Pages.ForEach(x =>
                    x.ReplaceStringInPageContent(img.OriginalFileName, img.EpubFileName));
            }
        }

    }
}