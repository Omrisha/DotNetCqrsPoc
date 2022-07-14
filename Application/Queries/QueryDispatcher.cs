using Application.Exceptions;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IList<IResult>> Send<T>(T query, CancellationToken cancellationToken) where T : IQuery
        {
            var handler = _serviceProvider.GetService(typeof(IQueryHandler<T>));
            if (handler != null)
            {
                return await ((IQueryHandler<T>)handler).Handle(query, cancellationToken);
            }
            else
            {
                throw new NotFoundException($"Query doesn't have any handler {query.GetType().Name}");
            }
        }
    }
}
