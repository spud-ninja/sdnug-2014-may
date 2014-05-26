using Owin;

namespace OwinSelfHost
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, action) =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello World! - Generated from OwinSelfHost");
            });
        }
    }
}