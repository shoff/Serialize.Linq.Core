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
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Interfaces;

    #region DataContract
#if !SERIALIZE_LINQ_OPTIMIZE_SIZE
    [DataContract]
#else
    [DataContract(Name = "CI")]
#endif
#if !SILVERLIGHT
    [Serializable]
#endif
    #endregion
    public class ConstructorInfoNode : MemberNode<ConstructorInfo>
    {
        public ConstructorInfoNode() { }

        public ConstructorInfoNode(INodeFactory factory, ConstructorInfo memberInfo)
            : base(factory, memberInfo) { }

        /// <summary>
        /// Gets the member infos for the specified type.
        /// </summary>
        /// <param name="context">The expression context.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected override IEnumerable<ConstructorInfo> GetMemberInfosForType(ExpressionContext context, Type type)
        {
            return type.GetConstructors();
        }
    }
}