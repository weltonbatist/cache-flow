using CashFlow.NATS.configure;
using CashFlow.RC.Work;
using CashFlow.RC.Work.extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context,logging) =>
    {
        logging.AddConfiguration(context.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        
    })
    .ConfigureServices((context,services) =>
    {
        services.AddPersistenceService();
        services.AddNATSConfiguration(context.Configuration);
        services.AddFinancialReleaseRepository(context.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
