namespace Majid.AspNetCore
{
    public class MajidApplicationBuilderOptions
    {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseCastleLoggerFactory { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseMajidRequestLocalization { get; set; }

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool UseSecurityHeaders { get; set; }

        public MajidApplicationBuilderOptions()
        {
            UseCastleLoggerFactory = true;
            UseMajidRequestLocalization = true;
            UseSecurityHeaders = true;
        }
    }
}