namespace WebToEpubKindle.Core.Domain.EpubComponents
{
    public class MetaInf
    {
        private const string _fullPath = "OEBPS/content.opf";
        private const string _mediaType = "application/oebps-package+xml";
        public string FullPath { get => _fullPath; }
        public string MediaType { get => _mediaType; }


    }
}