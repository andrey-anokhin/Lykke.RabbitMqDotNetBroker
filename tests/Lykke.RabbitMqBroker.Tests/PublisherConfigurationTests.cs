﻿// Copyright (c) Lykke Corp.
// Licensed under the MIT License. See the LICENSE file in the project root for more information.

using System;
using Lykke.Logs;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.RabbitMqBroker.Subscriber;
using NUnit.Framework;

namespace RabbitMqBrokerTests
{
    using NSubstitute;

    [TestFixture]
    internal sealed class PublisherConfigurationTests
    {
        private RabbitMqPublisher<string> _publisher;

        [SetUp]
        public void SetUp()
        {
            var settings = new RabbitMqSubscriptionSettings
            {
                ConnectionString = "",
                DeadLetterExchangeName = "",
                ExchangeName = "",
                IsDurable = true,
                QueueName = "",
                RoutingKey = "RoutingKey"
            };

            _publisher = new RabbitMqPublisher<string>(EmptyLogFactory.Instance, settings);

            _publisher
                .SetPublishStrategy(new DefaultFanoutPublishStrategy(settings))
                .SetSerializer(new JsonMessageSerializer<string>());
        }

        [Test]
        public void QueuePersistenceShouldBeConfiguredExplicitly()
        {
            Assert.Throws<InvalidOperationException>(() => _publisher.Start());

            _publisher.Stop();
        }

        [Test]
        public void QueueRepositoryCanBeSet()
        {
            _publisher.SetQueueRepository(Substitute.For<IPublishingQueueRepository>());

            Assert.DoesNotThrow(() => _publisher.Start());

            _publisher.Stop();
        }

        [Test]
        public void QueuePersistenceCanBeDisabled()
        {
            _publisher.DisableInMemoryQueuePersistence();

            Assert.DoesNotThrow(() => _publisher.Start());

            _publisher.Stop();
        }
    }
}
