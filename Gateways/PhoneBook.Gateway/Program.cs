using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");



builder.Services.AddOcelot(builder.Configuration);
//builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
//{
//    options.Authority = builder.Configuration["IdentityServerURL"];
//    options.Audience = "resource_gateway";
//    options.RequireHttpsMetadata = false;
//});

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}
//app.UseAuthorization();
app.UseSwaggerForOcelotUI().UseOcelot().Wait();
app.UseDeveloperExceptionPage();
app.MapControllers();
app.Run();


