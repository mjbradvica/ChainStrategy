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
        public virtual async Task<TRequest> Handle(TRequest request)
        {
            if (request.IsFaulted)
            {
                return request;
            }

            var result = await Middleware(request);

            return _handler == null ? result : await _handler.Handle(request);
        }

        /// <summary>
        /// Implementation method for each handler to perform its' unique logic.
        /// </summary>
        /// <param name="request">The request for the chain.</param>
        /// <returns>The resulting request object after a handler is finished.</returns>
        public abstract Task<TRequest> DoWork(TRequest request);

        /// <summary>
        /// Allows a step to tap into lifecycle of a handler.
        /// </summary>
        /// <param name="request">The request for the chain.</param>
        /// <returns>The resulting request after any lifecycle logic is required.</returns>
        public virtual async Task<TRequest> Middleware(TRequest request)
        {
            return await DoWork(request);
        }
    }
}
