namespace ChainStrategy.Samples
{
    /// <summary>
    /// Sample handler to show multiplication.
    /// </summary>
    internal class SampleMultiplicationHandler : ChainHandler<SampleChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleMultiplicationHandler"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        public SampleMultiplicationHandler(IChainHandler<SampleChainRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Multiplies the current request value.
        /// </summary>
        /// <param name="request">The request to be modified.</param>
        /// <returns>An updated request value.</returns>>
        public override Task<SampleChainRequest> DoWork(SampleChainRequest request)
        {
            request.Value *= 2;

            return Task.FromResult(request);
        }
    }
}
