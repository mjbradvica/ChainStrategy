// <copyright file="ChainFactory.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy
{
    /// <summary>
    /// Attempts to create a chain of responsibility for a given payload object.
    /// </summary>
    public sealed class ChainFactory : IChainFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Type> _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainFactory"/> class.
        /// </summary>
        /// <param name="serviceProvider">A service provider to extract dependencies from.</param>
        public ChainFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _registrations = new List<Type>();
        }

        /// <inheritdoc/>
        public async Task<TPayload> Execute<TPayload>(TPayload payload, CancellationToken cancellationToken)
            where TPayload : IChainPayload
        {
            var profile = _serviceProvider.GetService<ChainProfile<TPayload>>();

            if (profile == null)
            {
                throw new ArgumentNullException(nameof(payload), "No profile's were found for the provided payload.");
            }

            if (!profile.ChainRegistrations.Any())
            {
                throw new ArgumentNullException(nameof(payload), "The profile does not have any steps registered.");
            }

            _registrations = profile.ChainRegistrations.Reverse().ToList();

            var (handler, _) = InstantiateHandlers<TPayload>(null, 0);

            if (handler != null)
            {
                return await handler.Handle(payload, cancellationToken);
            }

            throw new ArgumentNullException(nameof(payload), "A handler could not be initialized for the parameters supplied. Does your profile have steps?");
        }

        private (IChainHandler<TPayload>? Handler, int Index) InstantiateHandlers<TPayload>(IChainHandler<TPayload>? handler, int index)
            where TPayload : IChainPayload
        {
            if (index == _registrations.Count)
            {
                return (handler, index);
            }

            if (handler == null)
            {
                var handlerType = _registrations.First().GetConstructors().FirstOrDefault(constructorInfo => constructorInfo.IsPublic);

                if (handlerType != null)
                {
                    var dependencies = new List<object?>();

                    foreach (var dependency in handlerType.GetParameters())
                    {
                        if (dependency.ParameterType == typeof(IChainHandler<TPayload>))
                        {
                            dependencies.Add(null);
                        }
                        else
                        {
                            var parameter = _serviceProvider.GetService(dependency.ParameterType);

                            if (parameter == null)
                            {
                                throw new ArgumentNullException(nameof(handler), $"A chain parameter could not be resolved for the type {dependency.ParameterType}. Did you register it?");
                            }

                            dependencies.Add(parameter);
                        }
                    }

                    var step = handlerType.Invoke(dependencies.ToArray()) as IChainHandler<TPayload>;

                    return InstantiateHandlers(step, ++index);
                }

                throw new ArgumentNullException(nameof(handler), $"A public constructor for {handlerType} could not be found. You must have a public constructor.");
            }
            else
            {
                var handlerType = _registrations[index].GetConstructors().FirstOrDefault(constructorInfo => constructorInfo.IsPublic);

                if (handlerType != null)
                {
                    var dependencies = new List<object?>();

                    foreach (var dependency in handlerType.GetParameters())
                    {
                        if (dependency.ParameterType == typeof(IChainHandler<TPayload>))
                        {
                            dependencies.Add(handler);
                        }
                        else
                        {
                            dependencies.Add(_serviceProvider.GetService(dependency.ParameterType));
                        }
                    }

                    var step = handlerType.Invoke(dependencies.ToArray()) as IChainHandler<TPayload>;

                    return InstantiateHandlers(step, ++index);
                }
            }

            return InstantiateHandlers(handler, ++index);
        }
    }
}
