namespace ChainStrategy.Samples
{
    /// <summary>
    /// A class that represents a sample strategy request.
    /// </summary>
    internal class SampleStrategyRequest : IStrategyRequest<SampleStrategyResponse>
    {
        /// <summary>
        /// Gets or sets the initial value for the sample request.
        /// </summary>
        public int InitialValue { get; set; } = 9;
    }
}
