namespace Majid.Net.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// Majid.Net.Mail.DefaultFromAddress
        /// </summary>
        public const string DefaultFromAddress = "Majid.Net.Mail.DefaultFromAddress";

        /// <summary>
        /// Majid.Net.Mail.DefaultFromDisplayName
        /// </summary>
        public const string DefaultFromDisplayName = "Majid.Net.Mail.DefaultFromDisplayName";

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
            /// <summary>
            /// Majid.Net.Mail.Smtp.Host
            /// </summary>
            public const string Host = "Majid.Net.Mail.Smtp.Host";

            /// <summary>
            /// Majid.Net.Mail.Smtp.Port
            /// </summary>
            public const string Port = "Majid.Net.Mail.Smtp.Port";

            /// <summary>
            /// Majid.Net.Mail.Smtp.UserName
            /// </summary>
            public const string UserName = "Majid.Net.Mail.Smtp.UserName";

            /// <summary>
            /// Majid.Net.Mail.Smtp.Password
            /// </summary>
            public const string Password = "Majid.Net.Mail.Smtp.Password";

            /// <summary>
            /// Majid.Net.Mail.Smtp.Domain
            /// </summary>
            public const string Domain = "Majid.Net.Mail.Smtp.Domain";

            /// <summary>
            /// Majid.Net.Mail.Smtp.EnableSsl
            /// </summary>
            public const string EnableSsl = "Majid.Net.Mail.Smtp.EnableSsl";

            /// <summary>
            /// Majid.Net.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public const string UseDefaultCredentials = "Majid.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}