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
    [DataContract(Name = "MMB")]
#endif
#if !SILVERLIGHT
    [Serializable]
#endif
    #endregion
    public class MemberMemberBindingNode : MemberBindingNode
    {
        public MemberMemberBindingNode() { }

        public MemberMemberBindingNode(INodeFactory factory, MemberMemberBinding memberMemberBinding)
            : base(factory, memberMemberBinding.BindingType, memberMemberBinding.Member)
        {
            this.Bindings = new MemberBindingNodeList(factory, memberMemberBinding.Bindings);
        }

        #region DataMember
#if !SERIALIZE_LINQ_OPTIMIZE_SIZE
        [DataMember(EmitDefaultValue = false)]
#else
        [DataMember(EmitDefaultValue = false, Name = "B")]
#endif
        #endregion
        public MemberBindingNodeList Bindings { get; set; }

        internal override MemberBinding ToMemberBinding(ExpressionContext context)
        {
            return Expression.MemberBind(this.Member.ToMemberInfo(context), this.Bindings.GetMemberBindings(context));
        }
    }
}
