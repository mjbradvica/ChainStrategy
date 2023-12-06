// <copyright file="SampleConstrainedHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample constrained handler.
    /// </summary>
    public class SampleConstrainedHandler : ChainHandler<SampleConstrainedRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleConstrainedHandler"/> class.
        /// </summary>
        /// <param name="handler">The successor handler.</param>
        public SampleConstrainedHandler(IChainHandler<SampleConstrainedRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Handles a constrained request.
        /// </summary>
        /// <param name="request">Chain request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task<SampleConstrainedRequest> DoWork(SampleConstrainedRequest request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
            }

            request.UpdateId(id);

            return Task.FromResult(request);
        }
    }
}
