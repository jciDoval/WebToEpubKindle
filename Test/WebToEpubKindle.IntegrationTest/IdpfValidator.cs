using System;
using System.IO;
using System.Net;

namespace WebToEpubKindle.IntegrationTest
{
    public class IdpfValidator
    {
        private const string _contentTypeHeader = "Content-Type: multipart/form-data; boundary={0}";
        private const string _originHeader = "Origin: http://validator.idpf.org";
        private const string _refererHeader = "Referer: http://validator.idpf.org/";
        private const string _userAgentHeader = "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.67 Safari/537.36";
        private const string _validatorUrl = "http://validator.idpf.org/application/validate";
        
        
        private readonly string _filePath;
        private readonly FileInfo _fileInfo;

        public IdpfValidator(string filePath)
        {
            _filePath = filePath;
            _fileInfo = new FileInfo(_filePath);
        }
        

        public bool Validate()
        {
            bool result = false;
            string boundaryString = $"----WebKitFormBoundary{Guid.NewGuid().ToString():N}";

            HttpWebRequest requestValidateEpub = WebRequest.CreateHttp(_validatorUrl);
            requestValidateEpub.Method = WebRequestMethods.Http.Post;
            requestValidateEpub.KeepAlive = true;
            requestValidateEpub.Credentials = System.Net.CredentialCache.DefaultCredentials;
            requestValidateEpub.Headers.Add(_originHeader);
            requestValidateEpub.Headers.Add(string.Format(_contentTypeHeader, boundaryString));
            requestValidateEpub.Headers.Add(_userAgentHeader);
            requestValidateEpub.Headers.Add(_refererHeader);


            MemoryStream postDataStream = new MemoryStream();
            StreamWriter postDataWriter = new StreamWriter(postDataStream);

            postDataWriter.Write(Environment.NewLine + "--" + boundaryString);
            postDataWriter.Write(Environment.NewLine + $"Content-Disposition: form-data; name=\"inputFile\"; filename=\"{_fileInfo.Name}\"");
            postDataWriter.Write(Environment.NewLine + "Content-Type: application/epub+zip" + Environment.NewLine + Environment.NewLine);
            postDataWriter.Flush();

            var fileBytes = File.ReadAllBytes(_fileInfo.FullName);
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