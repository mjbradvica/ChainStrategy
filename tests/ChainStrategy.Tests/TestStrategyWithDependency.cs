// <copyright file="TestStrategyWithDependency.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test Strategy with a dependency.
    /// </summary>
    internal sealed class TestStrategyWithDependency : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
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
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new TestStrategyResponse { Value = 10 - 5 });
        }
    }
}
