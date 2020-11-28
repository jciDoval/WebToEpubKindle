using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Infrastructure;

namespace WebToEpubKindle.Core.Interfaces
{
    public interface IFileEpubCreator
    {        
        void Create(string path, string fileName);
    }
}
