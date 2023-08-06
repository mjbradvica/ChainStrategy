namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// Sample handler to show addition.
    /// </summary>
    internal class SampleAdditionHandler : ChainHandler<SampleChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleAdditionHandler"/> class.
        /// </summary>
        /// <param name="handler">The next sample handler.</param>
        public SampleAdditionHandler(IChainHandler<SampleChainRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Adds to the current request value.
        /// </summary>
        /// <param name="request">The current request object.</param>
        /// <returns>A modified request object.</returns>
        public override Task<SampleChainRequest> DoWork(SampleChainRequest request)
        {
            request.Value += 5;

            return Task.FromResult(request);
        }
    }
}
