#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

#if !WINDOWS_PHONE
#else
using Serialize.Linq.Internals;
#endif

namespace Serialize.Linq.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;
    using System.Reflection;
    using Nodes;

    public class ExpressionContext
    {
        private readonly ConcurrentDictionary<string, ParameterExpression> _parameterExpressions;
        private readonly ConcurrentDictionary<string, Type> _typeCache;

        public ExpressionContext()
        {
            this._parameterExpressions = new ConcurrentDictionary<string, ParameterExpression>();
            this._typeCache = new ConcurrentDictionary<string, Type>();
        }

        public bool AllowPrivateFieldAccess { get; set; }

        public virtual BindingFlags? GetBindingFlags()
        {
            if (!this.AllowPrivateFieldAccess)
            {
                return null;
            }

            return BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        }

        public virtual ParameterExpression GetParameterExpression(ParameterExpressionNode node)
        {
            if(node == null)
            {
                throw new ArgumentNullException("node");
            }

            var key = node.Type.Name + Environment.NewLine + node.Name;
            return this._parameterExpressions.GetOrAdd(key, k => Expression.Parameter(node.Type.ToType(this), node.Name));
        }

        public virtual Type ResolveType(TypeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (string.IsNullOrWhiteSpace(node.Name))
            {
                return null;
            }

            return this._typeCache.GetOrAdd(node.Name, n =>
            {
                var type = Type.GetType(n);
                if (type == null)
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        type = assembly.GetType(n);
                        if (type != null)
                        {
                            break;
                        }
                    }

                }
                return type;
            });
        }
    }
}
