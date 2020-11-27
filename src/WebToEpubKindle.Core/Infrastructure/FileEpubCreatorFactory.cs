using System;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure
{
    public abstract class FileEpubCreatorFactory
    {
        public abstract IFileCreator Build(Epub epub);
    }
}
