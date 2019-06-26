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
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;
    using Interfaces;

    #region CollectionDataContract
#if !SERIALIZE_LINQ_OPTIMIZE_SIZE
    [CollectionDataContract]
#else
    [CollectionDataContract(Name = "EIL")]
#endif
#if !SILVERLIGHT
    [Serializable]
#endif
    #endregion
    public class ElementInitNodeList : List<ElementInitNode>
    {
        public ElementInitNodeList() { }

        public ElementInitNodeList(INodeFactory factory, IEnumerable<ElementInit> items)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.AddRange(items.Select(item => new ElementInitNode(factory, item)));
        }

        internal IEnumerable<ElementInit> GetElementInits(ExpressionContext context)
        {
            return this.Select(item => item.ToElementInit(context));
        }
    }
}
