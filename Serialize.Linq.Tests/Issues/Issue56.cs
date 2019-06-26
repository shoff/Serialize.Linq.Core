namespace Serialize.Linq.Tests.Issues
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core.Serializers;
    using Xunit;

    /// <summary>
    ///     https://github.com/esskar/Serialize.Linq/issues/56
    /// </summary>
    public class Issue56
    {
        private class GroupEntityWithoutNullable
        {
            public long GroupEnterpriseKey { get; set; }
        }

        private class GroupEntityWithNullable
        {
            public long? GroupEnterpriseKey { get; set; }
        }

        [Fact]
        public void SerializeContainsWithNullablesAndWithNullableKey()
        {
            var enterpriseKeys = new List<long?> {1, 2, 3, 4};

            var predicatePart =
                (Expression<Func<GroupEntityWithNullable, bool>>)
                (groupType =>
                    enterpriseKeys.Contains(groupType.GroupEnterpriseKey));

            var serializeTo = new ExpressionSerializer(new XmlSerializer())
            {
                AutoAddKnownTypesAsListTypes = true
            };
            var predicatePartSerializedToString = serializeTo.SerializeText(predicatePart);

            Assert.NotNull(predicatePartSerializedToString);
        }

        [Fact]
        public void SerializeContainsWithNullablesAndWithoutNullableKey()
        {
            var enterpriseKeys = new List<long?> {1, 2, 3, 4};

            var predicatePart =
                (Expression<Func<GroupEntityWithoutNullable, bool>>)
                (groupType =>
                    enterpriseKeys.Contains(groupType.GroupEnterpriseKey));

            var serializeTo = new ExpressionSerializer(new XmlSerializer())
            {
                AutoAddKnownTypesAsListTypes = true
            };
            var predicatePartSerializedToString = serializeTo.SerializeText(predicatePart);

            Assert.NotNull(predicatePartSerializedToString);
        }
    }
}