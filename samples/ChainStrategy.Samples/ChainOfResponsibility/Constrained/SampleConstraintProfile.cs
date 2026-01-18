// <copyright file="SampleConstraintProfile.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Constrained
{
    /// <summary>
    /// Sample constrained profile.
    /// </summary>
    internal sealed class SampleConstraintProfile : ChainProfile<SampleConstrainedPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleConstraintProfile"/> class.
        /// </summary>
        public SampleConstraintProfile()
        {
            AddStep<SampleConstrainedHandler>();
        }
    }
}
