// <copyright file="SampleStrategyLoggingHandler.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using Serilog;

namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// Sample base logging handler.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <typeparam name="TResponse">The type of the response object.</typeparam>
    internal abstract class SampleStrategyLoggingHandler<TRequest, TResponse> : IStrategyHandler<TRequest, TResponse>
        where TRequest : IStrategyRequest<TResponse>
        where TResponse : new()
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleStrategyLoggingHandler{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">An instance of the <see cref="ILogger"/> interface.</param>
        protected SampleStrategyLoggingHandler(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Virtual method for a base logging handler.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await DoWork(request, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.Error(exception, $"An exception occurred at {DateTime.UtcNow} in the {GetType().Name} handler.");
            }

            return new TResponse();
        }

        /// <summary>
        /// Method to be implemented by an inherited handler.
        /// </summary>
        /// <param name="request">The request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public abstract Task<TResponse> DoWork(TRequest request, CancellationToken cancellationToken);
    }
}
