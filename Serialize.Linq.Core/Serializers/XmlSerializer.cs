#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

namespace Serialize.Linq.Core.Serializers
{
    using System;
    using System.Runtime.Serialization;
    using Interfaces;

    public class XmlSerializer : TextSerializer, IXmlSerializer
    {
#if !WINDOWS_PHONE
        protected override XmlObjectSerializer CreateXmlObjectSerializer(Type type)
        {
            return new DataContractSerializer(type, this.GetKnownTypes());
        }
#else
        private DataContractSerializer CreateDataContractSerializer(Type type)
        {
            return new DataContractSerializer(type, this.GetKnownTypes());
        }

        public override void Serialize<T>(Stream stream, T obj)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var serializer = this.CreateDataContractSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
        }

        public override T Deserialize<T>(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var serializer = this.CreateDataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }
#endif
    }
}