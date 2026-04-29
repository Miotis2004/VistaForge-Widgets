using System;
using Moq;
using Xunit;

namespace VistaForge.Core.Tests
{
    public class BasicMathTests
    {
        [Fact]
        public void TestMath_Passes()
        {
            Assert.Equal(4, 2 + 2);
        }
    }
}
