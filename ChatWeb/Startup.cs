using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChatWeb.Startup))]

namespace ChatWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration();
            config.EnableJSONP = true;
            app.MapSignalR(config);
        }
    }
}
