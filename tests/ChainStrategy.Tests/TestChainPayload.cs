// <copyright file="TestChainPayload.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Internal class to allow testing of the <see cref="ChainPayload"/> class.
    /// </summary>
    internal class TestChainPayload : ChainPayload
    {
        /// <summary>
        /// Gets or sets the test value for the request.
        /// </summary>
        public int Value { get; set; }
    }
}
