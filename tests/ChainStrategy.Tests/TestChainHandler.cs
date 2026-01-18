// <copyright file="TestChainHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test class for the abstract <see cref="ChainHandler{TRequest}"/>.
    /// </summary>
    internal sealed class TestChainHandler : ChainHandler<TestChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainHandler"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        public TestChainHandler(IChainHandler<TestChainPayload>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Implementation of the DoWork method for testing.
        /// </summary>
        /// <param name="payload">The test chain payload.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The payload after the step has processed.</returns>
        public override Task<TestChainPayload> DoWork(TestChainPayload payload, CancellationToken cancellationToken)
        {
            ++payload.Value;

            return Task.FromResult(payload);
        }
    }
}
