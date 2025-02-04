// <copyright file="TestStrategyRequest.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// Test request for Strategy unit tests.
    /// </summary>
    internal class TestStrategyRequest : IStrategyRequest<TestStrategyResponse>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the strategy should use addition.
        /// </summary>
        public bool ShouldUseAddition { get; set; }

        /// <summary>
        /// Gets or sets a start value to use as a condition.
        /// </summary>
        public string? StartValue { get; set; }
    }
}
