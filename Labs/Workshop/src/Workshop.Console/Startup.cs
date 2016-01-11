using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;

namespace Workshop.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json")
                .AddEnvironmentVariables()
                .Build();

            if (env.IsDevelopment())
            {
                Debug.WriteLine("IsDev");
            }
            if (env.IsProduction())
            {
                Debug.WriteLine("IsProd");
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseFileServer();

            app.Run(async ctx => await ctx.Response.WriteAsync($"{Configuration["greeting"]}"));
        }
    }
}
