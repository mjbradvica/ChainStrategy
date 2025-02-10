// <copyright file="SampleStrategyRequest.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// A class that represents a sample strategy request.
    /// </summary>
    internal class SampleStrategyRequest : IStrategyRequest<SampleStrategyResponse>
    {
        /// <summary>
        /// Gets or sets the initial value for the sample request.
        /// </summary>
        public int InitialValue { get; set; } = 9;
    }
}
