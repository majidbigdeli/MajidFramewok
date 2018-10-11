using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Results.Wrapping
{
    public class NullMajidActionResultWrapper : IMajidActionResultWrapper
    {
        public void Wrap(ResultExecutingContext actionResult)
        {
            
        }
    }
}