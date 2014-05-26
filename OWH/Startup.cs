using Microsoft.Owin;
using Owin;
using OwinWebHost;

[assembly: OwinStartup(typeof (Startup))]

namespace OwinWebHost
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, action) =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello World! - Generated from Owin on IIS");
            });
        }
    }
}