// <copyright file="TestHandlerNonPublicConstructor.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A handler for unit testing with no public constructor.
    /// </summary>
    internal sealed class TestHandlerNonPublicConstructor : IChainHandler<TestChainPayload>
    {
        private TestHandlerNonPublicConstructor()
        {
        }

        /// <summary>
        /// Performs an operation on a payload object.
        /// </summary>
        /// <param name="payload">The payload object for the method.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The same payload object post-operation.</returns>
        public Task<TestChainPayload> Handle(TestChainPayload payload, CancellationToken cancellationToken)
        {
            ++payload.Value;

            return Task.FromResult(payload);
        }
    }
}
