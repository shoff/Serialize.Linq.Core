﻿namespace Serialize.Linq.Core.Exceptions
{
    using System;

    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException(string message, Type declaringType, string memberSignature)
            : base(message)
        {
            this.DeclaringType = declaringType;
            this.MemberSignature = memberSignature;
        }

        public Type DeclaringType { get; private set; }

        public string MemberSignature { get; private set; }

        public override string ToString()
        {
            return string.Format("{1}.{0}Declaring Type: '{2}'{0}Member Signature: '{3}'",
                Environment.NewLine,
                this.Message, this.DeclaringType, this.MemberSignature);
        }
    }
}