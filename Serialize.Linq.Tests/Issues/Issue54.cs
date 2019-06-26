namespace Serialize.Linq.Tests.Issues
{
    using System;
    using System.Linq.Expressions;
    using Core;
    using Core.Factories;
    using Core.Serializers;
    using Xunit;

    /// <summary>
    ///     https://github.com/esskar/Serialize.Linq/issues/54
    /// </summary>
    public class Issue54
    {
        public int publicField;
        private int privateField;
        protected int protectedField;
        internal int internalField;
        protected internal int protectedInternalField;

        private static int privateStaticField;
        public static int publicStaticField;
        protected static int protectedStaticField;
        internal static int internalStaticField;
        protected internal static int protectedInternalStaticField;

        private void TestExpression(Expression<Func<Test, bool>> expression, ReadFieldOn readFieldOn)
        {
            var initialValue = 42;
            var actualValue = -1;

            // Initialize fields
            this.SetFields(initialValue);

            // Serialize expression
            var settings = new FactorySettings
            {
                AllowPrivateFieldAccess = true
            };
            var serializer = new ExpressionSerializer(new JsonSerializer());
            var value = serializer.SerializeText(expression, settings);

            // Modify fields
            this.SetFields(actualValue);

            // Deserialize expression
            var actualExpression =
                (Expression<Func<Test, bool>>) serializer.DeserializeText(value,
                    new ExpressionContext {AllowPrivateFieldAccess = true});
            var func = actualExpression.Compile();

            // Set expected value.
            var expectedValue = readFieldOn == ReadFieldOn.Serialization
                ? initialValue
                : actualValue;

            // Assert
            Assert.True(func(new Test {IntProperty = expectedValue}));
        }

        public void SetFields(int value)
        {
            this.publicField = value;
            this.privateField = value;
            this.protectedField = value;
            this.internalField = value;
            this.protectedInternalField = value;
            privateStaticField = value;
            publicStaticField = value;
            protectedStaticField = value;
            internalStaticField = value;
            protectedInternalStaticField = value;
        }

        public class Test
        {
            public int IntProperty { get; set; }
        }

        public enum ReadFieldOn
        {
            Serialization, Execution
        }

        [Fact]
        public void SerializeInternalField()
        {
            this.TestExpression(test => test.IntProperty == this.internalField, ReadFieldOn.Serialization);
        }

        [Fact]
        public void SerializeInternalStaticField()
        {
            this.TestExpression(test => test.IntProperty == internalStaticField, ReadFieldOn.Execution);
        }

        [Fact]
        public void SerializePrivateField()
        {
            this.TestExpression(test => test.IntProperty == this.privateField, ReadFieldOn.Serialization);
        }

        [Fact]
        public void SerializePrivateStaticField()
        {
            this.TestExpression(test => test.IntProperty == privateStaticField, ReadFieldOn.Serialization);
        }

        [Fact]
        public void SerializeProtectedField()
        {
            this.TestExpression(test => test.IntProperty == this.protectedField, ReadFieldOn.Serialization);
        }

        [Fact]
        public void SerializeProtectedInternalField()
        {
            this.TestExpression(test => test.IntProperty == this.protectedInternalField, ReadFieldOn.Serialization);
        }

        [Fact]
        public void SerializeProtectedInternalStaticField()
        {
            this.TestExpression(test => test.IntProperty == protectedInternalStaticField, ReadFieldOn.Execution);
        }

        [Fact]
        public void SerializeProtectedStaticField()
        {
            this.TestExpression(test => test.IntProperty == protectedStaticField, ReadFieldOn.Execution);
        }

        [Fact]
        public void SerializePublicField()
        {
            this.TestExpression(test => test.IntProperty == this.publicField, ReadFieldOn.Serialization);
        }

        [Fact]
        public void SerializePublicStaticField()
        {
            this.TestExpression(test => test.IntProperty == publicStaticField, ReadFieldOn.Execution);
        }
    }
}