using System.Text;
using System;
using WebToEpubKindle.Core.Interfaces;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Content 
    {
        private const string _creator = "WebToEpubKindle";
        private const string _contributor = "WebToEpubKindle";
        private string _language;
        private string _title;
        private DateTime _date = DateTime.Now;

        private List<ContentItem> _imageItems;
        private List<ContentItem> _fileItems;

        public string Creator { get { return _creator; } }
        public List<ContentItem> ImageItems { get { return _imageItems; } }
        public List<ContentItem> FileItems { get { return _fileItems; } }
        public string Language { get { return _language; } }
        public string Title { get { return _title; } }
        

        public Content(string title, string language)
        {
            _imageItems = new List<ContentItem>();
            _fileItems = new List<ContentItem>();
            _title = title;
            _language = language;
        }

        public void AddContentFromChapter(Chapter chapter)
        {
            if (chapter.HasImages)
                AddImagesToItems(chapter.Images);

            AddFileToItems(chapter);
        }

        private void AddFileToItems(Chapter chapter)
        {
            _fileItems.Add(new ContentItem(chapter.Abbreviation, chapter.FileName, "application/xhtml+xml"));
        }

        private void AddImagesToItems(List<Image> images)
        {
            foreach (var image in images)
            {
                string contentImageId = "img" + image.EpubFileName.Split('.')[0];
                var item = new ContentItem(contentImageId, $"../images/{image.EpubFileName}", image.MimeType);
                if (!_imageItems.Contains(item))
                    _imageItems.Add(item);
            }
        }

    }
}