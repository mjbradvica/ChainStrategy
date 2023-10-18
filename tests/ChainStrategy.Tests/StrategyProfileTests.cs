// <copyright file="StrategyProfileTests.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test the <see cref="StrategyProfile{TStrategyRequest,TStrategyResponse}"/> class capabilities.
    /// </summary>
    [TestClass]
    public class StrategyProfileTests
    {
        /// <summary>
        /// Ensures that the AddStrategy function correctly adds strategies.
        /// </summary>
        [TestMethod]
        public void Profile_AddStrategiesCorrectly()
        {
            var profile = new TestStrategyProfile();

            Assert.AreEqual(2, profile.Strategies.Count);
        }

        /// <summary>
        /// Ensures that the Default function correctly adds the default strategy.
        /// </summary>
        [TestMethod]
        public void Profile_AddsDefaultStrategyCorrectly()
        {
            var profile = new TestStrategyProfile();

            Assert.AreEqual(typeof(TestStrategyAdditionHandler), profile.Default);
        }
    }
}
