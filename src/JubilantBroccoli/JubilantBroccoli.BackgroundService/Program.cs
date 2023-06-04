using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using JubilantBroccoli.BackgroundService;
using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations;
using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.BusinessLogic.Implementations.Menu;
using JubilantBroccoli.Infrastructure.Core.Base;
using JubilantBroccoli.Infrastructure.UnitOfWork.Extensions;
using JubilantBroccoli.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddUnitOfWork<ApplicationDbContext>();
        services.AddHangfire(config =>
        {
            config.UsePostgreSqlStorage(connectionString);
            config.UseSerilogLogProvider();
        });
        services.AddSingleton<PostgreSqlStorageOptions>();
        services.AddHangfireServer();

    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IOrderProcessor, BurgerPreparation>();
        services.AddTransient<IOrderProcessor, PizzaPreparation>();
        services.AddTransient<IOrderProcessor, SushiPreparation>();
        services.AddTransient<IOrderProcessor, WokPreparation>();
        services.AddTransient<IOrderProcessor, BeveragePreparation>();
        services.AddTransient<IOrderProcessor, KebabPreparation>();
        services.AddScoped<OrderCheckService>();

    })
    .Build();
using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var options = serviceProvider.GetRequiredService<PostgreSqlStorageOptions>();
    options.PrepareSchemaIfNecessary = true;
    
    var backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
    backgroundJobClient.Enqueue<OrderCheckService>(x => x.ExecuteAsync(default));

    Console.WriteLine("Hangfire Server started.");
    host.Run();
}