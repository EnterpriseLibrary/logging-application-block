// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.ContextBase;
using EnterpriseLibrary.Logging.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Logging.Formatters;

namespace EnterpriseLibrary.Logging.Tests.Configuration.Fluent
{
    public abstract class Given_BinaryFormatterBuilder : ArrangeActAssert
    {
        protected BinaryFormatterBuilder BinaryFormatterBuilder;
        protected string BinaryFormatterName = "Test Binary Formatter";

        protected override void Arrange()
        {
            BinaryFormatterBuilder = new FormatterBuilder().BinaryFormatterNamed(BinaryFormatterName);
        }

        protected BinaryLogFormatterData GetBinaryFormatterData()
        {
            return ((IFormatterBuilder)BinaryFormatterBuilder).GetFormatterData() as BinaryLogFormatterData;
        }
    }

    [TestClass]
    public class When_CreatingBinaryFormatterBuilder : Given_BinaryFormatterBuilder
    {
        [TestMethod]
        public void Then_BinaryFormatterDataHasSpecifiedName()
        {
            Assert.AreEqual(BinaryFormatterName, GetBinaryFormatterData().Name);
        }

        [TestMethod]
        public void Then_BinaryFormatterDataHasCorrectType()
        {
            Assert.AreEqual(typeof(BinaryLogFormatter), GetBinaryFormatterData().Type);
        }
    }

    [TestClass]
    public class When_CreatingBinaryFormatterPassingNullForName : ArrangeActAssert
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Then_BinaryFormatterNamed_ThrowsArgumentException()
        {
            new FormatterBuilder().BinaryFormatterNamed(null);
        }
    }
}
