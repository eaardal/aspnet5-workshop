using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Options;

namespace Workshop.Console
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestCultureOptions _options;

        public RequestCultureMiddleware(RequestDelegate next, IOptions<RequestCultureOptions> options)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            if (options == null) throw new ArgumentNullException(nameof(options));
            _next = next;
            _options = options.Value;
        }

        public Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];

            var requestCulture = !string.IsNullOrWhiteSpace(cultureQuery) 
                ? new CultureInfo(cultureQuery) 
                : _options.DefaultCulture;

            if (requestCulture != null)
            {
                var culture = new CultureInfo(cultureQuery);
#if !DNXCORE50
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
#else
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
#endif
            }
            return _next(context);
        }
    }

    public class RequestCultureOptions
    {
        public CultureInfo DefaultCulture { get; set; }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }

        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder,
            RequestCultureOptions options)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>(Options.Create(options));
        }
    }
}
