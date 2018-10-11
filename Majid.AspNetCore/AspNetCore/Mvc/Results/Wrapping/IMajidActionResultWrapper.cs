using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Results.Wrapping
{
    public interface IMajidActionResultWrapper
    {
        void Wrap(ResultExecutingContext actionResult);
    }
}