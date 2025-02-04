// <copyright file="TestStrategyHandlerBadConstructor.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test strategy handler with a bad constructor.
    /// </summary>
    internal class TestStrategyHandlerBadConstructor : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
    {
        private TestStrategyHandlerBadConstructor()
        {
        }

        /// <summary>
        /// Handle method for the bad handler.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TestStrategyResponse());
        }
    }
}
