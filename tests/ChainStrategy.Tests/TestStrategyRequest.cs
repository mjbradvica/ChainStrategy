namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test request for Strategy unit tests.
    /// </summary>
    internal class TestStrategyRequest : IStrategyRequest<TestStrategyResponse>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the strategy should use addition.
        /// </summary>
        public bool ShouldUseAddition { get; set; }
    }
}
