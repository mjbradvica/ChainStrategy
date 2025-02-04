// <copyright file="StrategyFactory.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

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
            if (Activator.CreateInstance(typeof(StrategyWrapper<,>).MakeGenericType(request.GetType(), typeof(TStrategyResponse))) is IStrategyWrapper<TStrategyResponse> service)
            {
                return await service.Execute(request, _serviceProvider, cancellationToken);
            }

            throw new NullReferenceException("The type of the request object could not be determined.");
        }
    }
}
