using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Majid.AspNetCore.Configuration
{
    public interface IMajidControllerAssemblySettingBuilder
    {
        MajidControllerAssemblySettingBuilder Where(Func<Type, bool> predicate);

        MajidControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer);
    }
}