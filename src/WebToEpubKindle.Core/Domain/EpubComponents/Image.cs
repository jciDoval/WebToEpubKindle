using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebToEpubKindle.Core.Properties;
using WebToEpubKindle.Core.Validation;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class Image
    {
        private byte[] _bytes;
        public byte[] Bytes => _bytes;

        private readonly string _fileName;
        public string FileName => _fileName;

        private Guid _guid;
        public string Id => _guid.ToString();
        
        private string _extension;
        public string Extension => _extension;

        private string _mimeType;
        public string MediaType => _mimeType;

        private Dictionary<string, string> _mimeTypesSupported = new Dictionary<string, string>()
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
            _fileName = fileName;
            _mimeType = GetMimeType(extension);
            _extension = extension;            
        }
        private string GetMimeType(string extension)
        {
            Ensure.That(_mimeTypesSupported.ContainsKey(extension), CoreStrings.ImageFileExtensionNotSupported);
            return _mimeTypesSupported[extension];
        }

        public static async Task<Image> LoadImage(string fileName, string fullPath)
        {
            FileInfo info = new FileInfo(fullPath);
            byte[] bytes = await File.ReadAllBytesAsync(fullPath);
            return new Image(fileName, info.Extension, bytes);
        }

        public static Image LoadImage(string fileName, string extension, byte[] bytes)
        {
            return new Image(fileName, extension, bytes);
        }
    }
}