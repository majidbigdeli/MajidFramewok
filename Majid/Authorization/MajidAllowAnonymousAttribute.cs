using System;

namespace Majid.Authorization
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="MajidAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class MajidAllowAnonymousAttribute : Attribute, IMajidAllowAnonymousAttribute
    {

    }
}