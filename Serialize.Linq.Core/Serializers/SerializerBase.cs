#if WINDOWS_PHONE7
using Serialize.Linq.Internals;
#endif

namespace Serialize.Linq.Core.Serializers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class SerializerBase
    {
        private static readonly Type[] _knownTypes =
        { 
            typeof(bool),
            typeof(decimal), typeof(double),
            typeof(float),
            typeof(int), typeof(uint),
            typeof(short), typeof(ushort),
            typeof(long), typeof(ulong),
            typeof(string),
            typeof(DateTime), typeof(TimeSpan), typeof(Guid), typeof(DayOfWeek)
        };

        private readonly HashSet<Type> _customKnownTypes;
        private bool _autoAddKnownTypesAsArrayTypes;
        private bool _autoAddKnownTypesAsListTypes;

        protected SerializerBase()
        {
            this._customKnownTypes = new HashSet<Type>();
            this.AutoAddKnownTypesAsArrayTypes = true;
        }

        public bool AutoAddKnownTypesAsArrayTypes
        {
            get { return this._autoAddKnownTypesAsArrayTypes; }
            set
            {
                this._autoAddKnownTypesAsArrayTypes = value;
                if (value)
                {
                    this._autoAddKnownTypesAsListTypes = false;
                }
            }
        }

        public bool AutoAddKnownTypesAsListTypes
        {
            get { return this._autoAddKnownTypesAsListTypes; }
            set
            {
                this._autoAddKnownTypesAsListTypes = value;
                if (value)
                {
                    this._autoAddKnownTypesAsArrayTypes = false;
                }
            }
        }

        public void AddKnownType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this._customKnownTypes.Add(type);
        }

        public void AddKnownTypes(IEnumerable<Type> types)
        {
            if (types == null)
            {
                throw new ArgumentNullException("types");
            }

            foreach (var type in types)
            {
                this.AddKnownType(type);
            }
        }

        protected virtual IEnumerable<Type> GetKnownTypes()
        {
            return this.ExplodeKnownTypes(_knownTypes).Concat(this.ExplodeKnownTypes(this._customKnownTypes));
        }

        private IEnumerable<Type> ExplodeKnownTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                yield return type;
                if (this.AutoAddKnownTypesAsArrayTypes)
                {
                    yield return type.MakeArrayType();
                }
                else if (this.AutoAddKnownTypesAsListTypes)
                {
                    yield return typeof(List<>).MakeGenericType(type);
                }

                if (type.IsClass)
                {
                    continue;
                }

                var nullableType = typeof (Nullable<>).MakeGenericType(type);
                yield return nullableType;
                if (this.AutoAddKnownTypesAsArrayTypes)
                {
                    yield return nullableType.MakeArrayType();
                }
                else if (this.AutoAddKnownTypesAsListTypes)
                {
                    yield return typeof(List<>).MakeGenericType(nullableType);
                }
            }
        }
    }
}
