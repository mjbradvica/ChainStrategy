// <copyright file="ChainPayloadTests.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Tests for the abstract <see cref="ChainPayload"/> class.
    /// </summary>
    [TestClass]
    public class ChainPayloadTests
    {
        /// <summary>
        /// Ensures the correct property values after the Faulted method is called.
        /// </summary>
        [TestMethod]
        public void Faulted_HasCorrectProperties()
        {
            var request = new TestChainPayload();

            request.Faulted();

            Assert.IsTrue(request.IsFaulted);
            Assert.IsNull(request.Exception);
        }

        /// <summary>
        /// Ensures the correct property values after the Faulted method is called with an exception.
        /// </summary>
        [TestMethod]
        public void Faulted_WithException_HasCorrectProperties()
        {
            var exception = new ArgumentNullException();

            var request = new TestChainPayload();

            request.Faulted(exception);

            Assert.IsTrue(request.IsFaulted);
            Assert.AreEqual(exception, request.Exception);
        }
    }
}
