namespace ChainStrategy
{
    /// <summary>
    /// An interface for a factory that will initialize a chain of responsibility for the given request object.
    /// </summary>
    /// <typeparam name="TRequest">The request object for the chain being initialized.</typeparam>
    public interface IChainFactory<TRequest>
        where TRequest : IChainRequest
    {
        /// <summary>
        /// Creates a chain of responsibility for the given request.
        /// </summary>
        /// <returns>An <see cref="IChainHandler{TRequest}"/> that represents the first step in a chain.</returns>
        IChainHandler<TRequest> CreateChain();
    }
}
