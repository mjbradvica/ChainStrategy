// <copyright file="ChainHandlerTests.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Tests for the <see cref="ChainHandler{TRequest}"/> class.
    /// </summary>
    [TestClass]
    public class ChainHandlerTests
    {
        /// <summary>
        /// Tests to ensure the request is return if it is in the faulted state.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_FaultedRequest_ReturnsImmediately()
        {
            var handler = new TestChainHandler(null);

            var request = new TestChainRequest();
            request.Faulted();

            var result = await handler.Handle(request);

            Assert.AreEqual(0, result.Value);
        }

        /// <summary>
        /// Tests to ensure on a valid request with a null next handler is processed correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_NullNextHandler_ProcessesAndReturns()
        {
            var handler = new TestChainHandler(null);

            var request = new TestChainRequest();

            var result = await handler.Handle(request);

            Assert.AreEqual(1, result.Value);
        }

        /// <summary>
        /// Tests to ensure on a valid request with a initialized next handler is processed correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_WithNextHandler_ProcessesAndReturns()
        {
            var handler = new TestChainHandler(new TestChainHandler(null));

            var request = new TestChainRequest();

            var result = await handler.Handle(request);

            Assert.AreEqual(2, result.Value);
        }
    }
}
