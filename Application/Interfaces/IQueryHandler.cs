using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQueryHandler
    {
    }

    public interface IQuery
    {
        
    }

    public interface IQueryHandler<T> : IQueryHandler where T : IQuery
    {
        Task<IList<IResult>> Handle(T query, CancellationToken cancellationToken);
    }

    public interface IQueryDispatcher
    {
        Task<IList<IResult>> Send<T>(T query, CancellationToken cancellationToken) where T : IQuery;
    }
}
