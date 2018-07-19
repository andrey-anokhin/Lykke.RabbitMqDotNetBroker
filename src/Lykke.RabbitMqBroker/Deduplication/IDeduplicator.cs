﻿using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Lykke.RabbitMqBroker.Deduplication
{
    /// <summary>
    /// Checks if the passed value was already received some time ago.
    /// </summary>
    [PublicAPI]
    public interface IDeduplicator
    {
        /// <summary>
        /// Check if the passed value was already received some time ago.
        /// </summary>
        Task<bool> EnsureNotDuplicateAsync(byte[] value);
    }
}
