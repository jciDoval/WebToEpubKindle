using System;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure
{
    public class FileEpubFactory
    {
        private EpubVersion _version;
        private Type _type;
        private Type _typeHtmlConverter;
        private string _textVersion;
        private Epub _epub;

        private FileEpubFactory(EpubVersion version, Epub epub)
        {
            _epub = epub;
            _version = version;
            _textVersion = Enum.GetName(typeof(EpubVersion), _version);
            _type = Type.GetType($"WebToEpubKindle.Core.Infrastructure.Versions.{_textVersion}.Creator");
            _typeHtmlConverter = Type.GetType($"WebToEpubKindle.Core.Domain.Versions.{_textVersion}.HtmlConverter{_textVersion}");
        }

        public static FileEpubFactory Initialize(EpubVersion version, Epub epub) => new FileEpubFactory(version, epub);

        public IFileEpubCreator BuildCreator()
        {
            var _htmlConverter = Activator.CreateInstance(_typeHtmlConverter);
            return (IFileEpubCreator)Activator.CreateInstance(_type, new object[] { _epub, _htmlConverter });
        }
        
    }
}
