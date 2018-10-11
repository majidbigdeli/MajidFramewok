﻿using Majid.Application.Features;
using Majid.Domain.Entities;

namespace Majid.MultiTenancy
{
    /// <summary>
    /// Feature setting for a Tenant (<see cref="MajidTenant{TUser}"/>).
    /// </summary>
    public class TenantFeatureSetting : FeatureSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantFeatureSetting"/> class.
        /// </summary>
        public TenantFeatureSetting()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantFeatureSetting"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="name">Feature name.</param>
        /// <param name="value">Feature value.</param>
        public TenantFeatureSetting(int tenantId, string name, string value)
            :base(name, value)
        {
            TenantId = tenantId;
        }
    }
}