// <copyright file="SampleChainProfile.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// An example chain profile.
    /// </summary>
    internal class SampleChainProfile : ChainProfile<SampleChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleChainProfile"/> class.
        /// </summary>
        public SampleChainProfile()
        {
            AddStep<SampleAdditionHandler>()
                .AddStep<SampleMultiplicationHandler>();
        }
    }
}
