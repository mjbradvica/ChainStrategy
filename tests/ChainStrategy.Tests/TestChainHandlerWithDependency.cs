using System.Threading.Tasks;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test chain handler with an injected dependency.
    /// </summary>
    internal class TestChainHandlerWithDependency : ChainHandler<TestChainRequest>
    {
        private readonly TestChainDependency _dependency;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainHandlerWithDependency"/> class.
        /// </summary>
        /// <param name="handler">The next handler in the chain.</param>
        /// <param name="dependency">An injected dependency.</param>
        public TestChainHandlerWithDependency(IChainHandler<TestChainRequest>? handler, TestChainDependency dependency)
            : base(handler)
        {
            _dependency = dependency;
        }

        /// <summary>
        /// Does work and returns the request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>The same request object after the operation is complete.</returns>
        public override Task<TestChainRequest> DoWork(TestChainRequest request)
        {
            ++request.Value;

            return Task.FromResult(request);
        }
    }
}
