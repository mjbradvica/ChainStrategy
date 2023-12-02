// <copyright file="IStrategyHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy
{
    /// <summary>
    /// An interface to designate a strategy handler that will return a response.
    /// </summary>
    /// <typeparam name="TStrategyRequest">The request object for the strategy handler.</typeparam>
    /// <typeparam name="TStrategyResponse">The response object for the strategy handler.</typeparam>
    public interface IStrategyHandler<in TStrategyRequest, TStrategyResponse>
        where TStrategyRequest : IStrategyRequest<TStrategyResponse>
    {
        /// <summary>
        /// A method to be implemented by a concrete handler to perform logic on a request.
        /// </summary>
        /// <param name="request">The request to be processed by the handler.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that contains the response object.</returns>
        public Task<TStrategyResponse> Handle(TStrategyRequest request, CancellationToken cancellationToken);
    }

    /// <summary>
    /// An interface to designate a strategy handler that does not return a response.
    /// </summary>
    /// <typeparam name="TStrategyRequest">The request object for the strategy handler.</typeparam>
    public interface IStrategyHandler<in TStrategyRequest> : IStrategyHandler<TStrategyRequest, Nothing>
        where TStrategyRequest : IStrategyRequest<Nothing>
    {
    }
}
