// <copyright file="SampleChainProfile.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Standard
{
    /// <summary>
    /// An example chain profile.
    /// </summary>
    internal sealed class SampleChainProfile : ChainProfile<SampleChainPayload>
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
