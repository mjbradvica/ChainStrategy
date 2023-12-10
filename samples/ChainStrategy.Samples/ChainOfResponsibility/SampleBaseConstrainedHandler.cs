// <copyright file="SampleBaseConstrainedHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample constrained handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    public abstract class SampleBaseConstrainedHandler<TRequest> : ChainHandler<TRequest>
        where TRequest : ISampleConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBaseConstrainedHandler{TRequest}"/> class.
        /// </summary>
        /// <param name="handler">The successor handler.</param>
        protected SampleBaseConstrainedHandler(IChainHandler<TRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Handles a constrained request.
        /// </summary>
        /// <param name="request">Chain request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task<TRequest> DoWork(TRequest request, CancellationToken cancellationToken)
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
