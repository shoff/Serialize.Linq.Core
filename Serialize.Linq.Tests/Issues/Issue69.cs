namespace Serialize.Linq.Tests.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core.Serializers;
    using Internals;
    using Xunit;

    /// <summary>
    ///     https://github.com/esskar/Serialize.Linq/issues/69
    /// </summary>
    public class Issue69
    {
        public Issue69()
        {
            this.jsonExpressionSerializer = new ExpressionSerializer(new JsonSerializer());
        }

        private readonly ExpressionSerializer jsonExpressionSerializer;

        private void SerialzeAndDeserializeDateTimeJson(DateTime dt)
        {
            Expression<Func<DateTime>> actual = () => dt;
            actual = actual.Update(Expression.Constant(dt), new List<ParameterExpression>());

            var serialized = this.jsonExpressionSerializer.SerializeText(actual);
            var expected = this.jsonExpressionSerializer.DeserializeText(serialized);
            ExpressionAssert.AreEqual(expected, actual);
        }

        [Fact]
        public void JsonSerialzeAndDeserialize1969Local()
        {
            this.SerialzeAndDeserializeDateTimeJson(new DateTime(1969, 1, 1, 0, 0, 0, DateTimeKind.Local));
        }

        [Fact]
        public void JsonSerialzeAndDeserialize1969Utc()
        {
            this.SerialzeAndDeserializeDateTimeJson(new DateTime(1969, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }

        [Fact]
        public void JsonSerialzeAndDeserialize1970Local()
        {
            this.SerialzeAndDeserializeDateTimeJson(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local));
        }

        [Fact]
        public void JsonSerialzeAndDeserialize1970Utc()
        {
            this.SerialzeAndDeserializeDateTimeJson(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }

        [Fact]
        public void JsonSerialzeAndDeserialize1971Local()
        {
            this.SerialzeAndDeserializeDateTimeJson(new DateTime(1971, 1, 1, 0, 0, 0, DateTimeKind.Local));
        }

        [Fact]
        public void JsonSerialzeAndDeserialize1971Utc()
        {
            this.SerialzeAndDeserializeDateTimeJson(new DateTime(1971, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}