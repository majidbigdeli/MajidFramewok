using System;
using System.Collections.Generic;
using Majid.Domain.Entities;

namespace Majid.EntityFramework
{
    public interface IDbContextEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}