using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy
{
    /// <summary>
    /// Attempts to create a chain of responsibility for a given request object.
    /// </summary>
    /// <typeparam name="TRequest">The request object for the chain being created.</typeparam>
    public sealed class ChainFactory<TRequest> : IChainFactory<TRequest>
        where TRequest : IChainRequest
    {
        private readonly IServiceProvider _serviceProvider;
        private IList<Type> _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainFactory{TRequest}"/> class.
        /// </summary>
        /// <param name="serviceProvider">A service provider to extract dependencies from.</param>
        public ChainFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _registrations = new List<Type>();
        }

        /// <summary>
        /// Creates a chain of responsibility for a given request object.
        /// </summary>
        /// <returns>A chain handler ready for execution.</returns>
        /// <exception cref="NotImplementedException">Thrown when a profile could not be found for the request object.</exception>
        public IChainHandler<TRequest> CreateChain()
        {
            var profile = _serviceProvider.GetService<ChainProfile<TRequest>>();

            if (profile == null)
            {
                throw new NullReferenceException("No profile's were found for the provided request.");
            }

            if (!profile.ChainRegistrations.Any())
            {
                throw new ArgumentNullException(nameof(profile.ChainRegistrations), "The profile does not have any steps registered.");
            }

            _registrations = profile.ChainRegistrations.Reverse().ToList();

            var (handler, _) = InstantiateHandlers(null, 0);

            if (handler != null)
            {
                return handler;
            }

            throw new NullReferenceException("A handler could not be initialized for the parameters supplied. Does your profile have steps?");
        }

        private (IChainHandler<TRequest>? Handler, int Index) InstantiateHandlers(IChainHandler<TRequest>? handler, int index)
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
                            dependencies.Add(_serviceProvider.GetService(dependency.ParameterType));
                        }
                    }

                    var step = handlerType.Invoke(dependencies.ToArray()) as IChainHandler<TRequest>;

                    return InstantiateHandlers(step, ++index);
                }
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
