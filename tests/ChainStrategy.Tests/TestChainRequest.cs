﻿namespace ChainStrategy.Tests
{
    /// <summary>
    /// Internal class to allow testing of the <see cref="ChainRequest"/> class.
    /// </summary>
    internal class TestChainRequest : ChainRequest
    {
        /// <summary>
        /// Gets or sets the test value for the request.
        /// </summary>
        public int Value { get; set; }
    }
}
