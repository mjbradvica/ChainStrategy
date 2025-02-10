// <copyright file="StrategyProfileWithDependentHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test strategy profile.
    /// </summary>
    internal class StrategyProfileWithDependentHandler : StrategyProfile<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyProfileWithDependentHandler"/> class.
        /// </summary>
        public StrategyProfileWithDependentHandler()
        {
            AddDefault<TestStrategyWithDependency>();
        }
    }
}
