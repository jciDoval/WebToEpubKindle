using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Page
    {
        private string _content;
        private List<Image> _images;
        private Guid _identifier;

        public string Identifier => _identifier.ToString();
        public List<Image> Images => _images;
        public string Content => _content;
        public bool HasImages => _images.Any();

        private Page(string content, List<Image> images)
        {
            _identifier = Guid.NewGuid();
            _content = content;
            if (images != null)
                _images = images;
            else
                _images = new List<Image>();
        }

        public static Page Create(string content, List<Image> images)
        {
            return new Page(content, images);
        }

        public void ReplaceStringInPageContent(string oldText, string newText)
        {
            _content = _content.Replace(oldText, newText);
        }

        public bool ValidateImageContent()
        {
            bool result = true;
            if (!this.HasImages) return result;
            
            const string imgRegexPattern = "<img.*?src=\"../images/{0}\"[^\\>]+>";
            
            foreach (Image image in _images)
            {
                if (!Regex.IsMatch(_content, string.Format(imgRegexPattern, image.OriginalFileName)))
                    result = false;
            }
            return result;
        }

    }
}
