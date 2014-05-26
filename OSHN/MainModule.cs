using Nancy;

namespace OSHN
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o => "Hello World! - Generated from Nancy - Now we are getting somewhere!";
        }
    }
}