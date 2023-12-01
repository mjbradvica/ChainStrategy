// <copyright file="IChainHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy
{
    /// <summary>
    /// An interface that designates a handler for chain operation.
    /// </summary>
    /// <typeparam name="TRequest">The request object being passed into the chain.</typeparam>
    public interface IChainHandler<TRequest>
        where TRequest : IChainRequest
    {
        /// <summary>
        /// The method each handler will implement that represents a step in the chain.
        /// </summary>
        /// <param name="request">The request object being passed into the chain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end an operation.</param>
        /// <returns>The request object after it has been processed by the handler.</returns>
        Task<TRequest> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
