using JubilantBroccoli.BusinessLogic.Contracts;
using JubilantBroccoli.BusinessLogic.Implementations;
using JubilantBroccoli.Infrastructure.Core.Base;
using JubilantBroccoli.Infrastructure.UnitOfWork.Extensions;
using JubilantBroccoli.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text;
using JubilantBroccoli.BusinessLogic.Implementations.Base;
using JubilantBroccoli.BusinessLogic.Implementations.Menu;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var services = builder.Services;

    // Add services to the container.
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));
    services.AddUnitOfWork<ApplicationDbContext>();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console()
    );
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
    services.AddScoped<IJwtGenerator, JwtGenerator>();
    services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
                )
            };
        });

    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<IOrderProcessor, BurgerPreparation>();
    services.AddTransient<IOrderProcessor, PizzaPreparation>();
    services.AddTransient<IOrderProcessor, SushiPreparation>();
    services.AddTransient<IOrderProcessor, WokPreparation>();
    services.AddTransient<IOrderProcessor, BeveragePreparation>();
    services.AddTransient<IOrderProcessor, KebabPreparation>();

    var app = builder.Build();
    app.UseExceptionHandler(a => a.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;
        await context.Response.WriteAsJsonAsync(new { error = exception.Message });
    }));

    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "myappname v1"));
    }
    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();

    using (var scope = app.Services.CreateScope())
    {
        await DataInitializer.InitializeAsync(scope.ServiceProvider);
    }


    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

