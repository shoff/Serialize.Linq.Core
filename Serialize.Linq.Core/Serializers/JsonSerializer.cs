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
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using Interfaces;

    public class JsonSerializer : TextSerializer, IJsonSerializer
    {
#if !WINDOWS_PHONE
        protected override XmlObjectSerializer CreateXmlObjectSerializer(Type type)
        {
            return new DataContractJsonSerializer(type, this.GetKnownTypes());
        }
#else
        private DataContractJsonSerializer CreateDataContractJsonSerializer(Type type)
        {
            return new DataContractJsonSerializer(type, this.GetKnownTypes());
        }

        public override void Serialize<T>(Stream stream, T obj)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var serializer = this.CreateDataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
        }

        public override T Deserialize<T>(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var serializer = this.CreateDataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }
#endif
    }
}