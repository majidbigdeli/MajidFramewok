using System;

namespace Majid.Runtime.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidatorAttribute : Attribute
    {
        public string Name { get; set; }

        public ValidatorAttribute(string name)
        {
            Name = name;
        }
    }
}