// <copyright file="Nothing.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// Class that represents a void return type for a strategy handler.
    /// </summary>
    /// TODO: Pramga will be resolved at a future time and point.
#pragma warning disable CA1716
    [Obsolete("Will be removed for the .NET 11 release. Please use the Unit class instead.")]
    public sealed class Nothing
#pragma warning restore CA1716
    {
        /// <summary>
        /// An instance of Nothing to represent void.
        /// </summary>
        public static readonly Nothing Value = null!;
    }
}
