using Cashing.Data;
using Cashing.Distributed_Services;
using Cashing.Helpers;
using Cashing.Hybrid_services;
using Cashing.In_Memory_Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
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
builder.Services.AddScoped<Cashing.Hybrid_services.IProductService, Cashing.Hybrid_services.ProductService>();
builder.Services.AddScoped<Cashing.OutputResonseCashService.IProductService, Cashing.OutputResonseCashService.ProductService>();

builder.Services.AddMemoryCache();
builder.Services.AddOutputCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "DSRedis";
});

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("SqlCache");
    options.SchemaName = "dbo";
    options.TableName = "CacheEntries";
});

builder.Services.AddHybridCache(options =>
{
   options.DefaultEntryOptions = new HybridCacheEntryOptions
   {
       Expiration = TimeSpan.FromMinutes(5),
       LocalCacheExpiration = TimeSpan.FromMinutes(5)
   };
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

app.UseOutputCache();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedDataProducts.Initialize(services);
}

app.Run();

