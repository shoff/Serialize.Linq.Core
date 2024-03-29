﻿#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

namespace Serialize.Linq.Core.Nodes
{
    using System;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using Interfaces;

    #region DataContract
#if !SERIALIZE_LINQ_OPTIMIZE_SIZE
    [DataContract]
#else
    [DataContract(Name = "U")]
#endif
#if !SILVERLIGHT
    [Serializable]
#endif
    #endregion
    public class UnaryExpressionNode : ExpressionNode<UnaryExpression>
    {
        public UnaryExpressionNode() { }

        public UnaryExpressionNode(INodeFactory factory, UnaryExpression expression)
            : base(factory, expression) { }

        #region DataMember
#if !SERIALIZE_LINQ_OPTIMIZE_SIZE
        [DataMember(EmitDefaultValue = false)]
#else
        [DataMember(EmitDefaultValue = false, Name = "O")]
#endif
        #endregion
        public ExpressionNode Operand { get; set; }

        protected override void Initialize(UnaryExpression expression)
        {
            this.Operand = this.Factory.Create(expression.Operand);
        }

        public override Expression ToExpression(ExpressionContext context)
        {
            return this.NodeType == ExpressionType.UnaryPlus
                ? Expression.UnaryPlus(this.Operand.ToExpression(context))
                : Expression.MakeUnary(this.NodeType, this.Operand.ToExpression(context), this.Type.ToType(context));
        }
    }
}
