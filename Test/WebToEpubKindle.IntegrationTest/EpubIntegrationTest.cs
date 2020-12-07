using System.Globalization;
using System.IO;
using WebToEpubKindle.Core.Domain;
using WebToEpubKindle.Core.Interfaces;
using WebToEpubKindle.Core.Domain.Enum;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Infrastructure;
using Xunit;
using System;
using System.Net;

namespace WebToEpubKindle.IntegrationTest
{
    public class EpubIntegrationTest
    {
        string PATH = AppDomain.CurrentDomain.BaseDirectory;
        const string FILENAME = "EpubTest";
        const string COMPLETEFILENAME = "EpubTest.epub";
        Epub _epub;
        Page _page;
        Chapter _chapter;
        IFileEpubCreator _fileCreator;

        public EpubIntegrationTest()
        {
            _page = Page.Create("This is a test.", null);
            _chapter = Chapter.Create("Test");
            _epub = EpubFactory.Initialize(EpubVersion.V3_0, "Test Epub", new CultureInfo("en-EN")).BuildInstance();
            _fileCreator = FileEpubFactory.Initialize(EpubVersion.V3_0, _epub).BuildCreator();

        }

        [Fact]
        public void validate_epub_file_through_idpf_validator()
        {

            _chapter.AddPage(_page);
            _epub.ChapterList.AddChapter(_chapter);
            _fileCreator.Create(PATH, FILENAME);

            bool result = ValidateEpubOnIDPFValidator();

            File.Delete(PATH + COMPLETEFILENAME);

            Assert.True(result);
        }


        private bool ValidateEpubOnIDPFValidator()
        {
            bool result = false;
            string boundaryString = String.Format("----WebKitFormBoundary{0:N}", Guid.NewGuid());

            HttpWebRequest requestValidateEpub = WebRequest.CreateHttp("http://validator.idpf.org/application/validate");
            requestValidateEpub.Method = WebRequestMethods.Http.Post;
            requestValidateEpub.KeepAlive = true;
            requestValidateEpub.Credentials = System.Net.CredentialCache.DefaultCredentials;
            requestValidateEpub.Headers.Add("Origin: http://validator.idpf.org");
            requestValidateEpub.Headers.Add("Content-Type: multipart/form-data; boundary=" + boundaryString);
            requestValidateEpub.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.67 Safari/537.36");
            requestValidateEpub.Headers.Add("Referer: http://validator.idpf.org/");


            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            postDataWriter.Write(Environment.NewLine + "--" + boundaryString);
            postDataWriter.Write(Environment.NewLine + $"Content-Disposition: form-data; name=\"inputFile\"; filename=\"{COMPLETEFILENAME}\"");
            postDataWriter.Write(Environment.NewLine + "Content-Type: application/epub+zip" + Environment.NewLine + Environment.NewLine);
            postDataWriter.Flush();

            var fileBytes = File.ReadAllBytes(PATH + COMPLETEFILENAME);
            postDataStream.Write(fileBytes, 0, fileBytes.Length);

            postDataWriter.Write(Environment.NewLine + "--" + boundaryString + "--" + Environment.NewLine);
            postDataWriter.Flush();

            requestValidateEpub.ContentLength = postDataStream.Length;

            using (Stream s = requestValidateEpub.GetRequestStream())
            {
                postDataStream.WriteTo(s);
            }
            postDataStream.Close();

            try
            {
                WebResponse response = requestValidateEpub.GetResponse();
                StreamReader responseReader = new StreamReader(response.GetResponseStream());
                string replyFromServer = responseReader.ReadToEnd();

                if (replyFromServer.Contains("Congratulation"))
                    result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
