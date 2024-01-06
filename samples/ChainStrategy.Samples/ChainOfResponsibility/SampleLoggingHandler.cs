// <copyright file="SampleLoggingHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility
{
    /// <summary>
    /// A sample base logging handler.
    /// </summary>
    /// <typeparam name="T">The type of the payload object.</typeparam>
    public abstract class SampleLoggingHandler<T> : ChainHandler<T>
        where T : ChainPayload
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
        /// A middleware method for logging a payload.
        /// </summary>
        /// <param name="payload">The payload object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public override async Task<T> Middleware(T payload, CancellationToken cancellationToken)
        {
            try
            {
                return await base.Middleware(payload, cancellationToken);
            }
            catch (Exception exception)
            {
                payload.Faulted(exception);

                return payload;
            }
        }
    }
}
