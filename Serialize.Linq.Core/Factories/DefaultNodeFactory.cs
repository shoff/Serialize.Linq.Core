﻿#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

namespace Serialize.Linq.Core.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Interfaces;
    using Internals;
    using Nodes;

    public class DefaultNodeFactory : INodeFactory
    {
        private readonly FactorySettings _factorySettings;
        private readonly INodeFactory _innerFactory;
        private readonly Type[] _types;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNodeFactory"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="factorySettings">The factory settings to use.</param>
        public DefaultNodeFactory(Type type, FactorySettings factorySettings = null)
            : this(new [] { type }, factorySettings) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNodeFactory"/> class.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="factorySettings">The factory settings to use.</param>
        /// <exception cref="System.ArgumentNullException">types</exception>
        /// <exception cref="System.ArgumentException">types</exception>
        public DefaultNodeFactory(IEnumerable<Type> types, FactorySettings factorySettings = null)
        {
            if(types == null)
            {
                throw new ArgumentNullException("types");
            }

            this._types = types.ToArray();
            if(this._types.Any(t => t == null))
            {
                throw new ArgumentException("types");
            }

            this._factorySettings = factorySettings ?? new FactorySettings();
            this._innerFactory = this.CreateFactory();
        }

        public FactorySettings Settings
        {
            get { return this._factorySettings; }
        }

        /// <summary>
        /// Creates the specified expression node an expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public ExpressionNode Create(Expression expression)
        {
            return this._innerFactory.Create(expression);
        }

        /// <summary>
        /// Creates the specified type node from a type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public TypeNode Create(Type type)
        {
            return this._innerFactory.Create(type);
        }

        /// <summary>
        /// Gets binding flags to be used when accessing type members.
        /// </summary>
        public BindingFlags? GetBindingFlags()
        {
            return this._innerFactory.GetBindingFlags();
        }

        /// <summary>
        /// Creates the factory.
        /// </summary>
        /// <returns></returns>
        private INodeFactory CreateFactory()
        {
            var expectedTypes = new HashSet<Type>();
            foreach (var type in this._types)
            {
                expectedTypes.UnionWith(GetComplexMemberTypes(type));
            }

            return new TypeResolverNodeFactory(expectedTypes, this.Settings);
        }

        /// <summary>
        /// Gets the complex member types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static IEnumerable<Type> GetComplexMemberTypes(Type type)        
        {
            var typeFinder = new ComplexPropertyMemberTypeFinder();
            return typeFinder.FindTypes(type);
        }
    }
}
