#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Serialize.Linq.Tests.Internals;

namespace Serialize.Linq.Tests
{
    using Core.Extensions;
    using Core.Factories;
    using Core.Interfaces;
    using Core.Nodes;
    using Xunit;

    public class ExpressionNodeTests
    {
        [Fact]
        public void SimpleBinaryExpressionTest()
        {
            this.AssertExpression(Expression.Add(Expression.Constant(5), Expression.Constant(10)));
            this.AssertExpression(Expression.Subtract(Expression.Constant(5), Expression.Constant(10)));
            this.AssertExpression(Expression.Multiply(Expression.Constant(5), Expression.Constant(10)));
            this.AssertExpression(Expression.Divide(Expression.Constant(5), Expression.Constant(10)));

            this.AssertExpression(Expression.AddAssign(Expression.Variable(typeof(int), "x"), Expression.Constant(10)));
            this.AssertExpression(Expression.SubtractAssign(Expression.Variable(typeof(int), "x"), Expression.Constant(10)));
            this.AssertExpression(Expression.MultiplyAssign(Expression.Variable(typeof(int), "x"), Expression.Constant(10)));
            this.AssertExpression(Expression.DivideAssign(Expression.Variable(typeof(int), "x"), Expression.Constant(10)));
        }

        [Fact]
        public void SimpleConditionalTest()
        {
            this.AssertExpression(Expression.Condition(Expression.Constant(true), Expression.Constant(5), Expression.Constant(10)));
            this.AssertExpression(Expression.Condition(Expression.Constant(false), Expression.Constant(5), Expression.Constant(10)));
        }

        [Fact]
        public void SimpleConditionalWithNullConstantTest()
        {
            var argParam = Expression.Parameter(typeof(Type), "type");
            var stringProperty = Expression.Property(argParam, "AssemblyQualifiedName");

            this.AssertExpression(Expression.Condition(Expression.Constant(true), stringProperty, Expression.Constant(null, typeof(string))));
        }

        [Fact]
        public void SimpleUnaryTest()
        {
            this.AssertExpression(Expression.UnaryPlus(Expression.Constant(43)));
        }

        [Fact]
        public void SimpleTypedNullConstantTest()
        {
            this.AssertExpression(Expression.Constant(null, typeof(string)));
        }

        [Fact]
        public void SimpleLambdaTest()
        {
            this.AssertExpression(Expression.Lambda(Expression.Constant("body"), Expression.Parameter(typeof(string))));
        }

        [Fact]
        public void SimpleTypeBinaryTest()
        {
            this.AssertExpression(Expression.TypeIs(Expression.Variable(this.GetType()), typeof(object)));
            this.AssertExpression(Expression.TypeEqual(Expression.Variable(this.GetType()), typeof(object)));
        }

        [Fact]
        public void SimpleMemberTest()
        {
            var type = this.GetType();
            var property = type.GetProperty("TestContext");

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            this.AssertExpression(propertyAccess);
        }

        [Fact]
        public void ToExpressionNodeTest()
        {
            var expressions = SerializerTestData.TestExpressions;
            var nodes = SerializerTestData.TestNodesOnlyExpressions;
            AssertToExpressionNode(expressions);
            AssertToExpressionNode(nodes);
        }

        private static void AssertToExpressionNode(IEnumerable<Expression> expressions)
        {
            foreach (var expression in expressions)
            {
                ExpressionNode node = expression.ToExpressionNode();

                if (expression != null)
                {
                    Assert.NotNull(node);
                }
                else
                {
                    Assert.Null(node);
                }
            }
        }

        private void AssertExpression(Expression expression, string message = null)
        {
            this.AssertExpression<NodeFactory>(expression, message);
        }

        private void AssertExpression<TFactory>(Expression expression, string message = null)
            where TFactory : INodeFactory
        {
            var factory = (TFactory)Activator.CreateInstance(typeof(TFactory));
            var expressionNode = factory.Create(expression);
            var createdExpression = expressionNode.ToExpression();

            ExpressionAssert.AreEqual(expression, createdExpression, message);
        }
        public string TestContext { get; set; } = "Test Context";
    }
}
