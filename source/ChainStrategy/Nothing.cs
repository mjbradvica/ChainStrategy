﻿// <copyright file="Nothing.cs" company="Simplex Software LLC">
// Copyright (c) Simplex Software LLC. All rights reserved.
// </copyright>

namespace ChainStrategy
{
    /// <summary>
    /// Class that represents a void return type for a strategy handler.
    /// </summary>
    public sealed class Nothing
    {
        /// <summary>
        /// An instance of Nothing to represent void.
        /// </summary>
        public static readonly Nothing Value = null!;
    }
}
