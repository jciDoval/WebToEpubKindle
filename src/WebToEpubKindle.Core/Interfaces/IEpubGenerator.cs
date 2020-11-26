using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Infrastructure;

namespace WebToEpubKindle.Core.Interfaces
{
    public interface IEpubGenerator
    {        
        void CreateEpub(string path, string fileName);
    }
}
