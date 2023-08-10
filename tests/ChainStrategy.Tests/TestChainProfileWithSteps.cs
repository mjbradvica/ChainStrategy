namespace ChainStrategy.Tests
{
    /// <summary>
    /// A chain profile with steps for unit testing.
    /// </summary>
    internal class TestChainProfileWithSteps : ChainProfile<TestChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainProfileWithSteps"/> class.
        /// </summary>
        public TestChainProfileWithSteps()
        {
            AddStep<TestChainHandler>().AddStep<TestChainHandler>();
        }
    }
}
