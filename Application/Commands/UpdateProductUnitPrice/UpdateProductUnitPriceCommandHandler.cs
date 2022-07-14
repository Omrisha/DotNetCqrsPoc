using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UpdateProductUnitPrice
{
    public class UpdateProductUnitPriceCommandHandler : ICommandHandler<UpdateProductUnitPriceCommand>
    {
        private readonly IApplicationContextInMemoryDB _applicationContext;

        public UpdateProductUnitPriceCommandHandler(IApplicationContextInMemoryDB applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Handle(UpdateProductUnitPriceCommand command, CancellationToken cancellationToken)
        {
            var product = _applicationContext.Products.FirstOrDefault(p => p.Id == command.Id);
            if (product == null)
                throw new NotFoundException(nameof(Product), command.Id);
            else
                product.UnitPrice = command.UnitPrice;

            await _applicationContext.SaveChangesAsync(cancellationToken);
        }
    }
}
