using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace NetCoreServer
{
    public class RequestInformation
    {
        public string Method { get; private set; }

        public string Url { get; private set; }

        public NameValueCollection Headers { get; private set; }

        public NameValueCollection Cookies { get; private set; }

        public string BodyContent { get; private set; }

        public int BodyLength { get; private set; }

        public bool SecureConnection { get; private set; }

        public bool ClientCertificatePresent { get; private set; }

        public X509Certificate2 ClientCertificate { get; private set; }

        public static RequestInformation Create(HttpRequest request)
        {
            var info = new RequestInformation();
            info.Method = request.Method;
            info.Url = request.Path + request.QueryString;
            info.Headers = new NameValueCollection();
            foreach (KeyValuePair<string, StringValues> header in request.Headers)
            {
                info.Headers.Add(header.Key, header.Value.ToString());
            }

            var cookies = new NameValueCollection();
            CookieCollection cookieCollection = RequestHelper.GetRequestCookies(request);
            foreach (Cookie cookie in cookieCollection)
            {
                cookies.Add(cookie.Name, cookie.Value);
            }
            info.Cookies = cookies;

            Stream stream = request.Body;
            using (var reader = new StreamReader(stream))
            {
                string body = reader.ReadToEnd();
                info.BodyContent = body;
                info.BodyLength = body.Length;
            }

            info.SecureConnection = request.IsHttps;

            //info.ClientCertificate = request.ClientCertificate;

            return info;
        }

        public static RequestInformation DeSerializeFromJson(string json)
        {
            return (RequestInformation)JsonConvert.DeserializeObject(
                json,
                typeof(RequestInformation),
                new NameValueCollectionConverter());

        }

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(this, new NameValueCollectionConverter());
        }

        private RequestInformation()
        {
        }
    }
}
