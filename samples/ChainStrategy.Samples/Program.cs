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

            var handler = factory.CreateChain();

            var result = await handler.Handle(new SampleChainRequest(5));

            Console.WriteLine(result.Value);

            var strategyFactory = provider.GetRequiredService<IStrategyFactory<SampleStrategyRequest, SampleStrategyResponse>>();

            var strategyResult = await strategyFactory.ExecuteStrategy(new SampleStrategyRequest());

            Console.WriteLine(strategyResult.Value);
        }
    }
}