using System;
using System.Reflection;
using Majid.Authorization.Roles;
using Majid.Authorization.Users;
using Majid.MultiTenancy;

namespace Majid.Zero.Configuration
{
    public class MajidZeroEntityTypes : IMajidZeroEntityTypes
    {
        public Type User
        {
            get { return _user; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof (MajidUserBase).IsAssignableFrom(value))
                {
                    throw new MajidException(value.AssemblyQualifiedName + " should be derived from " + typeof(MajidUserBase).AssemblyQualifiedName);
                }

                _user = value;
            }
        }
        private Type _user;

        public Type Role
        {
            get { return _role; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(MajidRoleBase).IsAssignableFrom(value))
                {
                    throw new MajidException(value.AssemblyQualifiedName + " should be derived from " + typeof(MajidRoleBase).AssemblyQualifiedName);
                }

                _role = value;
            }
        }
        private Type _role;

        public Type Tenant
        {
            get { return _tenant; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (!typeof(MajidTenantBase).IsAssignableFrom(value))
                {
                    throw new MajidException(value.AssemblyQualifiedName + " should be derived from " + typeof(MajidTenantBase).AssemblyQualifiedName);
                }

                _tenant = value;
            }
        }
        private Type _tenant;
    }
}