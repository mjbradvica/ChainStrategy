// <copyright file="SampleAdditionHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample handler to show addition.
    /// </summary>
    internal class SampleAdditionHandler : ChainHandler<SampleChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleAdditionHandler"/> class.
        /// </summary>
        /// <param name="handler">The next sample handler.</param>
        public SampleAdditionHandler(IChainHandler<SampleChainRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Adds to the current request value.
        /// </summary>
        /// <param name="request">The current request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A modified request object.</returns>
        public override Task<SampleChainRequest> DoWork(SampleChainRequest request, CancellationToken cancellationToken)
        {
            request.Value += 5;

            return Task.FromResult(request);
        }
    }
}
