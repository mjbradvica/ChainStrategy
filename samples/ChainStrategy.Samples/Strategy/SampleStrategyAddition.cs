namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// A sample strategy handler that will perform addition.
    /// </summary>
    internal class SampleStrategyAddition : IStrategyHandler<SampleStrategyRequest, SampleStrategyResponse>
    {
        /// <summary>
        /// Performs an addition operation.
        /// </summary>
        /// <param name="request">Our request object being executed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<SampleStrategyResponse> Handle(SampleStrategyRequest request)
        {
            return Task.FromResult(new SampleStrategyResponse { Value = request.InitialValue + 10 });
        }
    }
}
