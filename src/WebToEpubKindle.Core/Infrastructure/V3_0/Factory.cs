using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Infrastructure.V3_0
{
    public class Factory : FileEpubCreatorFactory
    {
        public Factory()
        {

        }
        public override IFileCreator Build(Epub epub) => new Creator(epub);
    }
}
