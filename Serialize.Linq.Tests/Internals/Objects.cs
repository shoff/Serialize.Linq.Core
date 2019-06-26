﻿#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

namespace Serialize.Linq.Tests.Internals
{
    public interface IFoo
    {
        string Name { get; set; }
    }

    public class Foo : IFoo
    {
        public string Name { get; set; }
    }

    public class Bar
    {
        public bool isFoo;

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GetName()
        {
            return $"{this.FirstName} {this.LastName}";
        }
    }

    public class Fish
    {
        public int? Count { get; set; }
    }

    public struct EmptyStruct { }

    public struct Struct
    {
        public string Name { get; set; }
    }
}
