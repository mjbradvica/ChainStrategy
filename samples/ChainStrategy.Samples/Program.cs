// <copyright file="Program.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

using System.Reflection;
using ChainStrategy.Registration;
using ChainStrategy.Samples.ChainOfResponsibility.Constrained;
using ChainStrategy.Samples.ChainOfResponsibility.Standard;
using ChainStrategy.Samples.Strategy;
using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy.Samples
{
    /// <summary>
    /// Sample project for the ChainStrategy library.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Sample main entry.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main()
        {
            var services = new ServiceCollection();

            services.AddChainStrategy(Assembly.GetExecutingAssembly());

            var provider = services.BuildServiceProvider();

            var factory = provider.GetRequiredService<IChainFactory>();

            var result = await factory.Execute(new SampleChainPayload(5));

            Console.WriteLine(result.Value);

            var strategyFactory = provider.GetRequiredService<IStrategyFactory>();

            var strategyResult = await strategyFactory.Execute(new SampleStrategyRequest());

            Console.WriteLine(strategyResult.Value);

            var constrainedFactory = provider.GetRequiredService<IChainFactory>();

            var constrainedRequest = await constrainedFactory.Execute(new SampleConstrainedPayload());

            Console.WriteLine(constrainedRequest.Id);
        }
    }
}