using System;
using System.Reflection;
using Majid.AspNetCore.Configuration;
using Majid.Dependency;
using Majid.Extensions;
using Majid.Reflection;
using Majid.Timing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Majid.Json
{
   public class MajidMvcContractResolver : DefaultContractResolver
    {
        private readonly IIocResolver _iocResolver;
        private bool? _useMvcDateTimeFormat { get; set; }
        private string _datetimeFormat { get; set; } = null;

        public MajidMvcContractResolver(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null)
            {
                var converter = new MajidDateTimeConverter();

                if (!_useMvcDateTimeFormat.HasValue)
                {
                    using (var configuration = _iocResolver.ResolveAsDisposable<IMajidAspNetCoreConfiguration>())
                    {
                        _useMvcDateTimeFormat = configuration.Object.UseMvcDateTimeFormatForAppServices;
                    }
                }

                // try to resolve MvcJsonOptions
                if (_useMvcDateTimeFormat.Value)
                {
                    using (var mvcJsonOptions = _iocResolver.ResolveAsDisposable<IOptions<MvcJsonOptions>>())
                    {
                        _datetimeFormat = mvcJsonOptions.Object.Value.SerializerSettings.DateFormatString;
                    }
                }

                // apply DateTimeFormat only if not empty
                if (!_datetimeFormat.IsNullOrWhiteSpace())
                {
                    converter.DateTimeFormat = _datetimeFormat;
                }

                property.Converter = converter;
            }
        }
    }      
}