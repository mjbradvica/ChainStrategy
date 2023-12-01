// <copyright file="TestChainHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test class for the abstract <see cref="ChainHandler{TRequest}"/>.
    /// </summary>
    internal class TestChainHandler : ChainHandler<TestChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainHandler"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        public TestChainHandler(IChainHandler<TestChainRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Implementation of the DoWork method for testing.
        /// </summary>
        /// <param name="request">The test chain request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The request after the step has processed.</returns>
        public override Task<TestChainRequest> DoWork(TestChainRequest request, CancellationToken cancellationToken)
        {
            ++request.Value;

            return Task.FromResult(request);
        }
    }
}
