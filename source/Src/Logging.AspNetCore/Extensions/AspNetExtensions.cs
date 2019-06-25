using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.AspNetCore
{
    /// <summary>
    /// Extension methods for ASP.NET Core
    /// </summary>
    public static class AspNetExtensions
    {
        /// <summary>
        /// Use Enterprise Library Logging for Dependency Injected loggers
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseEnterpriseLibraryLog(this IWebHostBuilder builder)
        {
            return UseEnterpriseLibraryLog(builder, () => null);
        }

        /// <summary>
        /// Use Enterprise Library Logging for Dependency Injected loggers
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="optionsFunc">Builder options function for logging</param>
        /// <returns></returns>
        public static IWebHostBuilder UseEnterpriseLibraryLog(this IWebHostBuilder builder, Func<LoggerOptions> optionsFunc)
        {
            return UseEnterpriseLibraryLog(builder, optionsFunc());
        }

        /// <summary>
        /// Use Enterprise Library Logging for Dependency Injected loggers
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options">Options for logging</param>
        /// <returns></returns>
        public static IWebHostBuilder UseEnterpriseLibraryLog(this IWebHostBuilder builder, LoggerOptions options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices(services => ConfigureEnterpriseLibraryLog(options, services, serviceProvider => serviceProvider.GetService<IConfiguration>()));
            return builder;
        }

        private static void ConfigureEnterpriseLibraryLog(LoggerOptions options, IServiceCollection services, Func<IServiceProvider, IConfiguration> lookupConfiguration)
        {
            services.AddSingleton<ILoggerProvider>(serviceProvider =>
            {
                LoggerProvider provider = new LoggerProvider(options ?? new LoggerOptions());
                IConfiguration configuration = lookupConfiguration(serviceProvider);
                return provider;
            });
        }
    }
}
