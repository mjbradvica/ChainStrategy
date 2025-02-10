// <copyright file="IChainPayload.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// The basic interface for a payload object in a chain of responsibility.
    /// </summary>
    public interface IChainPayload
    {
        /// <summary>
        /// Gets a value indicating whether the chain has faulted.
        /// </summary>
        bool IsFaulted { get; }
    }
}
