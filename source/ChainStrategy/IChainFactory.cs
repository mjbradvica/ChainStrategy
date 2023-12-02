// <copyright file="IChainFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

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
        /// Executes a chain of responsibility given a request.
        /// </summary>
        /// <param name="request">The request object to be executed.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> of the request type representing the asynchronous operation.</returns>
        Task<TRequest> Execute(TRequest request, CancellationToken cancellationToken = default);
    }
}
