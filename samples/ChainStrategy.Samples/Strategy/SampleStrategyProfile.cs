namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// A sample strategy profile that designates conditions for each handler.
    /// </summary>
    internal class SampleStrategyProfile : StrategyProfile<SampleStrategyRequest, SampleStrategyResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleStrategyProfile"/> class.
        /// </summary>
        public SampleStrategyProfile()
        {
            AddStrategy<SampleStrategyAddition>(x => x.InitialValue > 10);
            AddStrategy<SampleStrategyMultiplication>(x => x.InitialValue < 10);
            AddDefault<SampleStrategyAddition>();
        }
    }
}
