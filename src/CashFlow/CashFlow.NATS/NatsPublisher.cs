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
    public class NatsPublisher
    {
        private IConnection _connection;
        private readonly ILogger _logger;
        private readonly Server _server;
        private readonly ICreateConnectionFactory _createConnectionFactory;

        public NatsPublisher(
            ILogger<NatsPublisher> logger, 
            Server server, 
            ICreateConnectionFactory createConnectionFactory)
        {
            _logger = logger;
            _server = server;
            _createConnectionFactory = createConnectionFactory;
            _connection = _createConnectionFactory.GetConnection(_server).Result;
        }

        public void Publish(string topic, string message)
        {
            Send(topic, message);
        }

        private void Send(string topic, string message, int countErros = 0)
        {
            try
            {
                _connection.Publish(topic, Encoding.UTF8.GetBytes(message));
            }
            catch (NATSReconnectBufferException ne) 
            {
                _logger.LogError(ne, $"{nameof(NATSReconnectBufferException)}: Error publishing message to NATS => Message: {ne.Message} BaseException:{ne.GetBaseException()}");
                RunErrorFlow(topic, message, countErros, ne);
            }
            catch (NATSException ne)
            {
                _logger.LogError(ne, $"{nameof(NATSException)}: Error publishing message to NATS => Message: {ne.Message} BaseException:{ne.GetBaseException()}");
                RunErrorFlow(topic, message, countErros, ne);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to NATS");
                throw;
            }
        }

        private void RunErrorFlow(string topic, string message, int countErros, Exception exception) 
        {
            if (_connection is null || _connection?.State == ConnState.DISCONNECTED)
                RemountConnection(topic, message, countErros++);
            else
                throw exception;
        }

        private void RemountConnection(string topic, string message, int countErros)
        {
            _connection = _createConnectionFactory.GetConnection(_server).Result;

            if (countErros < 3)
                Send(topic, message, countErros);
            else
                throw new Exception("Error publishing message to NATS");
        }
    }
}
