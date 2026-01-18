// <copyright file="StrategyProfileTests.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

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
        public void ProfileAddStrategiesCorrectly()
        {
            var profile = new TestStrategyProfile();

            Assert.HasCount(2, profile.Strategies);
        }

        /// <summary>
        /// Ensures that the Default function correctly adds the default strategy.
        /// </summary>
        [TestMethod]
        public void ProfileAddsDefaultStrategyCorrectly()
        {
            var profile = new TestStrategyProfile();

            Assert.AreEqual(typeof(TestStrategyAdditionHandler), profile.Default);
        }
    }
}
