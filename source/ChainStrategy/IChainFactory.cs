// <copyright file="IChainFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy
{
    /// <summary>
    /// An interface for a factory that will initialize a chain of responsibility for the given payload object.
    /// </summary>
    public interface IChainFactory
    {
        /// <summary>
        /// Executes a chain of responsibility given a payload.
        /// </summary>
        /// <typeparam name="TPayload">The payload object for the chain being initialized.</typeparam>
        /// <param name="payload">The payload object to be executed.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> of the payload type representing the asynchronous operation.</returns>
        Task<TPayload> Execute<TPayload>(TPayload payload, CancellationToken cancellationToken = default)
            where TPayload : IChainPayload;
    }
}
