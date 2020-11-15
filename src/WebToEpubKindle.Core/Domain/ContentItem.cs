namespace WebToEpubKindle.Core.Domain
{
    public class ContentItem
    {
        private readonly string _id;
        public string Id { get { return  _id; } }
        private readonly string _href;
        public string Href { get { return _href;}}
        private  readonly string _mediaType;
        public string MediaType { get { return _mediaType;}}
        
        public ContentItem(string id, string href, string mediaType)
        {
            this._id = id;
            this._href = href;
            this._mediaType = mediaType;
        }
        
        
    }
}