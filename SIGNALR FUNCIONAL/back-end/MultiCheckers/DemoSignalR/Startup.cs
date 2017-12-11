using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;

namespace MultiCheckers.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Isso aqui é perigoso, não façam isso em projetos de verdade :)
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            
            //// Branch the pipeline here for requests that start with "/signalr"
            //app.Map("/signalr", map =>
            //{
            //    map.UseCors(CorsOptions.AllowAll);
            //    map.RunSignalR();
            //});
        }
    }
}
