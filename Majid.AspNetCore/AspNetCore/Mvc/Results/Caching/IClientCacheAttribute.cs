using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Results.Caching
{
    public interface IClientCacheAttribute
    {
        void Apply(ResultExecutingContext context);
    }
}