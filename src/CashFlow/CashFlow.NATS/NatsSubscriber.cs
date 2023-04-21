using CashFlow.NATS.configure;
using CashFlow.NATS.factory;
using Microsoft.Extensions.Logging;
using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.NATS
{
    public class NatsSubscriber
    {
        private IConnection _connection;
        private readonly ILogger _logger;
        private readonly Server _server;
        private readonly ICreateConnectionFactory _createConnectionFactory;
        private IAsyncSubscription _subscription;
        public NatsSubscriber(
          ILogger<NatsSubscriber> logger,
          Server server,
          ICreateConnectionFactory createConnectionFactory)
        {
            _logger = logger;
            _server = server;
            _createConnectionFactory = createConnectionFactory;
            _connection = _createConnectionFactory.GetConnection(_server).Result;
        }

        public void Subscribe(string topic, string queueGroup, EventHandler<MsgHandlerEventArgs> messageHandler)
        {
            try
            {
                _subscription = _connection.SubscribeAsync(topic, queueGroup);
                _subscription.MessageHandler += messageHandler;
                _subscription.Start();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error subscribing to NATS");
                throw;
            }

        }

        public void Unsubscribe() 
        {
            _subscription?.Unsubscribe();
        }
    }
}
