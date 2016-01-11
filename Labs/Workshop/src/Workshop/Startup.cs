using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Workshop
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var c = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json")
                .AddEnvironmentVariables()
                .Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
        
        public static void Main(string[] args)
        {
            var app = new WebApplicationBuilder()
                .UseServer("Microsoft.AspNet.Server.Kestrel")
                .UseConfiguration(WebApplicationConfiguration.GetDefault(args))
                .UseStartup<Startup>()
                .Build();

            app.Run();
        }
    }
}
