using System.ComponentModel.DataAnnotations.Schema;
using Majid.Domain.Entities;
using Majid.Domain.Entities.Auditing;

namespace Majid.Authorization.Users
{
    /// <summary>
    /// Represents role record of a user. 
    /// </summary>
    [Table("MajidUserRoles")]
    public class UserRole : CreationAuditedEntity<long>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// User id.
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// Role id.
        /// </summary>
        public virtual int RoleId { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        public UserRole()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UserRole"/> object.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        public UserRole(int? tenantId, long userId, int roleId)
        {
            TenantId = tenantId;
            UserId = userId;
            RoleId = roleId;
        }
    }
}