// <copyright file="IStrategyWrapper.cs" company="Michael Bradvica LLC">
// Copyright (c) Michael Bradvica LLC. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChainStrategy.Registration
{
    /// <summary>
    /// A wrapper to initialize a <see cref="IStrategyHandler{TStrategyRequest,TStrategyResponse}"/>.
    /// </summary>
    /// <typeparam name="TStrategyResponse">The type of the response.</typeparam>
    internal interface IStrategyWrapper<TStrategyResponse>
    {
        /// <summary>
        /// Executes a non-descriptive request object.
        /// </summary>
        /// <param name="request">The request object to be executed.</param>
        /// <param name="serviceProvider">An instance of the <see cref="IServiceProvider"/> interface.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/>of the response type.</returns>
        Task<TStrategyResponse> Execute(IStrategyRequest<TStrategyResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }
}
