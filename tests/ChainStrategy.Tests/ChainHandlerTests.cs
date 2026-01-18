// <copyright file="ChainHandlerTests.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

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
        public async Task HandleFaultedRequestReturnsImmediately()
        {
            var handler = new TestChainHandler(null);

            var request = new TestChainPayload();
            request.Faulted();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(0, result.Value);
        }

        /// <summary>
        /// Tests to ensure on a valid request with a null next handler is processed correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task HandleNullNextHandlerProcessesAndReturns()
        {
            var handler = new TestChainHandler(null);

            var request = new TestChainPayload();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(1, result.Value);
        }

        /// <summary>
        /// Tests to ensure on a valid request with an initialized next handler is processed correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task HandleWithNextHandlerProcessesAndReturns()
        {
            var handler = new TestChainHandler(new TestChainHandler(null));

            var request = new TestChainPayload();

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.AreEqual(2, result.Value);
        }
    }
}
