using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreServer
{
    public class VersionHandler
    {
        public static async Task InvokeAsync(HttpContext context)
        {
            string versionInfo = $"Framework: {RuntimeInformation.FrameworkDescription}";
            byte[] bytes = Encoding.UTF8.GetBytes(versionInfo);

            context.Response.ContentType = "text/plain";
            context.Response.ContentLength = bytes.Length;
            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
