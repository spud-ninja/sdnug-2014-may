using Owin;

namespace OSHNWVBAS
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", builder => builder.RunSignalR());
            app.UseNancy();
        }
    }
}