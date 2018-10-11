using System;
using System.Collections.Generic;
using AutoMapper;

namespace Majid.AutoMapper
{
    public class MajidAutoMapperConfiguration : IMajidAutoMapperConfiguration
    {
        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public bool UseStaticMapper { get; set; }

        public MajidAutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }
    }
}