// <copyright file="TestStrategySubtractionHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

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
        /// <returns>A response object after the operation is complete.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request)
        {
            return Task.FromResult(new TestStrategyResponse { Value = 10 - 5 });
        }
    }
}
