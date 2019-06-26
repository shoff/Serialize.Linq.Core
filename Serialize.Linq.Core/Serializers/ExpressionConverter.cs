﻿namespace Serialize.Linq.Core.Serializers
{
    using System.Linq;
    using System.Linq.Expressions;
    using Factories;
    using Interfaces;
    using Nodes;

    public class ExpressionConverter
    {
        public ExpressionNode Convert(Expression expression, FactorySettings factorySettings = null)
        {
            var factory = this.CreateFactory(expression, factorySettings);
            return factory.Create(expression);
        }

        protected virtual INodeFactory CreateFactory(Expression expression, FactorySettings factorySettings)
        {
            var lambda = expression as LambdaExpression;
            if(lambda != null)
            {
                return new DefaultNodeFactory(lambda.Parameters.Select(p => p.Type), factorySettings);
            }

            return new NodeFactory(factorySettings);
        }        
    }
}
