using Microsoft.Extensions.Options;
using PhoneBook.Services.Person.Settings.Interfaces;
using PhoneBook.Services.Person.Settings;
using System.Reflection;
using PhoneBook.Services.Person.Services.Persons;
using PhoneBook.Services.Person.Services.ContactInfos;
using PhoneBook.Services.Person.Dtos.Persons;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AddScoped
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
#endregion

#region DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
#endregion



builder.Services.AddControllers();
var app = builder.Build();

#region MongoDb_IsNull_Set_Default_Value
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var personService = serviceProvider.GetRequiredService<IPersonService>();

    if (!(await personService.GetAllAsync()).Data.Any())
    {
        await personService.CreateAsync(new PersonCreateDto { FirstName = "Ahmet", LastName = "BAYRAKCI", Company = "" });
        await personService.CreateAsync(new PersonCreateDto { FirstName = "Merve", LastName = "BAYRAKCI", Company = "" });
    }
}

#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

