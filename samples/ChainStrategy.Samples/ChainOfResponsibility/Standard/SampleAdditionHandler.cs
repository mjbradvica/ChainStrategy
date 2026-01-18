// <copyright file="SampleAdditionHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Standard
{
    /// <summary>
    /// Sample handler to show addition.
    /// </summary>
    internal sealed class SampleAdditionHandler : ChainHandler<SampleChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleAdditionHandler"/> class.
        /// </summary>
        /// <param name="handler">The next sample handler.</param>
        public SampleAdditionHandler(IChainHandler<SampleChainPayload>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Adds to the current payload value.
        /// </summary>
        /// <param name="payload">The current payload object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A modified payload object.</returns>
        protected override Task<SampleChainPayload> DoWork(SampleChainPayload payload, CancellationToken cancellationToken)
        {
            payload.Value += 5;

            return Task.FromResult(payload);
        }
    }
}
