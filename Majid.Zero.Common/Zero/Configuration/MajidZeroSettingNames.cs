namespace Majid.Zero.Configuration
{
    public static class MajidZeroSettingNames
    {
        public static class UserManagement
        {
            /// <summary>
            /// "Majid.Zero.UserManagement.IsEmailConfirmationRequiredForLogin".
            /// </summary>
            public const string IsEmailConfirmationRequiredForLogin = "Majid.Zero.UserManagement.IsEmailConfirmationRequiredForLogin";

            public static class UserLockOut
            {
                /// <summary>
                /// "Majid.Zero.UserManagement.UserLockOut.IsEnabled".
                /// </summary>
                public const string IsEnabled = "Majid.Zero.UserManagement.UserLockOut.IsEnabled";

                /// <summary>
                /// "Majid.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout".
                /// </summary>
                public const string MaxFailedAccessAttemptsBeforeLockout = "Majid.Zero.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout";

                /// <summary>
                /// "Majid.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds".
                /// </summary>
                public const string DefaultAccountLockoutSeconds = "Majid.Zero.UserManagement.UserLockOut.DefaultAccountLockoutSeconds";
            }

            public static class TwoFactorLogin
            {
                /// <summary>
                /// "Majid.Zero.UserManagement.TwoFactorLogin.IsEnabled".
                /// </summary>
                public const string IsEnabled = "Majid.Zero.UserManagement.TwoFactorLogin.IsEnabled";

                /// <summary>
                /// "Majid.Zero.UserManagement.TwoFactorLogin.IsEmailProviderEnabled".
                /// </summary>
                public const string IsEmailProviderEnabled = "Majid.Zero.UserManagement.TwoFactorLogin.IsEmailProviderEnabled";

                /// <summary>
                /// "Majid.Zero.UserManagement.TwoFactorLogin.IsSmsProviderEnabled".
                /// </summary>
                public const string IsSmsProviderEnabled = "Majid.Zero.UserManagement.TwoFactorLogin.IsSmsProviderEnabled";

                /// <summary>
                /// "Majid.Zero.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled".
                /// </summary>
                public const string IsRememberBrowserEnabled = "Majid.Zero.UserManagement.TwoFactorLogin.IsRememberBrowserEnabled";
            }

            public static class PasswordComplexity
            {
                /// <summary>
                /// "Majid.Zero.UserManagement.PasswordComplexity.RequiredLength"
                /// </summary>
                public const string RequiredLength = "Majid.Zero.UserManagement.PasswordComplexity.RequiredLength";

                /// <summary>
                /// "Majid.Zero.UserManagement.PasswordComplexity.RequireNonAlphanumeric"
                /// </summary>
                public const string RequireNonAlphanumeric = "Majid.Zero.UserManagement.PasswordComplexity.RequireNonAlphanumeric";

                /// <summary>
                /// "Majid.Zero.UserManagement.PasswordComplexity.RequireLowercase"
                /// </summary>
                public const string RequireLowercase = "Majid.Zero.UserManagement.PasswordComplexity.RequireLowercase";

                /// <summary>
                /// "Majid.Zero.UserManagement.PasswordComplexity.RequireUppercase"
                /// </summary>
                public const string RequireUppercase = "Majid.Zero.UserManagement.PasswordComplexity.RequireUppercase";

                /// <summary>
                /// "Majid.Zero.UserManagement.PasswordComplexity.RequireDigit"
                /// </summary>
                public const string RequireDigit = "Majid.Zero.UserManagement.PasswordComplexity.RequireDigit";
            }
        }

        public static class OrganizationUnits
        {
            /// <summary>
            /// "Majid.Zero.OrganizationUnits.MaxUserMembershipCount".
            /// </summary>
            public const string MaxUserMembershipCount = "Majid.Zero.OrganizationUnits.MaxUserMembershipCount";
        }
    }
}