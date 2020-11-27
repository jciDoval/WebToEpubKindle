using System;
using System.Collections.Generic;
using System.Globalization;
using WebToEpubKindle.Core.Domain.Enum;

namespace WebToEpubKindle.Core.Domain
{
    public class EpubFactory
    {
        private EpubVersion _version;
        private Type _type;
        private string _title;
        private CultureInfo _culture;

        private EpubFactory(EpubVersion version, string title, CultureInfo culture)
        {
            string textVersion = System.Enum.GetName(typeof(EpubVersion), version);
            _culture = culture;
            _version = version;
            _title = title;
            _type = Type.GetType($"WebToEpubKindle.Core.Domain.Versions.{textVersion}.Epub{textVersion}");
            
        }

        public static EpubFactory Initialize(EpubVersion version, string title, CultureInfo culture) => new EpubFactory(version, title, culture);
        public Epub BuildInstance() => (Epub)Activator.CreateInstance(_type, new Object[] { _title, _culture });
    }
}
