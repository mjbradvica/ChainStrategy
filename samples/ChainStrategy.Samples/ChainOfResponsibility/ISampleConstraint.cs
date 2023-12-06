// <copyright file="ISampleConstraint.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample interface for a constrained handler.
    /// </summary>
    public interface ISampleConstraint
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
