// <copyright file="IStrategyFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy
{
    /// <summary>
    /// An interface that represents a factory for the strategy operation.
    /// </summary>
    public interface IStrategyFactory
    {
        /// <summary>
        /// Executes the strategy given the request object.
        /// </summary>
        /// <typeparam name="TStrategyResponse">The response type for the strategy operation.</typeparam>
        /// <param name="request">The request to be executed by the strategy.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that contains the response object.</returns>
        Task<TStrategyResponse> Execute<TStrategyResponse>(IStrategyRequest<TStrategyResponse> request, CancellationToken cancellationToken = default);
    }
}
