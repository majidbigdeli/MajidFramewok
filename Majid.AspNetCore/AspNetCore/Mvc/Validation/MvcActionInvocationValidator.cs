using System.ComponentModel.DataAnnotations;
using Majid.AspNetCore.Mvc.Extensions;
using Majid.Collections.Extensions;
using Majid.Configuration.Startup;
using Majid.Dependency;
using Majid.Web.Validation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Validation
{
    public class MvcActionInvocationValidator : ActionInvocationValidatorBase
    {
        protected ActionExecutingContext ActionContext { get; private set; }

        public MvcActionInvocationValidator(IValidationConfiguration configuration, IIocResolver iocResolver)
            : base(configuration, iocResolver)
        {
        }

        public void Initialize(ActionExecutingContext actionContext)
        {
            ActionContext = actionContext;

            base.Initialize(actionContext.ActionDescriptor.GetMethodInfo());
        }

        protected override object GetParameterValue(string parameterName)
        {
            return ActionContext.ActionArguments.GetOrDefault(parameterName);
        }

        protected override void SetDataAnnotationAttributeErrors()
        {
            foreach (var state in ActionContext.ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    ValidationErrors.Add(new ValidationResult(error.ErrorMessage, new[] { state.Key }));
                }
            }
        }
    }
}