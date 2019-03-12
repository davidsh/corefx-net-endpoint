using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NetCoreServer
{
    public class GenericHandler
    {

        // Must have constructor with this signature, otherwise exception at run time.
        public GenericHandler(RequestDelegate next)
        {
            // This is an HTTP Handler, so no need to store next.
        }

        public async Task Invoke(HttpContext context)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"{context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
            foreach (KeyValuePair<string, StringValues> pair in context.Request.Headers)
            {
                builder.AppendLine($"{pair.Key}: {pair.Value}");
            }

            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(builder.ToString());
        }
    }

    public static class GenericHandlerExtensions
    {
        public static IApplicationBuilder UseGenericHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GenericHandler>();
        }
    }
}
