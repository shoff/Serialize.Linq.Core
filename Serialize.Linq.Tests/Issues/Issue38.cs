namespace Serialize.Linq.Tests.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core.Serializers;
    using Xunit;

    public class Issue38
    {
        public class Order
        {
            public virtual int Id { get; set; }
            public virtual int Qty { get; set; }
        }

        public class Document
        {
            public virtual int Id { get; set; }
            public virtual ICollection<Order> Orders { get; set; }
        }

        [Fact]
        public void SerializeAndDeserializeAsQueryableWithPredicateTest()
        {
            Expression<Func<Order, bool>> predicate = x => x.Id > 0 && x.Id < 5;

            Expression<Func<Document, bool>> pred = x => x.Orders.AsQueryable().Any(predicate);

            var serializer = new ExpressionSerializer(new BinarySerializer());
            var value = serializer.SerializeBinary(pred);

            var expression = serializer.DeserializeBinary(value);
            Assert.NotNull(expression);
        }

        [Fact]
        public void SerializeAsQueryableWithPredicateTest()
        {
            Expression<Func<Order, bool>> predicate = x => x.Id > 0 && x.Id < 5;

            Expression<Func<Document, bool>> pred = x => x.Orders.AsQueryable().Any(predicate);

            var serializer = new ExpressionSerializer(new BinarySerializer());
            var value = serializer.SerializeBinary(pred);

            Assert.NotNull(value);
        }
    }
}