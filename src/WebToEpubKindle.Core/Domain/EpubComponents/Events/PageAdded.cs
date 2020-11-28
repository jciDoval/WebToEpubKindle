using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebToEpubKindle.Core.Interfaces;

namespace WebToEpubKindle.Core.Domain.EpubComponents.Events
{
    public class PageAdded : IEvent
    {
        public static PageAdded Create()
        {
            return new PageAdded();
        }
    }
}
