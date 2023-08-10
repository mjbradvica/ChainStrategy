namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test profile that contains handlers which require dependencies injected.
    /// </summary>
    internal class TestChainProfileWithDependentHandlers : ChainProfile<TestChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainProfileWithDependentHandlers"/> class.
        /// </summary>
        public TestChainProfileWithDependentHandlers()
        {
            AddStep<TestChainHandlerWithDependency>().AddStep<TestChainHandlerWithDependency>();
        }
    }
}
