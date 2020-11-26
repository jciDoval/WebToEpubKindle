using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure
{
    public class EpubGenerator
    {
        private readonly Dictionary<EpubVersion, EpubGeneratorFactory> _factories;

        private EpubGenerator()
        {
            _factories = new Dictionary<EpubVersion, EpubGeneratorFactory>();

            foreach (EpubVersion version in Enum.GetValues(typeof(EpubVersion)))
            {
                var factory = (EpubGeneratorFactory)Activator.CreateInstance(Type.GetType($"WebToEpubKindle.Core.Infrastructure.{Enum.GetName(typeof(EpubVersion), version)}.Factory"));
                _factories.Add(version, factory);
            }
        }

        public static EpubGenerator InitializeFactories() => new EpubGenerator();

        public IEpubGenerator BuildInstance(EpubVersion version, Epub epub) => _factories[version].Build(epub);
    }
}
