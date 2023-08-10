using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy
{
    /// <summary>
    /// An implementation of the <see cref="IStrategyFactory{TStrategyRequest,TStrategyResponse}"/> that will initialize handlers and execute a strategy request.
    /// </summary>
    /// <typeparam name="TStrategyRequest">The request type to be executed.</typeparam>
    /// <typeparam name="TStrategyResponse">The response type to be returned from the operation.</typeparam>
    public sealed class StrategyFactory<TStrategyRequest, TStrategyResponse> : IStrategyFactory<TStrategyRequest, TStrategyResponse>
        where TStrategyRequest : IStrategyRequest<TStrategyResponse>
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyFactory{TStrategyRequest, TStrategyResponse}"/> class.
        /// </summary>
        /// <param name="serviceProvider">A service provider used to get dependencies of strategy handlers.</param>
        public StrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Executes a strategy operation given a particular request object.
        /// </summary>
        /// <param name="request">The request to be executed.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation that wraps the response.</returns>
        /// <exception cref="ArgumentNullException">Thrown when no handlers could be found for a request and response.</exception>
        public async Task<TStrategyResponse> ExecuteStrategy(TStrategyRequest request)
        {
            var builder = _serviceProvider.GetRequiredService<StrategyProfile<TStrategyRequest, TStrategyResponse>>();

            var matchingKey = builder.Strategies.Keys.FirstOrDefault(conditionCheck => conditionCheck.Invoke(request));

            if (matchingKey != null)
            {
                var handler = GetHandlerForType(builder.Strategies[matchingKey]);

                if (handler != null)
                {
                    return await handler.Handle(request);
                }
            }

            if (builder.Default != null)
            {
                var handler = GetHandlerForType(builder.Default);

                if (handler != null)
                {
                    return await handler.Handle(request);
                }
            }

            throw new ArgumentNullException(nameof(request), "No handler matched the request condition.");
        }

        private IStrategyHandler<TStrategyRequest, TStrategyResponse>? GetHandlerForType(Type type)
        {
            var constructor = type.GetConstructors().FirstOrDefault(constructorInfo => constructorInfo.IsPublic);

            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(type), "No public constructor on your handler exists.");
            }

            var dependencies = constructor.GetParameters().Select(parameterInfo => _serviceProvider.GetService(parameterInfo.ParameterType));

            return constructor.Invoke(dependencies.ToArray()) as IStrategyHandler<TStrategyRequest, TStrategyResponse>;
        }
    }
}
