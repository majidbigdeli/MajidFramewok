using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Majid.Authorization
{
    public interface IAuthorizationHelper
    {
        Task AuthorizeAsync(IEnumerable<IMajidAuthorizeAttribute> authorizeAttributes);

        Task AuthorizeAsync(MethodInfo methodInfo, Type type);
    }
}