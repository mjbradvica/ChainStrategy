// <copyright file="TestChainProfileWithBadHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test profile for a chain with non-public handler constructors.
    /// </summary>
    internal sealed class TestChainProfileWithBadHandler : ChainProfile<TestChainPayload>
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
