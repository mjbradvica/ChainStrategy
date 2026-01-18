// <copyright file="TestChainProfileWithDependentHandlers.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test profile that contains handlers which require dependencies injected.
    /// </summary>
    internal sealed class TestChainProfileWithDependentHandlers : ChainProfile<TestChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainProfileWithDependentHandlers"/> class.
        /// </summary>
        public TestChainProfileWithDependentHandlers()
        {
            AddStep<TestChainHandlerWithDependency>().AddStep<TestChainHandlerWithDependency>();
        }
    }
}
