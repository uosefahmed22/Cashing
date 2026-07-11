using Cashing.Data;
using Cashing.Distributed_Services;
using Cashing.Helpers;
using Cashing.In_Memory_Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();


builder.Services.AddDbContext<CashingDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMemoryCache();

builder.Services.AddScoped<Cashing.In_Memory_Services.IProductService, Cashing.In_Memory_Services.ProductService>();
builder.Services.AddScoped<Cashing.Distributed_Services.IProductService, Cashing.Distributed_Services.ProductService>();

builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "DSRedis";
});


builder.Services.AddAutoMapper(cf => cf.AddProfile<MappingProfile>(), AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseHsts();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedDataProducts.Initialize(services);
}

app.Run();

