using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.PlatformAbstractions;
using Workshop.Console;
using Xunit;

namespace Workshop.Tests
{
    public class StartupTests
    {
        [Fact]
        public void AddsConfigJsonToConfiguration()
        {
            //var appEnv = new DefaultApplicationEnvironment();
            //var hostingEnv = new HostingEnvironment();
            //var sut = new Workshop.Console.Startup(appEnv, hostingEnv);
            
        }
    }

    public class SomeTests
    {
        [Fact]
        public void Passes()
        {
            Assert.True(true);
        }

        [Fact]
        public void Fails()
        {
            Assert.True(false);
        }
    }
}
