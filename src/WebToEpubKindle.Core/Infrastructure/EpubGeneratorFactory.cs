using System;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure
{
    public abstract class EpubGeneratorFactory
    {
        public abstract IEpubGenerator Build(Epub epub);
    }
}
