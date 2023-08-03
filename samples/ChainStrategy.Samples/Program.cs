using System.Reflection;
using ChainStrategy.Registration;
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
        }
    }
}