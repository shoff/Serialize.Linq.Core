﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Serialize.Linq.Tests.Internals;

namespace Serialize.Linq.Tests.Issues
{
    using Core.Interfaces;
    using Core.Serializers;
    using Xunit;

    // https://github.com/esskar/Serialize.Linq/issues/35
    public class Issue35
    {
        [Fact]
        public void LetExpressionTests()
        {
            var expressions = new List<Expression>();

            Expression<Func<IEnumerable<int>, IEnumerable<int>>> intExpr = c =>
                from x in c
                let test = 8
                where x == test
                select x;
            expressions.Add(intExpr);

            Expression<Func<IEnumerable<string>, IEnumerable<string>>> strExpr = c =>
                from x in c
                let test = "bar"
                where x == test
                select x;
            expressions.Add(strExpr);            

            foreach (var textSerializer in new ITextSerializer[] { new JsonSerializer(), new XmlSerializer() })
            {
                var serializer = new ExpressionSerializer(textSerializer);
                foreach (var expected in expressions)
                {
                    var serialized = serializer.SerializeText(expected);
                    var actual = serializer.DeserializeText(serialized);

                    ExpressionAssert.AreEqual(expected, actual);
                }
            }
        }
    }
}
