using Nancy;

namespace OSHNWVBAS
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o => View["views/app"];
            Get["/chat.html"] = o => View["views/chat"];
            Get[@"/(.*)"] = o => View["views/app"];
        }
    }
}