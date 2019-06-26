namespace Serialize.Linq.Tests.Issues
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.Interfaces;
    using Core.Serializers;
    using Internals;
    using Xunit;

    // https://github.com/esskar/Serialize.Linq/issues/30
    public class Issue30
    {
        [Fact]
        public void SerializeLambdaWithNullableTest()
        {
            foreach (var textSerializer in new ITextSerializer[] {new JsonSerializer(), new XmlSerializer()})
            {
                var serializer = new ExpressionSerializer(textSerializer);
                var fish = new[]
                {
                    new Fish {Count = 0},
                    new Fish {Count = 1},
                    new Fish(),
                    new Fish {Count = 1}
                };
                int? count = 1;
                Expression<Func<Fish, bool>> expectedExpression = f => f.Count == count;
                var expected = fish.Where(expectedExpression.Compile()).Count();

                var serialized = serializer.SerializeText(expectedExpression);
                var actualExpression = (Expression<Func<Fish, bool>>) serializer.DeserializeText(serialized);
                var actual = fish.Where(actualExpression.Compile()).Count();

                Assert.Equal(expected, actual);
            }
        }
    }
}