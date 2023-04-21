using CashFlow.NATS.configure;
using NATS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.NATS.factory
{
    public interface ICreateConnectionFactory
    {
        Task<IConnection?> GetConnection(Server server);
    }
}
