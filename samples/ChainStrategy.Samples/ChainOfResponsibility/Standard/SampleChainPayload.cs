// <copyright file="SampleChainPayload.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Standard
{
    /// <summary>
    /// A sample chain payload to highlight the library.
    /// </summary>
    internal class SampleChainPayload : ChainPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleChainPayload"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value of the payload.</param>
        public SampleChainPayload(int initialValue = 1)
        {
            Value = initialValue;
        }

        /// <summary>
        /// Gets or sets the value being modified.
        /// </summary>
        public int Value { get; set; }
    }
}
