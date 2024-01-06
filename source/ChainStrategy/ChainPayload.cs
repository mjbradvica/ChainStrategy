// <copyright file="ChainPayload.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;

namespace ChainStrategy
{
    /// <summary>
    /// An abstract chain payload that has basic properties and methods for a chain process.
    /// </summary>
    public abstract class ChainPayload : IChainPayload
    {
        /// <inheritdoc/>
        public bool IsFaulted { get; private set; }

        /// <summary>
        /// Gets the exception that may have occurred in the chain.
        /// </summary>
        public Exception? Exception { get; private set; }

        /// <summary>
        /// A method that designates the chain has faulted during its execution.
        /// </summary>
        public virtual void Faulted()
        {
            IsFaulted = true;
        }

        /// <summary>
        /// A method that designates the chain has faulted during its execution with an exception.
        /// </summary>
        /// <param name="exception">A <see cref="Exception"/> that occurred during the chain process.</param>
        public virtual void Faulted(Exception exception)
        {
            IsFaulted = true;
            Exception = exception;
        }
    }
}
