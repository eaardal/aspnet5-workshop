using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;

namespace Workshop.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = WebApplicationConfiguration.GetDefault(args);

            var app = new WebApplicationBuilder()
                .UseServer("Microsoft.AspNet.Server.Kestrel")
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .Build();

            app.Run();
        }
    }
}
