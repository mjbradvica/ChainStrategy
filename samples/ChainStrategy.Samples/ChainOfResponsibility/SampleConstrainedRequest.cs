// <copyright file="SampleConstrainedRequest.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample constrained request object.
    /// </summary>
    public class SampleConstrainedRequest : ChainRequest, ISampleConstraint
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
