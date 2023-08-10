using System.Threading.Tasks;

namespace ChainStrategy
{
    /// <summary>
    /// An interface that represents a factory for the strategy operation.
    /// </summary>
    /// <typeparam name="TStrategyRequest">The request type for the strategy operation.</typeparam>
    /// <typeparam name="TStrategyResponse">The response type for the strategy operation.</typeparam>
    public interface IStrategyFactory<in TStrategyRequest, TStrategyResponse>
        where TStrategyRequest : IStrategyRequest<TStrategyResponse>
    {
        /// <summary>
        /// Executes the strategy given the request object.
        /// </summary>
        /// <param name="request">The request to be executed by the strategy.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that contains the response object.</returns>
        Task<TStrategyResponse> ExecuteStrategy(TStrategyRequest request);
    }
}
