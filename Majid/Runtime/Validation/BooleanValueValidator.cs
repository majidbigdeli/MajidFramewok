using System;

namespace Majid.Runtime.Validation
{
    [Serializable]
    [Validator("BOOLEAN")]
    public class BooleanValueValidator : ValueValidatorBase
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                return true;
            }

            bool b;
            return bool.TryParse(value.ToString(), out b);
        }
    }
}