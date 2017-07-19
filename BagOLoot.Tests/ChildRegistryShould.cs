using System;
using System.Collections.Generic;
using Xunit;

namespace BagOLoot.Tests
{
    public class ChildRegistryShould
    {
        private readonly ChildRegistry _register;

        public ChildRegistryShould()
        {
            _register = new ChildRegistry();
        }

        [Theory]
        [InlineData("Sarah")]
        [InlineData("Jamal")]
        [InlineData("Kelly")]
        public void AddChildren(string child)
        {
            var result = _register.AddChild(child);
            Assert.True(result);
        }

        [Fact]
        public void ReturnListOfChildren()
        {
            var result = _register.GetChildren();
            Assert.IsType<Dictionary<int, string>>(result);
        }
    }
}
