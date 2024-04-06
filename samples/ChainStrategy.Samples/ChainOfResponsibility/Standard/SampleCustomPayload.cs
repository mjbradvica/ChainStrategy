// <copyright file="SampleCustomPayload.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Standard
{
    /// <summary>
    /// Sample custom payload.
    /// </summary>
    internal abstract class SampleCustomPayload : ChainPayload
    {
        /// <summary>
        /// Gets the timestamp for the fault.
        /// </summary>
        public DateTime FaultedAt { get; private set; }

        /// <inheritdoc/>
        public override void Faulted(Exception exception)
        {
            FaultedAt = DateTime.UtcNow;
            base.Faulted(exception);
        }
    }
}
