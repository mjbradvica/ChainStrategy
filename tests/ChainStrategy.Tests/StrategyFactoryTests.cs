// <copyright file="StrategyFactoryTests.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test the <see cref="StrategyFactory"/> class capabilities.
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
            _serviceCollection.AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, EmptyChainStrategyProfile>();

            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExactlyAsync<NullReferenceException>(async () => await factory.Execute(new TestStrategyRequest(), CancellationToken.None));
        }

        /// <summary>
        /// Ensures an exception is thrown when no profiles can be found.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_NoProfile_ThrowsException()
        {
            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExactlyAsync<NullReferenceException>(async () => await factory.Execute(new TestStrategyRequest(), CancellationToken.None));
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

            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            var result = await factory.Execute(new TestStrategyRequest(), CancellationToken.None);

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

            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            var result = await factory.Execute(new TestStrategyRequest(), CancellationToken.None);

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

            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () => await factory.Execute(new TestStrategyRequest(), CancellationToken.None));
        }

        /// <summary>
        /// Ensures an exception is throw for handlers who don't have dependencies registered.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_HandlerWithNonRegisteredDependencies_ThrowsException()
        {
            _serviceCollection
                .AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, StrategyProfileWithDependentHandler>();

            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            await Assert.ThrowsExactlyAsync<NullReferenceException>(async () => await factory.Execute(new TestStrategyRequest(), CancellationToken.None));
        }

        /// <summary>
        /// Ensures a strategy executes with a handler with dependencies.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task ExecuteStrategy_HandlerNonRegisteredDependencies_ExecutesCorrectly()
        {
            _serviceCollection.AddTransient<TestChainDependency>();
            _serviceCollection
                .AddTransient<StrategyProfile<TestStrategyRequest, TestStrategyResponse>, StrategyProfileWithDependentHandler>();

            var factory = new StrategyFactory(_serviceCollection.BuildServiceProvider());

            var result = await factory.Execute(new TestStrategyRequest(), CancellationToken.None);

            Assert.AreEqual(5, result.Value);
        }
    }
}
