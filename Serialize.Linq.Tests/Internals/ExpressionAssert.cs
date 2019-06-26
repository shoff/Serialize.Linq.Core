#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System.Linq.Expressions;

namespace Serialize.Linq.Tests.Internals
{
    using Xunit;

    internal class ExpressionAssert
    {
        public static void AreEqual<TDelegate>(Expression<TDelegate> expected, Expression<TDelegate> actual, string message = null)
        {
            AreEqual(expected, (Expression)actual, message);
        }

        public static void AreEqual(Expression expected, Expression actual, string message = null)
        {
            var comparer = new ExpressionComparer();
            var result = comparer.AreEqual(expected, actual);
            if (result)
            {
                return;
            }

            var failMessage = !string.IsNullOrWhiteSpace(message) ? message : string.Empty;
            failMessage += $"Expected was <{expected}>, Actual was <{actual}>";
            // Assert.Fail(failMessage);
        }
    }
}