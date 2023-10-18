// <copyright file="IChainRequest.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// The basic interface for a request object in a chain of responsibility.
    /// </summary>
    public interface IChainRequest
    {
        /// <summary>
        /// Gets a value indicating whether the chain has faulted.
        /// </summary>
        bool IsFaulted { get; }
    }
}
