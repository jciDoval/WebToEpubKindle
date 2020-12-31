using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebToEpubKindle.Core.Properties;
using WebToEpubKindle.Core.Validation;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Image : IEquatable<Image>
    {
        private readonly byte[] _bytes;
        public byte[] Bytes => _bytes;

        private readonly string _originalFileName;
        public string OriginalFileName => _originalFileName;

        private string _epubFileName;
        public string EpubFileName => _epubFileName;

        private Guid _guid;
        public string Id => _guid.ToString();
        
        private readonly string _extension;
        public string Extension => _extension;

        private readonly string _mimeType;
        public string MimeType => _mimeType;

        private readonly Dictionary<string, string> _mimeTypesSupported = new Dictionary<string, string>()
        {
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".svg", "image/svg+xml" }
        };
        
        private Image(string fileName, string extension, byte[] bytes)
        {            
            _bytes = bytes;
            _guid = Guid.NewGuid();
            _originalFileName = fileName;
            _mimeType = GetMimeType(extension);
            _extension = extension;            
        }
        
        private string GetMimeType(string extension)
        {
            Ensure.That(_mimeTypesSupported.ContainsKey(extension), CoreStrings.ImageFileExtensionNotSupported);
            return _mimeTypesSupported[extension];
        }

        public static async Task<Image> LoadImage(string fullPath)
        {
            FileInfo info = new FileInfo(fullPath);
            byte[] bytes = await File.ReadAllBytesAsync(fullPath);
            return new Image(info.Name, info.Extension, bytes);
        }

        public static Image LoadImage(string fileName, string extension, byte[] bytes)
        {
            return new Image(fileName, extension, bytes);
        }

        public void AssingEpubFileNameNumbered(int number) => _epubFileName = number.ToString("D8") + _extension;

        public bool Equals(Image other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_bytes, other._bytes) && _originalFileName == other._originalFileName && _extension == other._extension && _mimeType == other._mimeType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Image) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_bytes, _originalFileName, _extension, _mimeType);
        }

        public static bool operator ==(Image left, Image right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Image left, Image right)
        {
            return !Equals(left, right);
        }
    }
}