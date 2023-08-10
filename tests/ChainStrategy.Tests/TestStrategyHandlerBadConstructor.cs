using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test strategy handler with a bad constructor.
    /// </summary>
    internal class TestStrategyHandlerBadConstructor : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
    {
        private TestStrategyHandlerBadConstructor()
        {
        }

        /// <summary>
        /// Handle method for the bad handler.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request)
        {
            return Task.FromResult(new TestStrategyResponse());
        }
    }
}
