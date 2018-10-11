using Majid.Dependency;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Majid.AspNetCore.Mvc.Results.Wrapping
{
    public interface IMajidActionResultWrapperFactory : ITransientDependency
    {
        IMajidActionResultWrapper CreateFor([NotNull] ResultExecutingContext actionResult);
    }
}