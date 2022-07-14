using Application.Commands;
using Application.Commands.AddNewProductCommand;
using Application.Commands.DeleteProduct;
using Application.Commands.UpdateProductCurrentStock;
using Application.Commands.UpdateProductUnitPrice;
using Application.Interfaces;
using Application.Queries;
using Application.Queries.FindOutOfStockProduct;
using Application.Queries.GetProductByName;
using cqrs_poc.Modules.Swagger;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var vPersonalised = Convert.ToBoolean(builder.Configuration["CustomSwaggerUi:Personalised"]);
var vCustomHeader = builder.Configuration["CustomSwaggerUi:HeaderImg"];
var vCustomTitle = builder.Configuration["CustomSwaggerUi:DocTitle"];
var vCustomPathCss = builder.Configuration["CustomSwaggerUi:PathCss"];

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

builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Modified V.1");
        c.RoutePrefix = string.Empty;

        if (vPersonalised)
        {
            c.DocumentTitle = vCustomTitle;
            c.HeadContent = vCustomHeader;
            c.InjectStylesheet(vCustomPathCss);
        }
    });
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers();
});

app.Run();
