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

        public virtual void Publish(string topic, string message, string correlationId)
        {
            Send(topic, message, correlationId);
        }

        private void Send(string topic, string message, string correlationId, int countErros = 0)
        {
            try
            {
                Msg msg = new Msg();
                msg.Subject = topic;
                msg.Data = Encoding.UTF8.GetBytes(message);
                msg.Header = new MsgHeader
                {
                    { "correlationId", correlationId }
                };

                _connection.Publish(msg);
            }
            catch (NATSReconnectBufferException ne) 
            {
                _logger.LogError(ne, $"{nameof(NATSReconnectBufferException)}: Error publishing message to NATS => Message: {ne.Message} BaseException:{ne.GetBaseException()}");
                RunErrorFlow(topic, message, correlationId, countErros, ne);
            }
            catch (NATSException ne)
            {
                _logger.LogError(ne, $"{nameof(NATSException)}: Error publishing message to NATS => Message: {ne.Message} BaseException:{ne.GetBaseException()}");
                RunErrorFlow(topic, message, correlationId, countErros, ne);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to NATS");
                throw;
            }
        }

        private void RunErrorFlow(string topic, string message, string correlationId, int countErros, Exception exception) 
        {
            if (_connection is null || _connection?.State == ConnState.DISCONNECTED)
                RemountConnection(topic, message, correlationId, countErros++);
            else
                throw exception;
        }

        private void RemountConnection(string topic, string message, string correlationId, int countErros)
        {
            _connection = _createConnectionFactory.GetConnection(_server).Result;

            if (countErros < 3)
                Send(topic, message, correlationId, countErros);
            else
                throw new Exception("Error publishing message to NATS");
        }
    }
}
