// <copyright file="ChainProfile.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace ChainStrategy
{
    /// <summary>
    /// Allows an end-user to configure handler and their order of operations for a request.
    /// </summary>
    /// <typeparam name="TRequest">The request for the chain being configured.</typeparam>
    public class ChainProfile<TRequest>
        where TRequest : IChainRequest
    {
        private readonly List<Type> _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainProfile{TRequest}"/> class.
        /// </summary>
        public ChainProfile()
        {
            _registrations = new List<Type>();
        }

        /// <summary>
        /// Gets all the of the chain registrations associated with the request.
        /// </summary>
        public IReadOnlyList<Type> ChainRegistrations => _registrations.AsReadOnly();

        /// <summary>
        /// Adds a chain handler to the order of operations.
        /// </summary>
        /// <typeparam name="THandler">The handler being added to the chain order.</typeparam>
        /// <returns>The ChainProfile instance to continue adding handlers.</returns>
        public ChainProfile<TRequest> AddStep<THandler>()
            where THandler : IChainHandler<TRequest>
        {
            _registrations.Add(typeof(THandler));

            return this;
        }
    }
}
