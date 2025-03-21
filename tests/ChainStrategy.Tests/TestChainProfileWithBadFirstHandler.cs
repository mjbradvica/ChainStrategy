﻿// <copyright file="TestChainProfileWithBadFirstHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Tests
{
    /// <summary>
    /// A test chain profile with a bad handler in the first position.
    /// </summary>
    internal class TestChainProfileWithBadFirstHandler : ChainProfile<TestChainPayload>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestChainProfileWithBadFirstHandler"/> class.
        /// </summary>
        public TestChainProfileWithBadFirstHandler()
        {
            AddStep<TestHandlerNonPublicConstructor>().AddStep<TestChainHandler>();
        }
    }
}
