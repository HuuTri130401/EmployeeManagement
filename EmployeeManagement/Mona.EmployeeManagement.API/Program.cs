using Microsoft.EntityFrameworkCore;
using Mona.EmployeeManagement.API.MiddleWares;
using Mona.EmployeeManagement.Domain.Data;
using Mona.EmployeeManagement.Repositories.IRepository;
using Mona.EmployeeManagement.Repositories.Repository;
using Mona.EmployeeManagement.Repositories.UnitOfWork;
using Mona.EmployeeManagement.Services.Commons;
using Mona.EmployeeManagement.Services.CustomService;
using Mona.EmployeeManagement.Services.ICustomService;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //============ Connect DB ============//
    builder.Services.AddDbContext<EmployeeContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    });

    //============ Add auto mapper ============//
    builder.Services.AddAutoMapper(typeof(AutoMapperService));

    //============ Create instance for repository ============//
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

    //============ Create instance for service ============//
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();

    //============Configure logging============//
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //============register this middleware to ServiceCollection============//
    builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    // NLog: Catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}