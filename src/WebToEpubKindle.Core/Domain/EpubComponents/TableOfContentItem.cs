using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class TableOfContentItem
    {
        private readonly string _abbreviation;
        public string Abbreviation => _abbreviation;

        private readonly string _src;
        public string Src => _src;

        private readonly string _title;
        public string Title => _title;

        public TableOfContentItem(string abbreviation, string src, string title)
        {
            this._abbreviation = abbreviation;
            this._src = src;
            this._title = title;
        }

    }
}
