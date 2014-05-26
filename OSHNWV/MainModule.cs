using System;
using Nancy;

namespace OSHNWV
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = o => View["views/basic", new {Time = DateTime.Now}];
        }
    }
}