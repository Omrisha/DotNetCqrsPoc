using Application;
using Application.Commands.AddNewProductCommand;
using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IApplicationContext, ApplicationContext>()
    .AddScoped<ICommandHandler<AddNewProductCommand>, AddNewProductCommandHandler>()
    .BuildServiceProvider();

var commandDispatcher = new CommandDispatcher(serviceProvider);

var product = new AddNewProductCommand(){
    Id = Guid.NewGuid(),
    Name = "iPhonr 13",
    Description = "Apple iPhone 13"
};

commandDispatcher.Send(product);