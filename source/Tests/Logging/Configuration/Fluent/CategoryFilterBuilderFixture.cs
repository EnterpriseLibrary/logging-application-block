﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.Common.TestSupport.ContextBase;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Logging.Filters;

namespace EnterpriseLibrary.Logging.Tests.Configuration.Fluent
{
    public abstract class Given_CategoryFilterBuilder : Given_LoggingSettingsInConfigurationSourceBuilder
    {
        protected ILoggingConfigurationFilterOnCategory categoryFilterBuilder;

        protected override void Arrange()
        {
            base.Arrange();

            categoryFilterBuilder = base.ConfigureLogging.WithOptions.FilterOnCategory("cat filter");
        }

        protected CategoryFilterData GetCategoryFilterData()
        {
            return GetLoggingConfiguration().LogFilters.OfType<CategoryFilterData>().FirstOrDefault();
        }
    }

    [TestClass]
    public class When_CreatingCategoryFilterBuilder : Given_CategoryFilterBuilder
    {
        [TestMethod]
        public void Then_CategoryFilterDataHasAppropriateName()
        {
            Assert.AreEqual("cat filter", GetCategoryFilterData().Name);
        }

        [TestMethod]
        public void Then_CategoryFilterIsContainedInSettings()
        {
            Assert.IsTrue(GetLoggingConfiguration().LogFilters.OfType<CategoryFilterData>().Any());
        }


        [TestMethod]
        public void ThenCategoryFilterHasCorrectType()
        {
            Assert.AreEqual(typeof(CategoryFilter), GetCategoryFilterData().Type);
        }
    }

    [TestClass]
    public class When_CallingFilterOnCateogryPassingNullForFilerName : Given_CategoryFilterBuilder
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Then_FilterOnCategory_ThrowsArgumentException()
        {
            base.ConfigureLogging.WithOptions.FilterOnCategory(null);
        }
    }

    [TestClass]
    public class When_CallingAllowAllExceptOnCategoryFilterBuilder : Given_CategoryFilterBuilder
    {
        protected override void Act()
        {
            categoryFilterBuilder.AllowAllCategoriesExcept("cat1", "cat2");
        }

        [TestMethod]
        public void Then_CategoryFilterModeIsAllowAllExcept()
        {
            Assert.AreEqual(CategoryFilterMode.AllowAllExceptDenied, GetCategoryFilterData().CategoryFilterMode);
        }

        [TestMethod]
        public void Then_CategoryFilterContainsPassedCategories()
        {
            var categoryData = GetCategoryFilterData().CategoryFilters;
            Assert.IsTrue(categoryData.Any(x => x.Name == "cat1"));
            Assert.IsTrue(categoryData.Any(x => x.Name == "cat2"));
        }
    }

    [TestClass]
    public class When_CallingAllowAllExceptOnCategoryFilterBuilderPassingNullForCategories : Given_CategoryFilterBuilder
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Then_AllowAllCategoriesExcept_ThrowsArgumentNullException()
        {
            categoryFilterBuilder.AllowAllCategoriesExcept(null);
        }
    }

    [TestClass]
    public class When_CallingDenyAllExceptOnCategoryFilterBuilder : Given_CategoryFilterBuilder
    {
        protected override void Act()
        {
            categoryFilterBuilder.DenyAllCategoriesExcept("cat1", "cat2");
        }

        [TestMethod]
        public void Then_CategoryFilterModeIsDenyAllExcept()
        {
            Assert.AreEqual(CategoryFilterMode.DenyAllExceptAllowed, GetCategoryFilterData().CategoryFilterMode);
        }

        [TestMethod]
        public void Then_CategoryFilterContainsPassedCategories()
        {
            var categoryData = GetCategoryFilterData().CategoryFilters;
            Assert.IsTrue(categoryData.Any(x => x.Name == "cat1"));
            Assert.IsTrue(categoryData.Any(x => x.Name == "cat2"));
        }
    }

    [TestClass]
    public class When_CallingDenyAllExceptOnCategoryFilterBuilderPassingNullForCategories : Given_CategoryFilterBuilder
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Then_AllowAllCategoriesExcept_ThrowsArgumentNullException()
        {
            categoryFilterBuilder.DenyAllCategoriesExcept(null);
        }
    }
}
