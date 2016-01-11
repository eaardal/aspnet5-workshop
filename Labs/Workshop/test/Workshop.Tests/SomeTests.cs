using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Workshop.Tests
{
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
