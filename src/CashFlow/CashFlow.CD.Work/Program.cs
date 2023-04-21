using CashFlow.CD.Work;
using CashFlow.CD.Work.extensions;
using CashFlow.NATS.configure;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logging) =>
    {
        logging.AddConfiguration(context.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();

    })
    .ConfigureServices((context, services) =>
    {
        services.AddPersistenceService();
        services.AddNATSConfiguration(context.Configuration);
        services.AddFinancialReportConsolidateDailyRepository(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
