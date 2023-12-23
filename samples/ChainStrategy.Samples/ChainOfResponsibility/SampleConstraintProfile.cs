// <copyright file="SampleConstraintProfile.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample constrained profile.
    /// </summary>
    internal class SampleConstraintProfile : ChainProfile<SampleConstrainedRequest>
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
