using System;
using System.Collections.Generic;
using System.Linq;
using Majid.Reflection.Extensions;
using JetBrains.Annotations;

namespace Majid.AspNetCore.Configuration
{
    public class ControllerAssemblySettingList : List<MajidControllerAssemblySetting>
    {
        [CanBeNull]
        public MajidControllerAssemblySetting GetSettingOrNull(Type controllerType)
        {
            return this.FirstOrDefault(controllerSetting => controllerSetting.Assembly == controllerType.GetAssembly());
        }
    }
}