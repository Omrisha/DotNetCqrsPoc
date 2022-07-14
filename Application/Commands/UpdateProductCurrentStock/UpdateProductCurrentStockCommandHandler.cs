using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UpdateProductCurrentStock
{
    public class UpdateProductCurrentStockCommandHandler : ICommandHandler<UpdateProductCurrentStockCommand>
    {
        private readonly IApplicationContextInMemoryDB _applicationContext;

        public UpdateProductCurrentStockCommandHandler(IApplicationContextInMemoryDB applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Handle(UpdateProductCurrentStockCommand command, CancellationToken cancellationToken)
        {
            var product = _applicationContext.Products.Where(p => p.Id == command.Id).SingleOrDefault();
            if (product == null)
                throw new NotFoundException(nameof(Product), command.Id);

            product.CurrentStock = command.CurrentStock;

            await _applicationContext.SaveChangesAsync(cancellationToken);
        }
    }
}
