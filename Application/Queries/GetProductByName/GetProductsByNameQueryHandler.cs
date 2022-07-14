using Application.Common;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetProductByName
{
    public class GetProductsByNameQueryHandler : IQueryHandler<GetProductsByNameQuery>
    {
        private readonly IApplicationContextInMemoryDB _context;

        public GetProductsByNameQueryHandler(IApplicationContextInMemoryDB context)
        {
            _context = context;
        }

        public async Task<IList<IResult>> Handle(GetProductsByNameQuery query, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Where(p => p.Name.Contains(query.Name, StringComparison.OrdinalIgnoreCase)).ToListAsync();
            if (products == null)
                return null;

            var results = new List<IResult>();
            foreach (var product in products)
            {
                results.Add(new ProductDisplay
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice
                });
            }

            return results;
        }
    }
}
