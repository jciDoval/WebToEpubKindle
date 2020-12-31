using System;
using WebToEpubKindle.Core.Domain.EpubComponents;

namespace WebToEpubKindle.Core.Domain.EventArg
{
    public class ChapterEventArgs : EventArgs
    {
        public Chapter Chapter { get; set; }
    }
}