using System.Text;
using System;
using WebToEpubKindle.Core.Interfaces;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Domain
{
    public class Content : IHtmlConvertible
    {
        private const string _creator = "WebToEpubKindle";
        private const string _contributor = "WebToEpubKindle";
        private DateTime _date = DateTime.Now;

        private List<ContentItem> _imageItems;
        private List<ContentItem> _fileItems;

        public Content()
        {
            _imageItems = new List<ContentItem>();
            _fileItems = new List<ContentItem>();
        }

        public void AddContentFromChapter(Chapter chapter)
        {
            if (chapter.HasImages)
                AddImagesToItems(chapter.Images);

            AddFileToItems(chapter);
        }

        private void AddFileToItems(Chapter chapter)
        {
            _fileItems.Add(new ContentItem(chapter.Identifier.ToString(), chapter.FileName, "application/xhtml+xml"));
        }

        private void AddImagesToItems(List<Image> images)
        {
            foreach (var image in images)
            {
                _imageItems.Add(new ContentItem(image.Id, $"images/{image.Id}", image.MediaType));
            }
        }

        public string ToHtml()
        {
            StringBuilder spineRefs = new StringBuilder();
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(@"<?xml version=""1.0""  encoding=""UTF-8""?>
            <package xmlns=""http://www.idpf.org/2007/opf"" unique-identifier=""uuid_id"" version=""2.0"">");
            textBuilder.AppendLine(@"<metadata xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:dcterms=""http://purl.org/dc/terms/"" xmlns:opf=""http://www.idpf.org/2007/opf"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">");
            textBuilder.AppendLine("</metadata>");
            textBuilder.AppendLine("<manifest>");
            foreach (var imageItem in _imageItems)
                textBuilder.AppendLine($@"<item href=""{imageItem.Href}.xhtml"" id=""{imageItem.Id}"" media-type=""{imageItem.MediaType}"" />");

            foreach (var fileItem in _fileItems)
            {
                spineRefs.AppendLine($@"<itemref idref=""{fileItem.Id}"" />"); 
                textBuilder.AppendLine($@"<item href=""{fileItem.Href}"" id=""{fileItem.Id}"" media-type=""{fileItem.MediaType}"" />");
            }
            textBuilder.AppendLine(@"<item href=""toc.ncx"" id=""ncx"" media-type=""application/x-dtbncx+xml""/>");
            textBuilder.AppendLine("</manifest>");
            textBuilder.AppendLine(@"<spine toc=""ncx"">");
            textBuilder.Append(spineRefs.ToString());
            textBuilder.AppendLine("</spine>");
            textBuilder.AppendLine("</package>");
            return textBuilder.ToString();
        }
    }
}