using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Infrastructure;

namespace WebToEpubKindle.Core.Interfaces
{
    public interface IFileCreator
    {        
        void Create(string path, string fileName);
    }
}
