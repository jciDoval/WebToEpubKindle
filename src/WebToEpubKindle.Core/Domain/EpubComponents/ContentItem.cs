using System;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class ContentItem :IEquatable<ContentItem>
    {
        private readonly string _id;
        public string Id => _id;
        private readonly string _href;
        public string Href => _href;
        private  readonly string _mediaType;
        public string MediaType => _mediaType;

        public ContentItem(string id, string href, string mediaType)
        {
            this._id = id;
            this._href = href;
            this._mediaType = mediaType;
        }

        public bool Equals(ContentItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id && _href == other._href && _mediaType == other._mediaType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ContentItem) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _href, _mediaType);
        }

        public static bool operator ==(ContentItem left, ContentItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ContentItem left, ContentItem right)
        {
            return !Equals(left, right);
        }
    }
}