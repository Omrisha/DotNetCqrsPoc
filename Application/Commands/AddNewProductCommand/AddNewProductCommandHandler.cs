using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddNewProductCommand
{
    public class AddNewProductCommandHandler : ICommandHandler<AddNewProductCommand>
    {
        private readonly IApplicationContextInMemoryDB _context;

        public AddNewProductCommandHandler(IApplicationContextInMemoryDB context)
        {
            _context = context;
        }

        public async Task Handle(AddNewProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new AddNewProductCommandValidator();
            ValidationResult results = validator.Validate(command);
            bool validationSucceeded = results.IsValid;
            if (!validationSucceeded)
            {
                var failures = results.Errors.ToList();
                var message = new StringBuilder();
                failures.ForEach(f => { message.Append(f.ErrorMessage + Environment.NewLine); });
                throw new ValidationException(message.ToString());
            }

            var product = new Product
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                UnitPrice = 0,
                CurrentStock = 0
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
