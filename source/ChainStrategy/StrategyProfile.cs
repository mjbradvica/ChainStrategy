// <copyright file="StrategyProfile.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// A profile used to define under what conditions should a particular strategy be used.
    /// </summary>
    /// <typeparam name="TStrategyRequest">The request type for the profile being created.</typeparam>
    /// <typeparam name="TStrategyResponse">The response type for the profile being created.</typeparam>
    public abstract class StrategyProfile<TStrategyRequest, TStrategyResponse>
        where TStrategyRequest : IStrategyRequest<TStrategyResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyProfile{TStrategyRequest, TStrategyResponse}"/> class.
        /// </summary>
        protected StrategyProfile()
        {
            Strategies = new Dictionary<Predicate<TStrategyRequest>, Type>();
        }

        /// <summary>
        /// Gets a dictionary of predicates that determine which strategy to be executed.
        /// </summary>
        public Dictionary<Predicate<TStrategyRequest>, Type> Strategies { get; }

        /// <summary>
        /// Gets the default strategy type to be called if no defined conditions satisfy the request.
        /// </summary>
        public Type? Default { get; private set; }

        /// <summary>
        /// Adds a strategy to the profile given a certain request condition.
        /// </summary>
        /// <typeparam name="TStrategyHandler">The strategy handler to be added for the condition.</typeparam>
        /// <param name="strategyPredicate">A <see cref="Predicate{T}"/> for the given handler to be called.</param>
        public void AddStrategy<TStrategyHandler>(Predicate<TStrategyRequest> strategyPredicate)
            where TStrategyHandler : IStrategyHandler<TStrategyRequest, TStrategyResponse>
        {
            Strategies.TryAdd(strategyPredicate, typeof(TStrategyHandler));
        }

        /// <summary>
        /// Adds a default strategy handler if no defined condition is met.
        /// </summary>
        /// <typeparam name="TStrategyHandler">The default handler to be used.</typeparam>
        public void AddDefault<TStrategyHandler>()
            where TStrategyHandler : IStrategyHandler<TStrategyRequest, TStrategyResponse>
        {
            Default = typeof(TStrategyHandler);
        }
    }
}