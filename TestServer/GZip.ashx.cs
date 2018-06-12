using System;
using System.Web;

namespace TestServer
{
    /// <summary>
    /// Summary description for GZip
    /// </summary>
    public class GZip : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string responseBody = "Sending GZIP compressed";

            context.Response.Headers.Add("Content-MD5", Convert.ToBase64String(ContentHelper.ComputeMD5Hash(responseBody)));
            context.Response.Headers.Add("Content-Encoding", "gzip");

            context.Response.ContentType = "text/plain";

            byte[] bytes = ContentHelper.GetGZipBytes(responseBody);
            context.Response.BinaryWrite(bytes);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
