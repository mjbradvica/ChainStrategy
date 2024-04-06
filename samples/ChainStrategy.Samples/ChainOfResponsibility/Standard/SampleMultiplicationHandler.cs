// <copyright file="SampleMultiplicationHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Standard
{
    /// <summary>
    /// Sample handler to show multiplication.
    /// </summary>
    internal class SampleMultiplicationHandler : ChainHandler<SampleChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleMultiplicationHandler"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        public SampleMultiplicationHandler(IChainHandler<SampleChainPayload>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Multiplies the current payload value.
        /// </summary>
        /// <param name="payload">The payload to be modified.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>An updated payload value.</returns>>
        public override Task<SampleChainPayload> DoWork(SampleChainPayload payload, CancellationToken cancellationToken)
        {
            payload.Value *= 2;

            return Task.FromResult(payload);
        }
    }
}
