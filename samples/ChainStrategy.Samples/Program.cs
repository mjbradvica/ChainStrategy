// <copyright file="Program.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System.Reflection;
using ChainStrategy.Registration;
using ChainStrategy.Samples.ChainOfResponsibility;
using ChainStrategy.Samples.Strategy;
using Microsoft.Extensions.DependencyInjection;

namespace ChainStrategy.Samples
{
    /// <summary>
    /// Sample project for the ChainStrategy library.
    /// </summary>
    public class Program
    {
        private static async Task Main()
        {
            var services = new ServiceCollection();

            services.AddChainStrategy(Assembly.GetExecutingAssembly());

            var provider = services.BuildServiceProvider();

            var factory = provider.GetRequiredService<IChainFactory<SampleChainRequest>>();

            var result = await factory.Execute(new SampleChainRequest(5));

            Console.WriteLine(result.Value);

            var strategyFactory = provider.GetRequiredService<IStrategyFactory<SampleStrategyRequest, SampleStrategyResponse>>();

            var strategyResult = await strategyFactory.Execute(new SampleStrategyRequest());

            Console.WriteLine(strategyResult.Value);

            var constrainedFactory = provider.GetRequiredService<IChainFactory<SampleConstrainedRequest>>();

            var constrainedRequest = await constrainedFactory.Execute(new SampleConstrainedRequest());

            Console.WriteLine(constrainedRequest.Id);
        }
    }
}