using Application.Commands;
using Application.Commands.AddNewProductCommand;
using Application.Commands.DeleteProduct;
using Application.Commands.UpdateProductCurrentStock;
using Application.Commands.UpdateProductUnitPrice;
using Application.Interfaces;
using Application.Queries;
using Application.Queries.FindOutOfStockProduct;
using Application.Queries.GetProductByName;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEntityFrameworkInMemoryDatabase()
    .AddDbContext<ApplicationContextInMemoryDB>(opt => opt.UseInMemoryDatabase(databaseName: "CQRS-POC"), ServiceLifetime.Singleton)
    .AddSingleton<IApplicationContextInMemoryDB>(p => p.GetService<ApplicationContextInMemoryDB>())
    .AddScoped<ICommandHandler<AddNewProductCommand>, AddNewProductCommandHandler>()
    .AddScoped<ICommandHandler<DeleteProductCommand>, DeleteProductCommandHandler>()
    .AddScoped<ICommandHandler<UpdateProductCurrentStockCommand>, UpdateProductCurrentStockCommandHandler>()
    .AddScoped<ICommandHandler<UpdateProductUnitPriceCommand>, UpdateProductUnitPriceCommandHandler>()
    .AddScoped<IQueryHandler<GetProductsByNameQuery>, GetProductsByNameQueryHandler>()
    .AddScoped<IQueryHandler<FindOutOfStockProductsQuery>, FindOutOfStockProductsQueryHandler>()
    .AddScoped<ICommandDispatcher, CommandDispatcher>()
    .AddScoped<IQueryDispatcher, QueryDispatcher>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers();
});

app.Run();
