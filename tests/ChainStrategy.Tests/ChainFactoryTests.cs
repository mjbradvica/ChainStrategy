using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Tests the <see cref="ChainFactory{TRequest}"/> class capabilities.
    /// </summary>
    [TestClass]
    public class ChainFactoryTests
    {
        private readonly IServiceCollection _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainFactoryTests"/> class.
        /// </summary>
        public ChainFactoryTests()
        {
            _collection = new ServiceCollection();
        }

        /// <summary>
        /// Factory will thrown an exception when no profiles are found for a given request.
        /// </summary>
        [TestMethod]
        public void Handler_NullProfile_ThrowsException()
        {
            Assert.ThrowsException<NullReferenceException>(() => new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider()));
        }

        /// <summary>
        /// Factory will throw an exception when no registrations are present in a profile.
        /// </summary>
        [TestMethod]
        public void Handler_NoRegistrations_ThrowsException()
        {
            _collection.AddTransient<ChainProfile<TestChainRequest>, TestChainProfile>();

            Assert.ThrowsException<ArgumentNullException>(() => new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider()));
        }

        /// <summary>
        /// Factory will create a proper chain given a profile with steps.
        /// </summary>
        [TestMethod]
        public void Handler_ProfileWithSteps_CreatesChainProperly()
        {
            _collection.AddTransient<ChainProfile<TestChainRequest>, TestChainProfileWithSteps>();

            var factory = new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider());

            var firstHandler = factory.Handler;

            Assert.IsNotNull(firstHandler);
        }

        /// <summary>
        /// Factory is able to instantiate handlers properly that have external dependencies.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handler_CanInitializedHandlerDependencies()
        {
            _collection.AddTransient<TestChainDependency>();
            _collection.AddTransient<ChainProfile<TestChainRequest>, TestChainProfileWithDependentHandlers>();

            var factory = new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider());

            var handler = factory.Handler;

            var result = await handler.Handle(new TestChainRequest());

            Assert.AreEqual(2, result.Value);
        }

        /// <summary>
        /// Factory throws an exception when a handler does not have a public constructor.
        /// </summary>
        [TestMethod]
        public void Handler_HandlerWithNoPublicConstructorThrowsException()
        {
            _collection.AddTransient<ChainProfile<TestChainRequest>, TestChainProfileWithBadHandler>();

            Assert.ThrowsException<NullReferenceException>(() => new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider()));
        }

        /// <summary>
        /// Factory ignores handlers in the first position with non-public constructors.
        /// </summary>
        [TestMethod]
        public void Handler_HandlerWithNoPublicConstructorInFirstPositionIsIgnored()
        {
            _collection.AddTransient<ChainProfile<TestChainRequest>, TestChainProfileWithBadFirstHandler>();

            var factory = new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider());

            var chain = factory.Handler;

            Assert.IsNotNull(chain);
        }
    }
}
