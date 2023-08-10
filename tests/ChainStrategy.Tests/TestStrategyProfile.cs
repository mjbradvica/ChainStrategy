﻿namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test profile for Strategy unit tests.
    /// </summary>
    internal class TestStrategyProfile : StrategyProfile<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStrategyProfile"/> class.
        /// </summary>
        public TestStrategyProfile()
        {
            AddStrategy<TestStrategyAdditionHandler>(request => request.ShouldUseAddition);
            AddStrategy<TestStrategySubtractionHandler>(request => request.ShouldUseAddition == false);
            AddDefault<TestStrategyAdditionHandler>();
        }
    }
}
