namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test profile with only a default strategy.
    /// </summary>
    internal class DefaultOnlyStrategyProfile : StrategyProfile<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultOnlyStrategyProfile"/> class.
        /// </summary>
        public DefaultOnlyStrategyProfile()
        {
            AddStrategy<TestStrategySubtractionHandler>(x => x.StartValue == "useThis");
            AddDefault<TestStrategyAdditionHandler>();
        }
    }
}
