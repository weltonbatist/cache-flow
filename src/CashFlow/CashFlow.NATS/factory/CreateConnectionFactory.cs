using CashFlow.NATS.configure;
using NATS.Client;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.NATS.factory
{
    public class CreateConnectionFactory : ICreateConnectionFactory
    {
        private readonly ConnectionFactory cf;
        public CreateConnectionFactory()
        {
            cf = new ConnectionFactory();
        }
        public Task<IConnection?> GetConnection(Server server)
        {
            var policy = Policy
               .Handle<Exception>()
               .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            IConnection? connection = null;

            policy.ExecuteAsync(() =>
            {
                connection = cf.CreateConnection(server.Url);
                return Task.CompletedTask;
            });


            return Task.FromResult(connection);

        }
    }
}
