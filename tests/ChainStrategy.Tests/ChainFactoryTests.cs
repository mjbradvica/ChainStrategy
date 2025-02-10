// <copyright file="ChainFactoryTests.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Tests the <see cref="ChainFactory"/> class capabilities.
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
        /// Factory will throw an exception when no profiles are found for a given request.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handler_NullProfile_ThrowsException()
        {
            var factory = new ChainFactory(_collection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await factory.Execute(new TestChainPayload(), CancellationToken.None));
        }

        /// <summary>
        /// Factory will throw an exception when no registrations are present in a profile.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handler_NoRegistrations_ThrowsException()
        {
            _collection.AddTransient<ChainProfile<TestChainPayload>, TestChainProfile>();

            var factory = new ChainFactory(_collection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await factory.Execute(new TestChainPayload(), CancellationToken.None));
        }

        /// <summary>
        /// Factory is able to instantiate handlers properly that have external dependencies.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handler_CanInitializedHandlerDependencies()
        {
            _collection.AddTransient<TestChainDependency>();
            _collection.AddTransient<ChainProfile<TestChainPayload>, TestChainProfileWithDependentHandlers>();

            var factory = new ChainFactory(_collection.BuildServiceProvider());

            var result = await factory.Execute(new TestChainPayload(), CancellationToken.None);

            Assert.AreEqual(2, result.Value);
        }

        /// <summary>
        /// Factory throws an exception when a handler does not have a public constructor.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handler_HandlerWithNoPublicConstructorThrowsException()
        {
            _collection.AddTransient<ChainProfile<TestChainPayload>, TestChainProfileWithBadHandler>();

            var factory = new ChainFactory(_collection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await factory.Execute(new TestChainPayload(), CancellationToken.None));
        }

        /// <summary>
        /// Factory throws an exception when a handler dependency is not registered.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handler_DependencyNotRegistered_ThrowsException()
        {
            _collection.AddTransient<ChainProfile<TestChainPayload>, TestChainProfileWithDependentHandlers>();

            var factory = new ChainFactory(_collection.BuildServiceProvider());

            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () => await factory.Execute(new TestChainPayload(), CancellationToken.None));
        }
    }
}
