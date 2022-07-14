using Application.Exceptions;
using Application.Interfaces;
using System;

namespace Application.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _service;

        public CommandDispatcher(IServiceProvider service)
        {
            _service = service;
        }

        public async Task Send<T>(T command, CancellationToken cancellationToken) where T : ICommand
        {
            var handler = _service.GetService(typeof(ICommandHandler<T>));

            if (handler != null)
            {
                await ((ICommandHandler<T>)handler).Handle(command, cancellationToken);
            }
            else
            {
                throw new NotFoundException($"Command doesn't have any handler {command.GetType().Name}");
            }
        }
    }
}