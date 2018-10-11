using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Results.Wrapping
{
    public class MajidActionResultWrapperFactory : IMajidActionResultWrapperFactory
    {
        public IMajidActionResultWrapper CreateFor(ResultExecutingContext actionResult)
        {
            Check.NotNull(actionResult, nameof(actionResult));

            if (actionResult.Result is ObjectResult)
            {
                return new MajidObjectActionResultWrapper(actionResult.HttpContext.RequestServices);
            }

            if (actionResult.Result is JsonResult)
            {
                return new MajidJsonActionResultWrapper();
            }

            if (actionResult.Result is EmptyResult)
            {
                return new MajidEmptyActionResultWrapper();
            }

            return new NullMajidActionResultWrapper();
        }
    }
}