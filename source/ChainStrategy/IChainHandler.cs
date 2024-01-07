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
    /// <typeparam name="TPayload">The payload object being passed into the chain.</typeparam>
    public interface IChainHandler<TPayload>
        where TPayload : IChainPayload
    {
        /// <summary>
        /// The method each handler will implement that represents a step in the chain.
        /// </summary>
        /// <param name="payload">The payload object being passed into the chain.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end an operation.</param>
        /// <returns>The payload object after it has been processed by the handler.</returns>
        Task<TPayload> Handle(TPayload payload, CancellationToken cancellationToken);
    }
}
