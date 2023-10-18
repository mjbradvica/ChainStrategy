// <copyright file="SampleStrategyMultiplication.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// A sample strategy handler to perform multiplication.
    /// </summary>
    internal class SampleStrategyMultiplication : IStrategyHandler<SampleStrategyRequest, SampleStrategyResponse>
    {
        /// <summary>
        /// Performs multiplication on our request.
        /// </summary>
        /// <param name="request">The request object to execute.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<SampleStrategyResponse> Handle(SampleStrategyRequest request)
        {
            return Task.FromResult(new SampleStrategyResponse { Value = request.InitialValue * 3 });
        }
    }
}
