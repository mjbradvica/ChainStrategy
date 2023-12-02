// <copyright file="SampleMultiplicationHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample handler to show multiplication.
    /// </summary>
    internal class SampleMultiplicationHandler : ChainHandler<SampleChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleMultiplicationHandler"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        public SampleMultiplicationHandler(IChainHandler<SampleChainRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Multiplies the current request value.
        /// </summary>
        /// <param name="request">The request to be modified.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>An updated request value.</returns>>
        public override Task<SampleChainRequest> DoWork(SampleChainRequest request, CancellationToken cancellationToken)
        {
            request.Value *= 2;

            return Task.FromResult(request);
        }
    }
}
