// <copyright file="SampleBaseConstrainedHandler.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy.Samples.ChainOfResponsibility.Constrained
{
    /// <summary>
    /// Sample constrained handler.
    /// </summary>
    /// <typeparam name="TConstraint">The type of the payload object.</typeparam>
    public abstract class SampleBaseConstrainedHandler<TConstraint> : ChainHandler<TConstraint>
        where TConstraint : ISampleConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleBaseConstrainedHandler{TRequest}"/> class.
        /// </summary>
        /// <param name="handler">The successor handler.</param>
        protected SampleBaseConstrainedHandler(IChainHandler<TConstraint>? handler)
            : base(handler)
        {
        }

        /// <summary>
        /// Handles a constrained payload.
        /// </summary>
        /// <param name="payload">Chain payload object.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override Task<TConstraint> DoWork(TConstraint payload, CancellationToken cancellationToken)
        {
            var id = payload.Id;

            if (id == Guid.Empty)
            {
                id = Guid.NewGuid();
            }

            payload.UpdateId(id);

            return Task.FromResult(payload);
        }
    }
}
