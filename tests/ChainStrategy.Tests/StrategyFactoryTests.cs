using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test the <see cref="StrategyFactory{TStrategyRequest,TStrategyResponse}"/> class capabilities.
    /// </summary>
    [TestClass]
    public class StrategyFactoryTests
    {
        private readonly IServiceCollection _serviceCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyFactoryTests"/> class.
        /// </summary>
        public StrategyFactoryTests()
        {
            _serviceCollection = new ServiceCollection();
        }

        /// <summary>
        /// Ensures an exception is thrown with no matching keys can be found.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_NoMatchingKeys_ThrowsException()
        {
            _serviceCollection
                .AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, EmptyChainStrategyProfile>();

            var factory = new StrategyFactory<TestStrategyRequest, TestStrategyResponse>(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await factory.ExecuteStrategy(new TestStrategyRequest()));
        }

        /// <summary>
        /// Ensures an exception is thrown when no profiles can be found.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_NoProfile_ThrowsException()
        {
            var factory = new StrategyFactory<TestStrategyRequest, TestStrategyResponse>(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await factory.ExecuteStrategy(new TestStrategyRequest()));
        }

        /// <summary>
        /// Ensures a strategy executes correctly given a profile and matching handlers.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_MatchingHandler_ExecutesCorrectly()
        {
            _serviceCollection
                .AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, TestStrategyProfile>();

            var factory = new StrategyFactory<TestStrategyRequest, TestStrategyResponse>(_serviceCollection.BuildServiceProvider());

            var result = await factory.ExecuteStrategy(new TestStrategyRequest());

            Assert.AreEqual(5, result.Value);
        }

        /// <summary>
        /// Ensures the default handler is used when no keys match.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_DefaultHandlerHit_ExecutesCorrectly()
        {
            _serviceCollection
                .AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, DefaultOnlyStrategyProfile>();

            var factory = new StrategyFactory<TestStrategyRequest, TestStrategyResponse>(_serviceCollection.BuildServiceProvider());

            var result = await factory.ExecuteStrategy(new TestStrategyRequest());

            Assert.AreEqual(20, result.Value);
        }

        /// <summary>
        /// Ensures an exception is thrown for handlers with private constructors.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_HandlerWithNoPublicMethod_ThrowsException()
        {
            _serviceCollection
                .AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, StrategyProfileBadConstructor>();

            var factory = new StrategyFactory<TestStrategyRequest, TestStrategyResponse>(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await factory.ExecuteStrategy(new TestStrategyRequest()));
        }
    }
}
