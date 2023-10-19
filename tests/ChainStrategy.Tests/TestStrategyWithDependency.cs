// <copyright file="TestStrategyWithDependency.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test Strategy with a dependency.
    /// </summary>
    internal class TestStrategyWithDependency : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
    {
        private readonly TestChainDependency _dependency;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStrategyWithDependency"/> class.
        /// </summary>
        /// <param name="dependency">A strategy handler dependency.</param>
        public TestStrategyWithDependency(TestChainDependency dependency)
        {
            _dependency = dependency;
        }

        /// <summary>
        /// Handles a test request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request)
        {
            return Task.FromResult(new TestStrategyResponse { Value = 10 - 5 });
        }
    }
}
