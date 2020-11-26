using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure.V3_0
{
    public class Factory : EpubGeneratorFactory
    {
        public Factory()
        {

        }
        public override IEpubGenerator Build(Epub epub) => new Generator(epub);
    }
}
