// <copyright file="SampleVoidStrategyHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// Sample void strategy handler.
    /// </summary>
    internal class SampleVoidStrategyHandler : IStrategyHandler<SampleVoidStrategyRequest>
    {
        /// <summary>
        /// Method for a void handler.
        /// </summary>
        /// <param name="request">A request object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<Nothing> Handle(SampleVoidStrategyRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Nothing.Value);
        }
    }
}
