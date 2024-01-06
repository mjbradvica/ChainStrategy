// <copyright file="TestChainProfileWithSteps.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A chain profile with steps for unit testing.
    /// </summary>
    internal class TestChainProfileWithSteps : ChainProfile<TestChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainProfileWithSteps"/> class.
        /// </summary>
        public TestChainProfileWithSteps()
        {
            AddStep<TestChainHandler>().AddStep<TestChainHandler>();
        }
    }
}
