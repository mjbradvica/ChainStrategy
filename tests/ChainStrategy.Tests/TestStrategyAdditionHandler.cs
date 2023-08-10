using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test handler for Strategy unit tests that implements addition.
    /// </summary>
    internal class TestStrategyAdditionHandler : IStrategyHandler<TestStrategyRequest, TestStrategyResponse>
    {
        /// <summary>
        /// Handles the request using addition.
        /// </summary>
        /// <param name="request">The Strategy request object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<TestStrategyResponse> Handle(TestStrategyRequest request)
        {
            return Task.FromResult(new TestStrategyResponse { Value = 10 + 10 });
        }
    }
}
