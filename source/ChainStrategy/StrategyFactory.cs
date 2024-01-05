// <copyright file="StrategyFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using ChainStrategy.Registration;

namespace ChainStrategy
{
    /// <inheritdoc />
    public sealed class StrategyFactory : IStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">A service provider used to get dependencies of strategy handlers.</param>
        public StrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Executes a strategy operation given a particular request object.
        /// </summary>
        /// <typeparam name="TStrategyResponse">The type of the response object.</typeparam>
        /// <param name="request">The request to be executed.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to prematurely end the operation if needed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that wraps the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when no handlers could be found for a request and response.</exception>
        public async Task<TStrategyResponse> Execute<TStrategyResponse>(IStrategyRequest<TStrategyResponse> request, CancellationToken cancellationToken)
        {
            var requestType = request.GetType();

            var responseType = requestType.GetInterfaces()[0].GetGenericArguments()[0];

            if (Activator.CreateInstance(typeof(StrategyWrapper<,>).MakeGenericType(requestType, responseType)) is IStrategyWrapper<TStrategyResponse> service)
            {
                return await service.Execute(request, _serviceProvider, cancellationToken);
            }

            throw new NullReferenceException("The type of the request object could not be determined.");
        }
    }
}
