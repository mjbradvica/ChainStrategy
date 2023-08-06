namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// A sample chain request to highlight the library.
    /// </summary>
    internal class SampleChainRequest : ChainRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleChainRequest"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value of the request.</param>
        public SampleChainRequest(int initialValue = 1)
        {
            Value = initialValue;
        }

        /// <summary>
        /// Gets or sets the value being modified.
        /// </summary>
        public int Value { get; set; }
    }
}
