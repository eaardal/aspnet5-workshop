using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddMvc();

            services.Configure<RequestCultureOptions>(options =>
            {
                options.DefaultCulture = new CultureInfo(Configuration["culture"] ?? "en-GB");
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseIISPlatformHandler();
            //app.UseFileServer();
            var router = new RouteBuilder(app)
                .MapGet("", async ctx => await ctx.Response.WriteAsync("Hello from routing"))
                .MapGet("sub", async ctx => await ctx.Response.WriteAsync("Hello from sub"))
                .MapGet("item/{id:int}", ctx => ctx.Response.WriteAsync($"Item ID: {ctx.GetRouteValue("id")}"))
                ;
            
            app.UseRouter(router.Build());
            app.UseMvc();

            //app.UseRequestCulture();
            //app.Run(async ctx => await ctx.Response.WriteAsync($"Hello {CultureInfo.CurrentCulture.DisplayName}"));
            //app.Run(async ctx => await ctx.Response.WriteAsync($"{Configuration["greeting"]}"));
        }
    }
}
