using System;
using System.Collections.Generic;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure
{
    public class FileEpubCreator
    {
        private EpubVersion _version;
        private Type _type;
        private string _textVersion;
        private Epub _epub;

        private FileEpubCreator(EpubVersion version, Epub epub)
        {
            _epub = epub;
            _version = version;
            _textVersion = Enum.GetName(typeof(EpubVersion), _version);
            _type = Type.GetType($"WebToEpubKindle.Core.Infrastructure.{_textVersion}.Factory");
        }

        public static FileEpubCreator Initialize(EpubVersion version, Epub epub) => new FileEpubCreator(version, epub);

        public IFileCreator BuildCreator() => ((FileEpubCreatorFactory)Activator.CreateInstance(_type)).Build(_epub);
    }
}
