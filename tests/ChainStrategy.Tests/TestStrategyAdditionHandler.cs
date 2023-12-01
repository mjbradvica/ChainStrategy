// <copyright file="TestStrategyAdditionHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test handler for Strategy unit tests that implements addition.
    /// </summary>
    internal class TestStrategyAdditionHandler : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Handles the request using addition.
        /// </summary>
        /// <param name="request">The Strategy request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TestStrategyResponse { Value = 10 + 10 });
        }
    }
}
