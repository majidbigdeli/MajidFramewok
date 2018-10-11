using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Majid.Runtime.Validation.Interception
{
    public class ValidatableObjectValidator : IMethodParameterValidator
    {
        public virtual IReadOnlyList<ValidationResult> Validate(object validatingObject)
        {
            var validationErrors = new List<ValidationResult>();

            if (validatingObject is IValidatableObject o)
            {
                validationErrors.AddRange(o.Validate(new ValidationContext(o)));
            }

            return validationErrors;
        }
    }
}
