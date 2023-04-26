using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Warehouse.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Configure services
builder.Services.AddControllers();
builder.Services.AddDbContext<WarehouseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalSqlServer1"), b => b.MigrationsAssembly("Warehouse.Api"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IGoodsIssueRepository, GoodsIssueRepository>();
builder.Services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockCardEntryRepository, StockCardEntryRepository>();
builder.Services.AddScoped<IStorageSlotRepository, StorageSlotRepository>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly(),typeof(IProductRepository).Assembly, typeof(ProductRepository).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
