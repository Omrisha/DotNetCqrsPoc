using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICommandHandler
    {
    }

    public interface ICommand
    {
    }

    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        Task Handle(T command, CancellationToken cancellationToken);
    }

    public interface ICommandDispatcher
    {
        Task Send<T>(T command, CancellationToken cancellationToken) where T : ICommand;
    }
}
