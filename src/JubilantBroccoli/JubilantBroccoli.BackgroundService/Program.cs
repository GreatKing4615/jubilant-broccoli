using Hangfire;
using Hangfire.PostgreSql;
using JubilantBroccoli.BackgroundService;
using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations;
using JubilantBroccoli.BusinessLogic.Implementations.Menu;
using JubilantBroccoli.Infrastructure.Core.Base;
using JubilantBroccoli.Infrastructure.UnitOfWork.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        });
        services.AddSingleton<PostgreSqlStorageOptions>();
        services.AddHangfireServer(opt =>
        {
            opt.WorkerCount = 1;
            opt.IsLightweightServer=true;
        });
        services.AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
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
        //services.AddScoped<OrderCheckService>();
        services.AddScoped<MockFlowService>();
    })
    .Build();
using (var scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var options = serviceProvider.GetRequiredService<PostgreSqlStorageOptions>();
    options.PrepareSchemaIfNecessary = true;
    
    var backgroundJobClient = serviceProvider.GetRequiredService<IBackgroundJobClient>();
    //RecurringJob.AddOrUpdate<OrderCheckService>("OrderCheckService", x => x.ExecuteAsync(default), Cron.Minutely());
    RecurringJob.AddOrUpdate<MockFlowService>("MockFlowService", x => x.ExecuteAsync(default), "*/1 * * * *");
    Console.WriteLine("Hangfire Server started.");
    host.Run();
}