using System;
using System.Web;

namespace TestServer
{
    /// <summary>
    /// Summary description for Deflate
    /// </summary>
    public class Deflate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string responseBody = "Sending DEFLATE compressed";

            context.Response.Headers.Add("Content-MD5", Convert.ToBase64String(ContentHelper.ComputeMD5Hash(responseBody)));
            context.Response.Headers.Add("Content-Encoding", "deflate");

            context.Response.ContentType = "text/plain";

            byte[] bytes = ContentHelper.GetDeflateBytes(responseBody);
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
