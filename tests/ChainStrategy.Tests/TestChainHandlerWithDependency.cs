// <copyright file="TestChainHandlerWithDependency.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test chain handler with an injected dependency.
    /// </summary>
    internal sealed class TestChainHandlerWithDependency : ChainHandler<TestChainPayload>
    {
        private readonly TestChainDependency _dependency;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainHandlerWithDependency"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        /// <param name="dependency">An injected dependency.</param>
        public TestChainHandlerWithDependency(IChainHandler<TestChainPayload>? handler, TestChainDependency dependency)
            : base(handler)
        {
            _dependency = dependency;
        }

        /// <summary>
        /// Does work and returns the payload.
        /// </summary>
        /// <param name="payload">The payload object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The same payload object after the operation is complete.</returns>
        public override Task<TestChainPayload> DoWork(TestChainPayload payload, CancellationToken cancellationToken)
        {
            ++payload.Value;

            return Task.FromResult(payload);
        }
    }
}
