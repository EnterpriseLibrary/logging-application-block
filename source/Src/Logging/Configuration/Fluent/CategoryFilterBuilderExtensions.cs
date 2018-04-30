﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Filters;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Logging;
using EnterpriseLibrary.Common.Properties;

namespace EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// <see cref="ILoggingConfigurationOptions"/> extensions to configure <see cref="CategoryFilter"/> instances.
    /// </summary>
    /// <seealso cref="CategoryFilter"/>
    /// <seealso cref="CategoryFilterData"/>
    public static class CategoryFilterBuilderExtensions
    {
        /// <summary>
        /// Adds an <see cref="CategoryFilter"/> instance to the logging configuration.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="categoryFilterName">Name of the <see cref="CategoryFilter"/> instance added to configuration.</param>
        /// <seealso cref="CategoryFilter"/>
        /// <seealso cref="CategoryFilterData"/>
        public static ILoggingConfigurationFilterOnCategory FilterOnCategory(this ILoggingConfigurationOptions context, string categoryFilterName)
        {
            if (categoryFilterName == null)
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "categoryFilterName");

            return new FilterOnCategoryBuilder(context, categoryFilterName);
        }

        private class FilterOnCategoryBuilder : LoggingConfigurationExtension, ILoggingConfigurationFilterOnCategory
        {
            CategoryFilterData categoryFilter;

            public FilterOnCategoryBuilder(ILoggingConfigurationOptions context, string logFilterName)
                :base(context)
            {
                categoryFilter = new CategoryFilterData()
                {
                    Name = logFilterName
                };

                LoggingSettings.LogFilters.Add(categoryFilter);
            }

            public ILoggingConfigurationOptions DenyAllCategoriesExcept(params string[] categories)
            {
                if (categories == null)
                    throw new ArgumentNullException("categories");

                categoryFilter.CategoryFilterMode = CategoryFilterMode.DenyAllExceptAllowed;
                AddCategoriesToFilter(categories);

                return base.LoggingOptions;
            }

            public ILoggingConfigurationOptions AllowAllCategoriesExcept(params string[] categories)
            {
                if (categories == null)
                    throw new ArgumentNullException("categories");

                categoryFilter.CategoryFilterMode = CategoryFilterMode.AllowAllExceptDenied;
                AddCategoriesToFilter(categories);

                return base.LoggingOptions;
            }

            private void AddCategoriesToFilter(string[] categories)
            {
                categoryFilter.CategoryFilters.Clear();
                foreach (string category in categories)
                {
                    categoryFilter.CategoryFilters.Add(new CategoryFilterEntry(category));
                }
            }
        }
    }
}
