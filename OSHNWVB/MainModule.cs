using Nancy;

namespace OSHNWVB
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o => View["views/app"];
        }
    }
}