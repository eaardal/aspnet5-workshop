using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Serilog;
using Workshop.Console.Middleware;
using Workshop.Console.Services;

namespace Workshop.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IApplicationEnvironment appEnv, IHostingEnvironment env)
        {
            var logFile = Path.Combine(appEnv.ApplicationBasePath, "logfile.txt");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TextWriter(File.CreateText(logFile))
                .CreateLogger();

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
            services.AddSingleton<IRequestIdFactory, RequestIdFactory>();
            services.AddScoped<IRequestId, RequestId>();

            services.Configure<RequestCultureOptions>(options =>
            {
                options.DefaultCulture = new CultureInfo(Configuration["culture"] ?? "en-GB");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages(subApp =>
                {
                    subApp.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync($"Error @ {context.Response.StatusCode}");
                    });
                });
            }

            logFactory.AddConsole(LogLevel.Trace);
            logFactory.AddSerilog();

            var logger = logFactory.CreateLogger<Startup>();

            logger.LogTrace("Trace msg");
            logger.LogDebug("Debug msg");
            logger.LogInformation("Info msg");
            logger.LogWarning("Warn msg");
            logger.LogError("Error msg");
            logger.LogCritical("Critical msg");

            app.UseMiddleware<RequestIdMiddleware>();

            //app.Run(ctx =>
            //{
            //    //throw new Exception("some error");
            //    //ctx.Response.StatusCode = 500;
            //    return Task.FromResult(0);
            //});


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
