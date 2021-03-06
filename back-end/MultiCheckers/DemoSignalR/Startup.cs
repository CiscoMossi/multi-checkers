﻿using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

namespace MultiCheckers.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
