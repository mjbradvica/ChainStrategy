// <copyright file="ChainHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// An abstract base class for all chain handlers to inherit from and implement their respective logic.
    /// </summary>
    /// <typeparam name="TPayload">The payload object for the chain.</typeparam>
    public abstract class ChainHandler<TPayload> : IChainHandler<TPayload>
        where TPayload : IChainPayload
    {
        private readonly IChainHandler<TPayload>? _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainHandler{TPayload}"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain used to process the payload.</param>
        protected ChainHandler(IChainHandler<TPayload>? handler)
        {
            _handler = handler;
        }

        /// <inheritdoc/>
        public virtual async Task<TPayload> Handle(TPayload payload, CancellationToken cancellationToken)
        {
            if (payload.IsFaulted)
            {
                return payload;
            }

            var result = await Middleware(payload, cancellationToken);

            return _handler == null ? result : await _handler.Handle(payload, cancellationToken);
        }

        /// <summary>
        /// Implementation method for each handler to perform its unique logic.
        /// </summary>
        /// <param name="payload">The payload for the chain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The resulting payload object after a handler is finished.</returns>
        public abstract Task<TPayload> DoWork(TPayload payload, CancellationToken cancellationToken);

        /// <summary>
        /// Allows a step to tap into lifecycle of a handler.
        /// </summary>
        /// <param name="payload">The payload for the chain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>The resulting payload after any lifecycle logic is required.</returns>
        public virtual async Task<TPayload> Middleware(TPayload payload, CancellationToken cancellationToken)
        {
            return await DoWork(payload, cancellationToken);
        }
    }
}
