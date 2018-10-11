using System;
using System.Collections.Generic;
using Majid.Collections;
using Majid.Runtime.Validation.Interception;

namespace Majid.Configuration.Startup
{
    public interface IValidationConfiguration
    {
        List<Type> IgnoredTypes { get; }

        /// <summary>
        /// A list of method parameter validators.
        /// </summary>
        ITypeList<IMethodParameterValidator> Validators { get; }
    }
}