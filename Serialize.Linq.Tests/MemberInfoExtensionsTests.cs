#region Copyright

//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq

#endregion

namespace Serialize.Linq.Tests
{
    using Core.Extensions;
    using Internals;
    using Xunit;

    public class MemberInfoExtensionsTests
    {
        [Fact]
        public void GetReturnTypeOfFieldTest()
        {
            var field = typeof(Bar).GetField("isFoo");
            var actual = field.GetReturnType();
            Assert.Equal(typeof(bool), actual);
        }

        [Fact]
        public void GetReturnTypeOfMethodTest()
        {
            var actual = typeof(Bar).GetMethod("GetName").GetReturnType();
            Assert.Equal(typeof(string), actual);
        }

        [Fact]
        public void GetReturnTypeOfPropertyTest()
        {
            var actual = typeof(Bar).GetProperty("FirstName").GetReturnType();
            Assert.Equal(typeof(string), actual);
        }
    }
}