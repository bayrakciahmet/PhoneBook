using FluentValidation;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Services.Person.Services.Interfaces;
using PhoneBook.Services.Person.Services.Interfaces.Implementations;
using PhoneBook.Services.Person.Settings;
using PhoneBook.Services.Person.Settings.Interfaces;
using PhoneBook.Services.Person.Validators.ContactInfos;
using PhoneBook.Services.Person.Validators.Persons;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Person API", Version = "v1" });
});
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
builder.Services.AddValidatorsFromAssemblyContaining<PersonCreateDtoValidator>()
                .AddValidatorsFromAssemblyContaining<PersonUpdateDtoValidator>()
                .AddValidatorsFromAssemblyContaining<ContactInfoCreateDtoValidator>()
                .AddValidatorsFromAssemblyContaining<ContactInfoUpdateDtoValidator>();

var app = builder.Build();

#region MongoDb_IsNull_Set_Default_Value
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var personService = serviceProvider.GetRequiredService<IPersonService>();
    var contactInfoService = serviceProvider.GetRequiredService<IContactInfoService>();
    if (!(await personService.GetAllAsync()).Data.Any())
    {
        var personOne = await personService.CreateAsync(new PersonCreateDto { FirstName = "Ahmet", LastName = "BAYRAKCI", Company = "" });
        await contactInfoService.CreateAsync(new PhoneBook.Services.Person.Dtos.ContactInfos.ContactInfoCreateDto { PersonId = personOne.Data.UUID, InfoType = "Konum", InfoContent = "Konya" });
        await contactInfoService.CreateAsync(new PhoneBook.Services.Person.Dtos.ContactInfos.ContactInfoCreateDto { PersonId = personOne.Data.UUID, InfoType = "E-mail", InfoContent = "bayrakciahmet42@gmail.com" });
        await contactInfoService.CreateAsync(new PhoneBook.Services.Person.Dtos.ContactInfos.ContactInfoCreateDto { PersonId = personOne.Data.UUID, InfoType = "Telefon", InfoContent = "05073072605" });
        var personTwo = await personService.CreateAsync(new PersonCreateDto { FirstName = "Merve", LastName = "BAYRAKCI", Company = "" });
        await contactInfoService.CreateAsync(new PhoneBook.Services.Person.Dtos.ContactInfos.ContactInfoCreateDto { PersonId = personTwo.Data.UUID, InfoType = "Telefon", InfoContent = "05538330296" });
        await contactInfoService.CreateAsync(new PhoneBook.Services.Person.Dtos.ContactInfos.ContactInfoCreateDto { PersonId = personTwo.Data.UUID, InfoType = "Konum", InfoContent = "Konya" });

        var personThree = await personService.CreateAsync(new PersonCreateDto { FirstName = "Fatih", LastName = "Celen", Company = "" });
        await contactInfoService.CreateAsync(new PhoneBook.Services.Person.Dtos.ContactInfos.ContactInfoCreateDto { PersonId = personThree.Data.UUID, InfoType = "Konum", InfoContent = "Ankara" });
    }
}
#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API V1");
    });
}
app.MapControllers();
app.Run();

