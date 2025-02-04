// <copyright file="StrategyWrapper.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy.Registration
{
    /// <inheritdoc />
    internal class StrategyWrapper<TStrategyRequest, TStrategyResponse> : IStrategyWrapper<TStrategyResponse>
        where TStrategyRequest : IStrategyRequest<TStrategyResponse>
    {
        /// <inheritdoc/>
        public async Task<TStrategyResponse> Execute(IStrategyRequest<TStrategyResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            var builder = serviceProvider.GetService<StrategyProfile<TStrategyRequest, TStrategyResponse>>();

            if (builder == null)
            {
                throw new NullReferenceException("No profile was found. Did you forget to register?");
            }

            var matchingKey = builder.Strategies.Keys.FirstOrDefault(conditionCheck => conditionCheck.Invoke((TStrategyRequest)request));

            if (matchingKey != null)
            {
                var handler = GetHandlerForType(builder.Strategies[matchingKey], serviceProvider);

                if (handler != null)
                {
                    return await handler.Handle((TStrategyRequest)request, cancellationToken);
                }
            }

            if (builder.Default != null)
            {
                var handler = GetHandlerForType(builder.Default, serviceProvider);

                if (handler != null)
                {
                    return await handler.Handle((TStrategyRequest)request, cancellationToken);
                }
            }

            throw new NullReferenceException("A strategy handler could not be instantiated based on the arguments given.");
        }

        private static IStrategyHandler<TStrategyRequest, TStrategyResponse>? GetHandlerForType(Type type, IServiceProvider serviceProvider)
        {
            var constructor = type.GetConstructors().FirstOrDefault(constructorInfo => constructorInfo.IsPublic);

            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(type), "No public constructor on your handler exists.");
            }

            var dependencies = new List<object?>();

            foreach (var parameterInfo in constructor.GetParameters())
            {
                var dependency = serviceProvider.GetService(parameterInfo.ParameterType);

                if (dependency == null)
                {
                    throw new NullReferenceException($"The dependency {parameterInfo.ParameterType} could not be resolved. Did you register it?");
                }

                dependencies.Add(dependency);
            }

            return constructor.Invoke(dependencies.ToArray()) as IStrategyHandler<TStrategyRequest, TStrategyResponse>;
        }
    }
}
