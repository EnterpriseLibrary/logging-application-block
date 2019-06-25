namespace Microsoft.Practices.EnterpriseLibrary.Logging.AspNetCore
{
    /// <summary>
    /// Options for logging
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LoggerOptions()
        {
            
        }

        /// <summary>
        /// Get or set custom configuration filepath. If empty default application config file is used.
        /// </summary>
        public string ConfigurationFilepath { get; set; }
    }
}
