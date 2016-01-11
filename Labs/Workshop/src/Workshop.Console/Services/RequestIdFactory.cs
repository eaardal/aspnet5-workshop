using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Workshop.Console.Services
{
    public interface IRequestIdFactory
    {
        string MakeRequestId();
    }

    public class RequestIdFactory : IRequestIdFactory
    {
        private int _requestId;

        public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
    }

    public class RequestId : IRequestId
    {
        private readonly string _id;

        public RequestId(IRequestIdFactory requestIdFactory)
        {
            _id = requestIdFactory.MakeRequestId();
        }

        public string Id => _id;
    }

    public interface IRequestId
    {
        string Id { get; }
    }
}
