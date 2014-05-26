using Nancy;

namespace OSHNWVBA
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o => View["views/app"];
            Get["/external"] = o => View["views/external"];
            Get[@"/(.*)"] = o => View["views/app"];
        }
    }
}