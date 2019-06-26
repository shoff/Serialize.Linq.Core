using System;
using System.Linq.Expressions;

namespace Serialize.Linq.Tests.Issues
{
    using Core.Extensions;
    using Xunit;

    /// <summary>
    /// https://github.com/esskar/Serialize.Linq/issues/39
    /// </summary>
    public class Issue39
    {
        private class DataPoint
        {
            public DateTime timestamp;
            public int acctId;
        }

        [Fact]
        public void ToExpressionNodeWithSimilarConstantNames()
        {
            var feb1 = new DateTime(2015, 2, 1);
            var feb15 = new DateTime(2015, 2, 15);

            Expression<Func<DataPoint, bool>> expression =
                dp => dp.timestamp >= feb1 && dp.timestamp < feb15 && dp.acctId == 1;

            var result = expression.ToExpressionNode();

            Assert.NotNull(result);
        }
    }
}
