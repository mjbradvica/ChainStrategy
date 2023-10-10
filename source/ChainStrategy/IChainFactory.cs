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
        /// Gets the first handler in the chain.
        /// </summary>
        IChainHandler<TRequest> Handler { get; }
    }
}
