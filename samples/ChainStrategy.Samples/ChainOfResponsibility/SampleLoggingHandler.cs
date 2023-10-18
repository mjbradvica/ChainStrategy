// <copyright file="SampleLoggingHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// A sample base logging handler.
    /// </summary>
    /// <typeparam name="T">The type of the request object.</typeparam>
    public abstract class SampleLoggingHandler<T> : ChainHandler<T>
        where T : ChainRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleLoggingHandler{T}"/> class.
        /// </summary>
        /// <param name="handler">An instance of <see cref="IChainHandler{TRequest}"/>.</param>
        protected SampleLoggingHandler(IChainHandler<T>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// A middleware method for logging a request.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override Task<T> Middleware(T request)
        {
            try
            {
                return base.Middleware(request);
            }
            catch (Exception exception)
            {
                request.Faulted(exception);

                return Task.FromResult(request);
            }
        }
    }
}
