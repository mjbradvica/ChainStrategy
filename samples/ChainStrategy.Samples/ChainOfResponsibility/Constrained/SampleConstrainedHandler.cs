// <copyright file="SampleConstrainedHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Constrained
{
    /// <summary>
    /// Sample defined constrained handler.
    /// </summary>
    public class SampleConstrainedHandler : SampleBaseConstrainedHandler<SampleConstrainedPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleConstrainedHandler"/> class.
        /// </summary>
        /// <param name="handler">The successor handler.</param>
        public SampleConstrainedHandler(IChainHandler<SampleConstrainedPayload>? handler)
            : base(handler)
        {
        }
    }
}
