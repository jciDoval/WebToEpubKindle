using System.Collections.Generic;
using System.Text;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Page  
    {
        private string _content;
        private List<Image> _images;

        public List<Image> Images { get => _images; }
        public string Content { get => _content; }

        private Page(string content, List<Image> images) 
        {
            _content = content;
            if (images!=null)
                _images = images;
            else
                _images = new List<Image>();
        } 

        public static Page Create(string content, List<Image> images)
        {
            return new Page(content, images);
        }

    }
}
