using MassTransit;
using Npgsql;
using PhoneBook.Services.Report.Consumers;
using PhoneBook.Services.Report.Repositories.Report;
using PhoneBook.Services.Report.Repositories.ReportLocation;
using PhoneBook.Services.Report.Services.Report;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("PostgreSql");
    return new NpgsqlConnection(connectionString);
});
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportLocationRepository, ReportLocationRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    // Default Port : 5672
    x.AddConsumer<CreateReportMessageCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
        cfg.ReceiveEndpoint("create-report-service", e =>
        {
            e.ConfigureConsumer<CreateReportMessageCommandConsumer>(context);
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();