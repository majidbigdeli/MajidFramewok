using System;
using System.Collections.Generic;
using System.Reflection;
using Majid.AspNetCore.Mvc.Results.Caching;
using Majid.Domain.Uow;
using Majid.Web.Models;
using Microsoft.AspNetCore.Routing;

namespace Majid.AspNetCore.Configuration
{
    public class MajidAspNetCoreConfiguration : IMajidAspNetCoreConfiguration
    {
        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        public IClientCacheAttribute DefaultClientCacheAttribute { get; set; }

        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        public List<Type> FormBodyBindingIgnoredTypes { get; }

        public ControllerAssemblySettingList ControllerAssemblySettings { get; }

        public bool IsValidationEnabledForControllers { get; set; }

        public bool IsAuditingEnabled { get; set; }

        public bool SetNoCacheForAjaxResponses { get; set; }

        public bool UseMvcDateTimeFormatForAppServices { get; set; }

        public List<Action<IRouteBuilder>> RouteConfiguration { get; }

        public MajidAspNetCoreConfiguration()
        {
            DefaultWrapResultAttribute = new WrapResultAttribute();
            DefaultClientCacheAttribute = new NoClientCacheAttribute(false);
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            ControllerAssemblySettings = new ControllerAssemblySettingList();
            FormBodyBindingIgnoredTypes = new List<Type>();
            RouteConfiguration = new List<Action<IRouteBuilder>>();
            IsValidationEnabledForControllers = true;
            SetNoCacheForAjaxResponses = true;
            IsAuditingEnabled = true;
            UseMvcDateTimeFormatForAppServices = false;
            
        }
       
        public MajidControllerAssemblySettingBuilder CreateControllersForAppServices(
            Assembly assembly,
            string moduleName = MajidControllerAssemblySetting.DefaultServiceModuleName,
            bool useConventionalHttpVerbs = true)
        {
            var setting = new MajidControllerAssemblySetting(moduleName, assembly, useConventionalHttpVerbs);
            ControllerAssemblySettings.Add(setting);
            return new MajidControllerAssemblySettingBuilder(setting);
        }
    }
}