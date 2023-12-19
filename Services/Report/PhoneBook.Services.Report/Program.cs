using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using PhoneBook.Services.Report.Consumers;
using PhoneBook.Services.Report.Repositories.Interfaces;
using PhoneBook.Services.Report.Repositories.Interfaces.Implementations;
using PhoneBook.Services.Report.Services.Interfaces;
using PhoneBook.Services.Report.Services.Interfaces.Implementations;
using PhoneBook.Services.Report.Validators;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<IReportProcessingService, ReportProcessingService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "resource_report";
    options.RequireHttpsMetadata = false;
});


builder.Services.AddScoped<IDbConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("PostgreSql");
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportLocationRepository, ReportLocationRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportLocationService, ReportLocationService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddValidatorsFromAssemblyContaining<ReportCreateDtoValidator>()
                .AddValidatorsFromAssemblyContaining<ReportUpdateDtoValidator>();

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


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report API V1");
    });
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();