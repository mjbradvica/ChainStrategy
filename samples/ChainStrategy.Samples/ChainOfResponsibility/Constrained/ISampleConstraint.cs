// <copyright file="ISampleConstraint.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Constrained
{
    /// <summary>
    /// Sample interface for a constrained handler.
    /// </summary>
    public interface ISampleConstraint : IChainPayload
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Updates the identifier.
        /// </summary>
        /// <param name="id">The new identifier.</param>
        void UpdateId(Guid id);
    }
}
