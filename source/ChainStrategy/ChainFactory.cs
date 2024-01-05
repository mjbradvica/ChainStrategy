// <copyright file="ChainFactory.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy
{
    /// <summary>
    /// Attempts to create a chain of responsibility for a given request object.
    /// </summary>
    public sealed class ChainFactory : IChainFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private IList<Type> _registrations;

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
        public async Task<TRequest> Execute<TRequest>(TRequest request, CancellationToken cancellationToken)
            where TRequest : IChainRequest
        {
            var profile = _serviceProvider.GetService<ChainProfile<TRequest>>();

            if (profile == null)
            {
                throw new NullReferenceException("No profile's were found for the provided request.");
            }

            if (!profile.ChainRegistrations.Any())
            {
                throw new NullReferenceException("The profile does not have any steps registered.");
            }

            _registrations = profile.ChainRegistrations.Reverse().ToList();

            var (handler, _) = InstantiateHandlers<TRequest>(null, 0);

            if (handler != null)
            {
                return await handler.Handle(request, cancellationToken);
            }

            throw new NullReferenceException("A handler could not be initialized for the parameters supplied. Does your profile have steps?");
        }

        private (IChainHandler<TRequest>? Handler, int Index) InstantiateHandlers<TRequest>(IChainHandler<TRequest>? handler, int index)
            where TRequest : IChainRequest
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
                        if (dependency.ParameterType == typeof(IChainHandler<TRequest>))
                        {
                            dependencies.Add(null);
                        }
                        else
                        {
                            var parameter = _serviceProvider.GetService(dependency.ParameterType);

                            if (parameter == null)
                            {
                                throw new NullReferenceException($"A chain parameter could not be resolved for the type {dependency.ParameterType}. Did you register it?");
                            }

                            dependencies.Add(parameter);
                        }
                    }

                    var step = handlerType.Invoke(dependencies.ToArray()) as IChainHandler<TRequest>;

                    return InstantiateHandlers(step, ++index);
                }

                throw new NullReferenceException($"A public constructor for {handlerType} could not be found. You must have a public constructor.");
            }
            else
            {
                var handlerType = _registrations[index].GetConstructors().FirstOrDefault(constructorInfo => constructorInfo.IsPublic);

                if (handlerType != null)
                {
                    var dependencies = new List<object?>();

                    foreach (var dependency in handlerType.GetParameters())
                    {
                        if (dependency.ParameterType == typeof(IChainHandler<TRequest>))
                        {
                            dependencies.Add(handler);
                        }
                        else
                        {
                            dependencies.Add(_serviceProvider.GetService(dependency.ParameterType));
                        }
                    }

                    var step = handlerType.Invoke(dependencies.ToArray()) as IChainHandler<TRequest>;

                    return InstantiateHandlers(step, ++index);
                }
            }

            return InstantiateHandlers(handler, ++index);
        }
    }
}
