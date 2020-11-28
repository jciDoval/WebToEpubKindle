using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Domain.EpubComponents.Events
{
    public class PageRemoved : IEvent
    {
        public static PageRemoved Create()
        {
            return new PageRemoved();
        }
    }
}
