﻿using System.Globalization;
using Majid.Configuration;
using Majid.Dependency;
using Majid.Domain.Entities;
using Majid.Localization;
using AutoMapper;
using System.Linq;

namespace Majid.AutoMapper
{
    public static class AutoMapExtensions
    {
        /// <summary>
        /// Converts an object to another using AutoMapper library. Creates a new object of <typeparamref name="TDestination"/>.
        /// There must be a mapping between objects before calling this method.
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination object</typeparam>
        /// <param name="source">Source object</param>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// There must be a mapping between objects before calling this method.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public static void CreateMultiLingualMap<TMultiLingualEntity, TMultiLingualEntityPrimaryKey, TTranslation, TDestination>(
            this IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
            where TTranslation : class, IEntityTranslation<TMultiLingualEntity, TMultiLingualEntityPrimaryKey>
            where TMultiLingualEntity : IMultiLingualEntity<TTranslation>
        {
            configuration.CreateMap<TTranslation, TDestination>();

            configuration.CreateMap<TMultiLingualEntity, TDestination>().BeforeMap((source, destination, context) =>
            {
                var translation = source.Translations.FirstOrDefault(pt => pt.Language == CultureInfo.CurrentUICulture.Name);
                if (translation != null)
                {
                    context.Mapper.Map(translation, destination);
                    return;
                }

                var defaultLanguage = multiLingualMapContext.SettingManager
                                                            .GetSettingValue(LocalizationSettingNames.DefaultLanguage);

                translation = source.Translations.FirstOrDefault(pt => pt.Language == defaultLanguage);
                if (translation != null)
                {
                    context.Mapper.Map(translation, destination);
                    return;
                }

                translation = source.Translations.FirstOrDefault();
                if (translation != null)
                {
                    context.Mapper.Map(translation, destination);
                }
            });
        }

        public static void CreateMultiLingualMap<TMultiLingualEntity, TTranslation, TDestination>(this IMapperConfigurationExpression configuration, MultiLingualMapContext multiLingualMapContext)
            where TTranslation : class, IEntity, IEntityTranslation<TMultiLingualEntity, int>
            where TMultiLingualEntity : IMultiLingualEntity<TTranslation>
        {
            configuration.CreateMultiLingualMap<TMultiLingualEntity, int, TTranslation, TDestination>(multiLingualMapContext);
        }
    }
}
