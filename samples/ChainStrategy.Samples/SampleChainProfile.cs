namespace ChainStrategy.Samples
{
    /// <summary>
    /// An example chain profile.
    /// </summary>
    internal class SampleChainProfile : ChainProfile<SampleChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleChainProfile"/> class.
        /// </summary>
        public SampleChainProfile()
        {
            AddStep<SampleAdditionHandler>()
                .AddStep<SampleMultiplicationHandler>();
        }
    }
}
