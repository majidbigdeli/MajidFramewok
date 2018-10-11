namespace Majid.Zero.Configuration
{
    /// <summary>
    /// Configuration options for zero module.
    /// </summary>
    public interface IMajidZeroConfig
    {
        /// <summary>
        /// Gets role management config.
        /// </summary>
        IRoleManagementConfig RoleManagement { get; }

        /// <summary>
        /// Gets user management config.
        /// </summary>
        IUserManagementConfig UserManagement { get; }

        /// <summary>
        /// Gets language management config.
        /// </summary>
        ILanguageManagementConfig LanguageManagement { get; }

        /// <summary>
        /// Gets entity type config.
        /// </summary>
        IMajidZeroEntityTypes EntityTypes { get; }
    }
}