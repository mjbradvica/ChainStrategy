// <copyright file="StrategyProfileBadConstructor.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test profile for a handler with a bad constructor.
    /// </summary>
    internal sealed class StrategyProfileBadConstructor : StrategyProfile<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyProfileBadConstructor"/> class.
        /// </summary>
        public StrategyProfileBadConstructor()
        {
            AddStrategy<TestStrategyHandlerBadConstructor>(x => x.ShouldUseAddition == false);
        }
    }
}
