using System;
using System.Web;

namespace TestServer
{
    public class StatusCode : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string statusCodeString = context.Request.QueryString["statuscode"];
            string statusDescription = context.Request.QueryString["statusdescription"];
            try
            {
                int statusCode = int.Parse(statusCodeString);
                context.Response.StatusCode = statusCode;
                if (statusDescription != null)
                {
                    context.Response.StatusDescription = statusDescription;
                }
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                context.Response.StatusDescription = "Error parsing statuscode: " + statusCodeString;
            }
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
