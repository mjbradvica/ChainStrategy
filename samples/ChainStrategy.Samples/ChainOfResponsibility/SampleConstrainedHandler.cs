// <copyright file="SampleConstrainedHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample defined constrained handler.
    /// </summary>
    public class SampleConstrainedHandler : SampleBaseConstrainedHandler<SampleConstrainedRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleConstrainedHandler"/> class.
        /// </summary>
        /// <param name="handler">The successor handler.</param>
        public SampleConstrainedHandler(IChainHandler<SampleConstrainedRequest>? handler)
            : base(handler)
        {
        }
    }
}
