// <copyright file="TestHandlerNonPublicConstructor.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A handler for unit testing with no public constructor.
    /// </summary>
    internal class TestHandlerNonPublicConstructor : IChainHandler<TestChainRequest>
    {
        private TestHandlerNonPublicConstructor()
        {
        }

        /// <summary>
        /// Performs an operation on a request object.
        /// </summary>
        /// <param name="request">The request object for the method.</param>
        /// <returns>The same request object post-operation.</returns>
        public Task<TestChainRequest> Handle(TestChainRequest request)
        {
            ++request.Value;

            return Task.FromResult(request);
        }
    }
}
