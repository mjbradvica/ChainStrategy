// <copyright file="IStrategyRequest.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// An interface to designate a request for a strategy handler.
    /// </summary>
    /// <typeparam name="TResponse">The response type for the strategy request.</typeparam>
    public interface IStrategyRequest<TResponse>
    {
    }

    /// <summary>
    /// An interface to designate a request for a strategy handler that returns void.
    /// </summary>
    public interface IStrategyRequest : IStrategyRequest<Unit>
    {
    }
}
