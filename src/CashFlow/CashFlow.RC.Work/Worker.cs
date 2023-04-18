using CashFlow.NATS;
using CashFlow.RC.Work.service;

namespace CashFlow.RC.Work
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly NatsSubscriber _subscriber;
        private readonly PersistenceService _service;
        private const string TOPIC = "cashflow.financialrelease";
        public Worker(ILogger<Worker> logger, NatsSubscriber subscriber, PersistenceService service)
        {
            _logger = logger;
            _service = service;
            _subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await BackgroundProcess(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the background process.");
                throw;
            }
            
        }

        private async Task BackgroundProcess(CancellationToken stoppingToken)
        {
            await Task.Run(() => {

                _subscriber.Subscribe(TOPIC, (sender, message) =>
                {
                    try
                    {
                        _service.Process(message.Message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error on process message");
                    }

                });

            });
            
        }
    }
}