﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy.Registration
{
    /// <summary>
    /// Extensions methods that allow for easy registration of the ChainStrategy library.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all dependencies for the ChainStrategy library into the ServiceCollection.
        /// </summary>
        /// <param name="services">The service collection for registration.</param>
        /// <param name="assemblies">The assembly to scan for dependencies in.</param>
        /// <returns>The ServiceCollection object to continue with.</returns>
        public static IServiceCollection AddChainStrategy(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                throw new ArgumentNullException(nameof(assemblies), "You must provide at least one assembly to register.");
            }

            foreach (var assembly in assemblies)
            {
                TryAddChainProfiles(services, assembly, typeof(ChainProfile<>));
                TryAddChainProfiles(services, assembly, typeof(StrategyProfile<,>));
            }

            services.AddTransient(typeof(IChainFactory<>), typeof(ChainFactory<>));
            services.AddTransient(typeof(IStrategyFactory<,>), typeof(StrategyFactory<,>));

            return services;
        }

        private static void TryAddChainProfiles(IServiceCollection services, Assembly assembly, Type baseType)
        {
            assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface)
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == baseType)
                .ToList()
                .ForEach(implementationType =>
                {
                    if (implementationType.BaseType != null)
                    {
                        services.AddTransient(implementationType.BaseType, implementationType);
                    }
                });
        }
    }
}
