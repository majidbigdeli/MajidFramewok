using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Majid.AspNetCore.Configuration
{
    public class MajidControllerAssemblySettingBuilder : IMajidControllerAssemblySettingBuilder
    {
        private readonly MajidControllerAssemblySetting _setting;

        public MajidControllerAssemblySettingBuilder(MajidControllerAssemblySetting setting)
        {
            _setting = setting;
        }

        public MajidControllerAssemblySettingBuilder Where(Func<Type, bool> predicate)
        {
            _setting.TypePredicate = predicate;
            return this;
        }

        public MajidControllerAssemblySettingBuilder ConfigureControllerModel(Action<ControllerModel> configurer)
        {
            _setting.ControllerModelConfigurer = configurer;
            return this;
        }
    }
}