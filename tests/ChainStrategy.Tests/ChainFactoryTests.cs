// <copyright file="ChainFactoryTests.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;
using System.Threading;
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
        /// Factory will throw an exception when no profiles are found for a given request.
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

            Assert.ThrowsException<NullReferenceException>(() => new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider()));
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

            var result = await factory.Execute(new TestChainRequest(), CancellationToken.None);

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
        /// Factory throws an exception when a handler dependency is not registered.
        /// </summary>
        [TestMethod]
        public void Handler_DependencyNotRegistered_ThrowsException()
        {
            _collection.AddTransient<ChainProfile<TestChainRequest>, TestChainProfileWithDependentHandlers>();

            Assert.ThrowsException<NullReferenceException>(() => new ChainFactory<TestChainRequest>(_collection.BuildServiceProvider()));
        }
    }
}
