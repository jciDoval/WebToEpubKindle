using System.IO;
using System.Text;
using WebToEpubKindle.Core.Domain;

namespace WebToEpubKindle.Core.Infrastructure
{

    public class EpubWriter
    {
        private Epub _epub;
        private string _path;

        private EpubWriter(Epub epub, string path)
        {
            this._epub = epub;
            this._path = path;
        }

        public static void WriteToDisk(Epub epub, string path, string fileName)
        {
            EpubWriter epubWriter = new EpubWriter(epub, path);
            using (var file = new FileStream(@"D:\",FileMode.Create, FileAccess.Write))
            {
                var fileBytes = BuildEpubToFile();
                file.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        private static byte[] BuildEpubToFile()
        {
            return Encoding.UTF8.GetBytes("Esto es una prueba");
        }

        private static byte[] BuildTableOfContents()
        {
            return null;
        }
    }

}


