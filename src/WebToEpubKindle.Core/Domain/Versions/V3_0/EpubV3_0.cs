using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebToEpubKindle.Core.Domain.Versions.V3_0
{
    public class EpubV3_0 : Domain.Epub
    {

        public EpubV3_0(string title, CultureInfo culture) : base(title, culture)
        {

        }
    }
}
