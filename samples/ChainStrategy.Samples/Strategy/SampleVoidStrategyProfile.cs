// <copyright file="SampleVoidStrategyProfile.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.Strategy
{
    /// <summary>
    /// Sample void strategy profile.
    /// </summary>
    internal class SampleVoidStrategyProfile : StrategyProfile<SampleVoidStrategyRequest, Nothing>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleVoidStrategyProfile"/> class.
        /// </summary>
        public SampleVoidStrategyProfile()
        {
            AddDefault<SampleVoidStrategyHandler>();
        }
    }
}
