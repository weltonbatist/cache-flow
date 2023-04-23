using CashFlow.NATS;
using CashFlow.NATS.configure;
using CashFlow.NATS.factory;
using CashFlow.RC.API.Service.services;
using CashFlow.RC.Common.dto;
using CashFlow.RC.Domain.enums;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;

namespace CashFlow.RC.Tests.service
{
    public class FinancialReleaseServiceTest
    {
        private static readonly Mock<ILogger<NatsPublisher>> _loggerNats = new Mock<ILogger<NatsPublisher>>();
        private static readonly Mock<Server> _server = new Mock<Server>();
        private static readonly Mock<ICreateConnectionFactory> _createConnectionFactory = new Mock<ICreateConnectionFactory>();
        private readonly Mock<NatsPublisher> _publisher = new Mock<NatsPublisher>(_loggerNats.Object, _server.Object, _createConnectionFactory.Object);

        private readonly Mock<ILogger<FinancialReleaseService>> _logger = new Mock<ILogger<FinancialReleaseService>>();

        public FinancialReleaseServiceTest()
        {
            _publisher.Setup(x => x.Publish(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void SendAValidCashFlowStatement()
        {
            FinancialReleaseStock financialReleaseStock = GenerateFinancialReleaseStockObjectValid();
            FinancialReleaseService financialReleaseService = new FinancialReleaseService(_logger.Object, _publisher.Object);
            var mockLoose = new Mock<FinancialReleaseService>(MockBehavior.Loose);
            mockLoose.Setup(x => x.LaunchNewCashFlow(financialReleaseStock, It.IsAny<string>())).Returns(Task.FromResult(true));
        }

        [Fact]
        public void SendAInValidCashFlowStatement()
        {
            FinancialReleaseStock financialReleaseStock = GenerateFinancialReleaseStockObjectInValid();
            FinancialReleaseService financialReleaseService = new FinancialReleaseService(_logger.Object, _publisher.Object);
            var mockLoose = new Mock<FinancialReleaseService>(MockBehavior.Loose);
            mockLoose.Setup(x => x.LaunchNewCashFlow(financialReleaseStock, It.IsAny<string>())).Returns(Task.FromResult(false));
        }

        private static FinancialReleaseStock GenerateFinancialReleaseStockObjectValid()
        {
            Mock<FinancialReleaseStock> mockFinancialReleaseStock = new Mock<FinancialReleaseStock>(MockBehavior.Loose);
            mockFinancialReleaseStock.SetupProperty(x => x.CashRegisterId,1);
            mockFinancialReleaseStock.SetupProperty(x => x.StoreId,1);
            mockFinancialReleaseStock.SetupProperty(x => x.Amount,100);
            mockFinancialReleaseStock.SetupProperty(x => x.Date, DateTime.Now);
            mockFinancialReleaseStock.SetupProperty(x => x.ReleaseType, (int)ReleaseType.creditor);
            return mockFinancialReleaseStock.Object;
        }

        private static FinancialReleaseStock GenerateFinancialReleaseStockObjectInValid()
        {
            Mock<FinancialReleaseStock> mockFinancialReleaseStock = new Mock<FinancialReleaseStock>(MockBehavior.Loose);
            mockFinancialReleaseStock.SetupProperty(x => x.CashRegisterId, 0);
            mockFinancialReleaseStock.SetupProperty(x => x.StoreId, 0);
            mockFinancialReleaseStock.SetupProperty(x => x.Amount, 0);
            mockFinancialReleaseStock.SetupProperty(x => x.Date, DateTime.MinValue);
            mockFinancialReleaseStock.SetupProperty(x => x.ReleaseType, (int)ReleaseType.creditor);
            return mockFinancialReleaseStock.Object;
        }
    }
}