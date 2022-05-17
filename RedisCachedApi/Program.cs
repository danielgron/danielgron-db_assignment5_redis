using RedisCachedApi.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
 {
     options.Configuration = builder.Configuration.GetConnectionString("Redis");
     options.InstanceName = "SampleInstance";
 });


var redis = builder.Configuration.GetConnectionString("Redis") ?? throw new NullReferenceException("Redis key is missing");
builder.Services.AddSingleton<IDatabase>(x => ConnectionMultiplexer.Connect(redis).GetDatabase());

builder.Services.AddSingleton<OccupationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Occupation}/api/{cached}");

app.Run();
