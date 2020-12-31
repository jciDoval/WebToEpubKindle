using System;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure
{
    public class FileEpubFactory
    {
        private readonly EpubVersion _version;
        private readonly Type _type;
        private readonly Type _typeHtmlConverter;
        private readonly Type _typeImageConverter;
        private readonly Epub _epub;

        private FileEpubFactory(EpubVersion version, Epub epub)
        {
            _epub = epub;
            _version = version;
            var textVersion = Enum.GetName(typeof(EpubVersion), _version);
            _type = Type.GetType($"WebToEpubKindle.Core.Infrastructure.Versions.{textVersion}.Creator");
            _typeHtmlConverter = Type.GetType($"WebToEpubKindle.Core.Domain.Versions.{textVersion}.HtmlConverter{textVersion}");
            _typeImageConverter = Type.GetType($"WebToEpubKindle.Core.Domain.Versions.{textVersion}.ImageConverter{textVersion}");
        }

        public static FileEpubFactory Initialize(EpubVersion version, Epub epub) => new FileEpubFactory(version, epub);

        public IFileEpubCreator BuildCreator()
        {
            var htmlConverter = Activator.CreateInstance(_typeHtmlConverter);
            return (IFileEpubCreator)Activator.CreateInstance(_type, new object[] { _epub, htmlConverter });
        }
        
    }
}
