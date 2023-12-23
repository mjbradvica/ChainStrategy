// <copyright file="ChainHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy
{
    /// <summary>
    /// An abstract base class for all chain handlers to inherit from and implement their respective logic.
    /// </summary>
    /// <typeparam name="TRequest">The request object for the chain.</typeparam>
    public abstract class ChainHandler<TRequest> : IChainHandler<TRequest>
        where TRequest : IChainRequest
    {
        private readonly IChainHandler<TRequest>? _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainHandler{TRequest}"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain used to process the request.</param>
        protected ChainHandler(IChainHandler<TRequest>? handler)
        {
            _handler = handler;
        }

        /// <inheritdoc/>
        public virtual async Task<TRequest> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request.IsFaulted)
            {
                return request;
            }

            var result = await Middleware(request, cancellationToken);

            return _handler == null ? result : await _handler.Handle(request, cancellationToken);
        }

        /// <summary>
        /// Implementation method for each handler to perform its unique logic.
        /// </summary>
        /// <param name="request">The request for the chain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The resulting request object after a handler is finished.</returns>
        public abstract Task<TRequest> DoWork(TRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Allows a step to tap into lifecycle of a handler.
        /// </summary>
        /// <param name="request">The request for the chain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The resulting request after any lifecycle logic is required.</returns>
        public virtual async Task<TRequest> Middleware(TRequest request, CancellationToken cancellationToken)
        {
            return await DoWork(request, cancellationToken);
        }
    }
}
