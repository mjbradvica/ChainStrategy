// <copyright file="TestStrategySubtractionHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test Strategy handler that implements subtraction.
    /// </summary>
    internal class TestStrategySubtractionHandler : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Handles a test request using subtraction.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A response object after the operation is complete.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TestStrategyResponse { Value = 10 - 5 });
        }
    }
}
