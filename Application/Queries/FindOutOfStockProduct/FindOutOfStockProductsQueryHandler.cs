using Application.Common;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.FindOutOfStockProduct
{
    public class FindOutOfStockProductsQueryHandler : IQueryHandler<FindOutOfStockProductsQuery>
    {
        private readonly IApplicationContextInMemoryDB _context;

        public FindOutOfStockProductsQueryHandler(IApplicationContextInMemoryDB context)
        {
            _context = context;
        }

        public async Task<IList<IResult>> Handle(FindOutOfStockProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Where(p => p.CurrentStock <= 0).ToListAsync();
            if (products == null)
                return null;
            
            var results = new List<IResult>();
            foreach (var p in products)
            {
                results.Add(new ProductInventory
                {
                    Id = p.Id,
                    Name = p.Name,
                    IsOutOfStock = p.CurrentStock <= 0
                });
            }

            return results;
        }
    }
}
