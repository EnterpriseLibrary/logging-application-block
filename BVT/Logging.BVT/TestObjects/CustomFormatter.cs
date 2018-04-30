using System.Collections.Specialized;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;

namespace EnterpriseLibrary.Logging.BVT.TestObjects
{
    [ConfigurationElementType(typeof(CustomFormatterData))]
    public class CustomFormatter : ILogFormatter
    {
        public CustomFormatter() { }

        public CustomFormatter(NameValueCollection collection)
        { }

        public bool FormattedInvoked = false;

        public string Format(LogEntry log)
        {
            FormattedInvoked = true;
            return "Formatted text.";
        }
    }
}