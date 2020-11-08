using System.Text;
using System;

namespace WebToEpubKindle.Core.Domain
{
    public class Content
    {
        private const string _creator = "WebToEpubKindle";
        private const string _contributor = "WebToEpubKindle";
        private DateTime _date = DateTime.Now;

        public override string ToString()
        {
            StringBuilder textBuilder = new StringBuilder();
            textBuilder.AppendLine(@"<?xml version=""1.0""  encoding=""UTF-8""?>
            <package xmlns=""http://www.idpf.org/2007/opf"" unique-identifier=""uuid_id"" version=""2.0"">");
            textBuilder.AppendLine(@"<metadata xmlns:dc=""http://purl.org/dc/elements/1.1/"" xmlns:dcterms=""http://purl.org/dc/terms/"" xmlns:opf=""http://www.idpf.org/2007/opf"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">");
            textBuilder.AppendLine("</metadata>");
            textBuilder.AppendLine("<manifest>");
            textBuilder.AppendLine("</manifest>");
            textBuilder.AppendLine("</package>");
            return textBuilder.ToString();
        }

    }
}