using System;
using System.Reflection;
using ChainStrategy.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test the <see cref="ServiceCollectionExtensions"/> class capabilities.
    /// </summary>
    [TestClass]
    public class ServiceCollectionExtensionsTests
    {
        /// <summary>
        /// Ensures an exception is thrown when no assemblies are supplied.
        /// </summary>
        [TestMethod]
        public void AddChainStrategy_NoAssemblies_ThrowsException()
        {
            var collection = new ServiceCollection();

            Assert.ThrowsException<ArgumentNullException>(() => collection.AddChainStrategy());
        }

        /// <summary>
        /// Ensures that all types are correctly registered.
        /// </summary>
        [TestMethod]
        public void AddChainStrategy_CorrectlyAddsTypes()
        {
            var collection = new ServiceCollection();

            collection.AddChainStrategy(Assembly.GetExecutingAssembly());

            var provider = collection.BuildServiceProvider();

            var chainFactory = provider.GetRequiredService<IChainFactory<TestChainRequest>>();
            var strategyFactory = provider.GetRequiredService<IStrategyFactory<TestStrategyRequest, TestStrategyResponse>>();
            var chainProfile = provider.GetRequiredService<ChainProfile<TestChainRequest>>();
            var strategyProfile = provider.GetRequiredService<StrategyProfile<TestStrategyRequest, TestStrategyResponse>>();

            Assert.IsNotNull(chainFactory);
            Assert.IsNotNull(strategyFactory);
            Assert.IsNotNull(chainProfile);
            Assert.IsNotNull(strategyProfile);
        }
    }
}
