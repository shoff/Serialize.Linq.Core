#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

#if !WINDOWS_PHONE
#endif

namespace Serialize.Linq.Core.Serializers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using Interfaces;
    using Nodes;

    public abstract class DataSerializer : SerializerBase, ISerializer
    {
#if !WINDOWS_PHONE
        public virtual void Serialize<T>(Stream stream, T obj) where T : Node
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            var serializer = this.CreateXmlObjectSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
        }

        public virtual T Deserialize<T>(Stream stream) where T : Node
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            var serializer = this.CreateXmlObjectSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        protected abstract XmlObjectSerializer CreateXmlObjectSerializer(Type type);

#else

        public abstract void Serialize<T>(Stream stream, T obj) where T : Node;

        public abstract T Deserialize<T>(Stream stream) where T : Node;

#endif
    }
}
