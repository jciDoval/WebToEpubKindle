using System.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using WebToEpubKindle.Core.Domain;
using System.Collections.Generic;

namespace WebToEpubKindle.Core.Infrastructure
{

    public class EpubWriter
    {
        private const string _metaInf = @"<?xml version=""1.0""?><container version=""1.0"" xmlns=""urn:oasis:names:tc:opendocument:xmlns:container"">   <rootfiles>     <rootfile full-path=""content.opf"" media-type=""application/oebps-package+xml""/>         </rootfiles></container>    ";
        private const string _mimetype = "application/epub+zip";
        private const string _extension  = ".xhtml";

        private Epub _epub;
        private string _path;

        private EpubWriter(Epub epub)
        {
            this._epub = epub;
        }

        public static EpubWriter Create(Epub epub)
        {
            return new EpubWriter(epub);
        }

        public void WriteToDisk(string path, string fileName)
        {
            if (string.IsNullOrEmpty(path)) 
                path = AppDomain.CurrentDomain.BaseDirectory;

            var filesBytes = BuildFilesBytes();
            foreach(var fileBytes in filesBytes)
            {
                using (var file = new FileStream(path + fileBytes.Key.ToString() + _extension, FileMode.Create, FileAccess.Write))
                {
                    file.Write(fileBytes.Value,0,fileBytes.Value.Length);
                }
            }            
        }

        private Dictionary<Guid,byte[]> BuildFilesBytes()
        {
            Dictionary<Guid,byte[]> files = new Dictionary<Guid, byte[]>();            
            foreach(var chapter in _epub.Chapters)
            {
                files.Add(chapter.Identifier, Encoding.UTF8.GetBytes(chapter.ToString()));
            }
            return files;
        }

        private static byte[] BuildTableOfContents()
        {
            return null;
        }
    }

}


