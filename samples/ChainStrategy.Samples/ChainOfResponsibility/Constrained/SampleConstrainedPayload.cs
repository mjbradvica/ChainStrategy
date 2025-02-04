// <copyright file="SampleConstrainedPayload.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Constrained
{
    /// <summary>
    /// Sample constrained request object.
    /// </summary>
    public class SampleConstrainedPayload : ChainPayload, ISampleConstraint
    {
        /// <inheritdoc/>
        public Guid Id { get; private set; }

        /// <inheritdoc/>
        public void UpdateId(Guid id)
        {
            Id = id;
        }
    }
}
