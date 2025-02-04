// <copyright file="ChainProfile.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// Allows an end-user to configure handler and their order of operations for a payload.
    /// </summary>
    /// <typeparam name="TPayload">The payload for the chain being configured.</typeparam>
    public abstract class ChainProfile<TPayload>
        where TPayload : IChainPayload
    {
        private readonly List<Type> _registrations;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChainProfile{TPayload}"/> class.
        /// </summary>
        protected ChainProfile()
        {
            _registrations = new List<Type>();
        }

        /// <summary>
        /// Gets all the of the chain registrations associated with the payload.
        /// </summary>
        public IReadOnlyList<Type> ChainRegistrations => _registrations.AsReadOnly();

        /// <summary>
        /// Adds a chain handler to the order of operations.
        /// </summary>
        /// <typeparam name="THandler">The handler being added to the chain order.</typeparam>
        /// <returns>The ChainProfile instance to continue adding handlers.</returns>
        public ChainProfile<TPayload> AddStep<THandler>()
            where THandler : IChainHandler<TPayload>
        {
            _registrations.Add(typeof(THandler));

            return this;
        }
    }
}
