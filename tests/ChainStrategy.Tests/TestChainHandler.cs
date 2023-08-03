using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test class for the abstract <see cref="ChainHandler{TRequest}"/>.
    /// </summary>
    internal class TestChainHandler : ChainHandler<TestChainRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainHandler"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        public TestChainHandler(IChainHandler<TestChainRequest>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Implementation of the DoWork method for testing.
        /// </summary>
        /// <param name="request">The test chain request.</param>
        /// <returns>The request after the step has processed.</returns>
        public override Task<TestChainRequest> DoWork(TestChainRequest request)
        {
            ++request.Value;

            return Task.FromResult(request);
        }
    }
}
