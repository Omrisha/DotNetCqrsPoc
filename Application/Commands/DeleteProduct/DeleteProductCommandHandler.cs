using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IApplicationContextInMemoryDB _applicationContext;

        public DeleteProductCommandHandler(IApplicationContextInMemoryDB applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = _applicationContext.Products.FirstOrDefault(p => p.Id == command.Id);
            if (product == null)
                throw new NotFoundException(nameof(Product), command.Id);

            _applicationContext.Products.Remove(product);
            await _applicationContext.SaveChangesAsync(cancellationToken);
        }
    }
}
