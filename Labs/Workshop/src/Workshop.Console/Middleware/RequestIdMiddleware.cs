using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using Workshop.Console.Services;

namespace Workshop.Console.Middleware
{
    public class RequestIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestIdMiddleware> _logger;

        public RequestIdMiddleware(RequestDelegate next, ILogger<RequestIdMiddleware> logger)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context, IRequestId requestId)
        {
            _logger.LogInformation($"Request {requestId.Id} executing");

            return _next(context);
        }
    }
}
