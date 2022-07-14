using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UpdateProductUnitPrice
{
    public class UpdateProductUnitPriceCommand : ICommand
    {
        public Guid Id { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
