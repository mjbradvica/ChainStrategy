﻿// <copyright file="ChainProfileTests.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Tests the <see cref="ChainProfile{TRequest}"/> class capabilities.
    /// </summary>
    [TestClass]
    public class ChainProfileTests
    {
        /// <summary>
        /// Ensures that method correctly adds to the profile.
        /// </summary>
        [TestMethod]
        public void AddStep_CorrectlyAddsToProfile()
        {
            var profile = new TestChainProfileWithSteps();

            Assert.AreEqual(2, profile.ChainRegistrations.Count);
        }
    }
}
