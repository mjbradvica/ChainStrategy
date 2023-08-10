﻿namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test profile for a chain with non-public handler constructors.
    /// </summary>
    internal class TestChainProfileWithBadHandler : ChainProfile<TestChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainProfileWithBadHandler"/> class.
        /// </summary>
        public TestChainProfileWithBadHandler()
        {
            AddStep<TestHandlerNonPublicConstructor>().AddStep<TestHandlerNonPublicConstructor>();
        }
    }
}
